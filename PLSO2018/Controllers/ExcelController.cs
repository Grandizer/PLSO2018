using DataContext;
using DataContext.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using PLSO2018.DataContext.Services;
using PLSO2018.Entities;
using PLSO2018.Entities.Support;
using PLSO2018.Website.Controllers;
using PLSO2018.Website.Models;
using PLSO2018.Website.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PLSO2018.Controllers
{

    [Produces("application/json")]
    [Route("api/Excel")]
    public class ExcelController : BaseController
    {

        private ExcelTemplateRepo excelTemplateRepo;
        private ICellStyle IsBold;
        //private ICellStyle IsTopAlign;
        private ICellStyle IsBlue;
        private ICellStyle IsValidation;
        private ICellStyle IsInteger = null;
        private readonly IEnumProperties enumProps;
        private PLSODb DataContext;

        public ExcelController(PLSODb context, ExcelTemplateRepo excelTemplateRepo, IEnumProperties enumProps, IHttpContextAccessor contextAccessor, ILoggerFactory loggerFactory) : base(contextAccessor)
        {
            this.excelTemplateRepo = excelTemplateRepo;
            base.logger = loggerFactory.CreateLogger<ExcelController>();
            this.enumProps = enumProps;
            DataContext = context;
        }

        [Route("GenerateBlankTemplate")]
        public async Task<IActionResult> GenerateBlankTemplateAsync()
        {
            var TheStream = new NPOIMemoryStream {
                AllowClose = false
            };
            var IntegerFields = new List<string> { "*** Surveyor Number", "DeedVolume", "DeedPage", "AutomatedFileNumber" };
            var FileName = $"PLSO_Upload_Template {DateTime.Now.ToString("yy-MMdd")}-{DateTime.Now.Ticks}.xlsx";

            try {
                var Columns = await excelTemplateRepo.GetTemplateColumnsAsync();

                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet1 = workbook.CreateSheet("PLSO Record Import");
                IsBold = CreateBoldStyle(workbook);
                IsBlue = CreateBlueStyle(workbook);
                IsValidation = CreateValidationStyle(workbook);
                IsInteger = CreateIntegerStyle(workbook);

                int RowIndex = 0;

                IRow row = sheet1.CreateRow(RowIndex++);

                foreach (var col in Columns.Result) {
                    var Cell = row.CreateCell(col.ColumnIndex - 1);

                    sheet1.SetColumnWidth(col.ColumnIndex - 1, (col.ColumnWidth * 256));

                    if (col.IsRequired)
                        Cell.CellStyle = IsBold;

                    Cell.SetCellType(CellType.String);
                    Cell.SetCellValue(col.DisplayName);
                } // foreach of the columns on the Display Name row

                row = sheet1.CreateRow(RowIndex++);

                foreach (var col in Columns.Result) {
                    var Cell = row.CreateCell(col.ColumnIndex - 1);
                    Cell.SetCellType(CellType.String);
                    Cell.SetCellValue(col.ExampleData);
                    Cell.CellStyle = IsBlue;
                } // foreach of the columns on the Example Data row

                row = sheet1.CreateRow(RowIndex++);

                foreach (var col in Columns.Result) {
                    var Cell = row.CreateCell(col.ColumnIndex - 1);
                    Cell.SetCellType(CellType.String);
                    Cell.SetCellValue((col.IsRequired ? "REQUIRED: " : "") + col.Validation);
                    Cell.CellStyle = IsValidation;
                    Cell.CellStyle.WrapText = true;
                } // foreach of the columns on the Validation row


                for (var index = 1; index < 6; index++) {
                    row = sheet1.CreateRow(RowIndex++);

                    foreach (var col in Columns.Result) {
                        var Cell = row.CreateCell(col.ColumnIndex - 1);

                        if (col.IsRequired)
                            Cell.CellStyle = IsBold;

                        if (col.FieldName == "SurveyDate") {
                            IDataFormat dataFormat = workbook.CreateDataFormat();
                            Cell.CellStyle.DataFormat = dataFormat.GetFormat("M/d/yyyy");
                        } else if (IntegerFields.Contains(col.FieldName)) {
                            Cell.SetCellType(CellType.Numeric);
                            Cell.CellStyle = IsInteger;
                        } else
                            Cell.SetCellType(CellType.String);
                    } // foreach of the columns on the Display Name row
                } // for 5 rows add formatting and make the cell bold if required

                XSSFFormulaEvaluator.EvaluateAllFormulaCells(workbook);

                workbook.Write(TheStream);
            } catch (Exception e) {
                TheStream.Dispose();
                logger.LogError(1, e, "Unable to export Blank Template to Excel");
            }

            TheStream.Seek(0, SeekOrigin.Begin);
            TheStream.AllowClose = true;

            return File(TheStream, "application/vnd.ms-excel", FileName);
        }

        [Route("TemplateColumns")]
        public async Task<IActionResult> GetTemplateColumnsAsync()
        {
            var Result = new PLSOResponse<List<ExcelTemplate>>();

            if (ModelState.IsValid) {
                Result = await excelTemplateRepo.GetTemplateColumnsAsync();
            } else {
                return BadRequest(Result);
            }

            return Ok(Result);
        }

        private ICellStyle IsError = null;
        private ICellStyle IsComment = null;

        public FileStreamResult ProcessExcelFile(Stream stream)
        {
            var size = stream.Length;
            var Errors = new List<ParsingError>();
            var NewRecords = new List<Record>();
            var TotalErrors = 0;
            var LatLongTypeID = 2; // enumProps.GetDBID(LocationTypes.LatLong);
            IWorkbook wb = WorkbookFactory.Create(stream);

            try {
                IsError = CreateErrorStyle(wb);
                IsComment = CreateCommentStyle(wb);
                ISheet sheet = wb.GetSheetAt(0);

                // Loop through each Row
                // Loop through each Column in the current row
                var UseDate = DateTime.UtcNow;

                // Row is zero based, skip row 0 (title row), 1 = Example, 2 = Validation
                for (var r = 3; r <= sheet.LastRowNum; r++) {
                    IRow row = sheet.GetRow(r);
                    var staged = new Record {
                        UploadedByID = UsersID,
                        UploadedDate = UseDate,
                        Active = false,
                    };
                    var RowErrors = new List<ParsingError>();

                    for (var c = 2; c <= 24; c++) { // c = 2 (Column C to start), 24 (Column Y to end)
                                                    // # - Req - Title          - Validation
                                                    // -----------------------------------------------
                                                    // A - No  - Parsing Errors - None
                                                    // B - No  - Comments       - None
                                                    // C - Yes - Map Image Name - 5 digits only
                                                    // D - Yes - City, Village  - None (max 50)
                                                    // E - Yes - County         - Minimum length = 5 (max 50)
                                                    // F - Yes - Defunct Twp    - None (max 50)
                                                    // G - No  - Lot Number     - Numbers only, seperated by comma's, no spaces
                                                    // H - No  - Section        - "
                                                    // I - No  - Tract          - None
                                                    // J - No  - Range          - None
                                                    // K - Yes - Survey Date    - MM/DD/YYYY
                                                    // L - Yes - Surveyor Name  - None
                                                    // M - No  - Surveyor No.   - Digits only
                                                    // N - Yes - Address        - Full address
                                                    // O - No  - Cross Street   - None
                                                    // P - No  - Parcel Numbers - ###-##-###,###-##-###...
                                                    // Q - No  - Volume         - Digits only
                                                    // R - No  - Page           - "
                                                    // S - No  - AFN...         - None
                                                    // T - No  - Subdivision    - None
                                                    // U - No  - Subd. Lot      - Digits only, seperated by commas
                                                    // V - No  - Survey Name    - None
                                                    // W - No  - Location       - Latitude on Longitude seperated by commas
                                                    // X - No  - Client         - None
                                                    // Y - No  - Notes          - None

                        var pCell = new ParsingCell(row.GetCell(c), r, c);

                        if (c == 2) {
                            // Column C: Map Image Name
                            var Check = RequiredString(pCell, 15, 5);

                            if (Check.InError) {
                                RowErrors.AddRange(Check.Errors);
                            } else {
                                staged.MapImageName = Check.Result;
                            }
                        } else if (c == 3) {
                            // Column D: City, Village, Township
                            var Check = RequiredString(pCell, 25, 5);

                            if (Check.InError) {
                                RowErrors.AddRange(Check.Errors);
                            } else {
                                staged.CityVillageTownship = Check.Result;
                            }
                        } else if (c == 4) {
                            // Column E: County
                            var Check = RequiredString(pCell, 15, 5);

                            if (Check.InError) {
                                RowErrors.AddRange(Check.Errors);
                            } else {
                                staged.County = Check.Result;
                            }
                        } else if (c == 5) {
                            // Column F: Defunct Township
                            var Check = RequiredString(pCell, 20, 5);

                            if (Check.InError)
                                RowErrors.AddRange(Check.Errors);
                            else
                                staged.DefunctTownship = Check.Result.Replace("township", "");
                        } else if (c == 6) {
                            // Column G: Lot Numbers
                            var Check = DelimetedDigitsOnly(pCell, ',', 15, 0, true);

                            if (Check.InError)
                                RowErrors.AddRange(Check.Errors);
                            else
                                staged.LotNumbers = Check.Result;
                        } else if (c == 7) {
                            // Column H: Section
                            var Check = DelimetedDigitsOnly(pCell, ',', 15, 0, true);

                            if (Check.InError)
                                RowErrors.AddRange(Check.Errors);
                            else
                                staged.Section = Check.Result;
                        } else if (c == 8) {
                            // Column I: Tract
                            var Check = NonRequiredString(pCell, 15);

                            if (Check.InError)
                                RowErrors.AddRange(Check.Errors);
                            else
                                staged.Tract = Check.Result;
                        } else if (c == 9) {
                            // Column J: Range
                            var Check = NonRequiredString(pCell, 15);

                            if (Check.InError)
                                RowErrors.AddRange(Check.Errors);
                            else
                                staged.Range = Check.Result;
                        } else if (c == 10) {
                            // Column K: Survey Date
                            var Check = RequiredDate(pCell);

                            if (Check.InError)
                                RowErrors.AddRange(Check.Errors);
                            else
                                staged.SurveyDate = DateTime.Parse(Check.Result);
                        } else if (c == 11) {
                            // Column L: Surveyor Name
                            var Check = RequiredString(pCell, 25, 5);

                            // TODO: This Name needs to be transmuted to an actual ApplicationUser ID
                            if (Check.InError)
                                RowErrors.AddRange(Check.Errors);
                            else {
                                staged.SurveyorName = Check.Result;
                                // TODO: Lookup the name in the security.AspNetUser table
                                // If found, add the ID to the staged.SurveyorID field
                            }
                        } else if (c == 12) {
                            // Column M: Surveyor Number
                            var Check = NonRequiredDigitsOnly(pCell, 5, 2);

                            if (Check.InError)
                                RowErrors.AddRange(Check.Errors);
                            else
                                staged.SurveyorNumber = Check.Result;
                        } else if (c == 13) {
                            // Column N: Address
                            var Check = RequiredString(pCell, 100, 5);

                            if (Check.InError)
                                RowErrors.AddRange(Check.Errors);
                            else
                                staged.Address = Check.Result;
                        } else if (c == 14) {
                            // Column O: Cross Street Name
                            var Check = NonRequiredString(pCell, 30);

                            if (Check.InError)
                                RowErrors.AddRange(Check.Errors);
                            else
                                staged.CrossStreet = Check.Result;
                        } else if (c == 15) {
                            // Column P: Parcel Numbers
                            var Check = DelimetedDigitsOnly(pCell, new List<char> { ',', '-' }, 30);

                            if (Check.InError)
                                RowErrors.AddRange(Check.Errors);
                            else
                                staged.ParcelNumbers = Check.Result;
                        } else if (c == 16) {
                            // Column Q: Volume
                            var Check = NonRequiredDigitsOnly(pCell, 8, 1);

                            if (Check.InError)
                                RowErrors.AddRange(Check.Errors);
                            else {
                                if (int.TryParse(Check.Result, out int value))
                                    staged.DeedVolume = value; // TODO: This needs to be a TryParse so we can handle overflows to long
                            }
                            // TODO: Idealy, the method (in this case) NonRequiredDigitsOnly will return a Null or push an Error
                        } else if (c == 17) {
                            // Column R: Page
                            var Check = NonRequiredDigitsOnly(pCell, 6, 1);

                            if (Check.InError)
                                RowErrors.AddRange(Check.Errors);
                            else if (int.TryParse(Check.Result, out int value))
                                staged.DeedPage = value; // TODO: This needs to be a TryParse so we can handle overflows to long
                        } else if (c == 18) {
                            // Column S: AFN
                            var Check = NonRequiredString(pCell, 18, 3);

                            if (Check.InError)
                                RowErrors.AddRange(Check.Errors);
                            else
                                staged.AutomatedFileNumber = Check.Result;
                        } else if (c == 19) {
                            // Column T: Subdivision
                            var Check = NonRequiredString(pCell, 50, 5);

                            if (Check.InError)
                                RowErrors.AddRange(Check.Errors);
                            else
                                staged.Subdivision = Check.Result;
                        } else if (c == 20) {
                            // Column U: Subdivision-Sublot
                            var Check = DelimetedDigitsOnly(pCell, ',', 10, 0, true);

                            if (Check.InError)
                                RowErrors.AddRange(Check.Errors);
                            else
                                staged.Sublot = Check.Result;
                        } else if (c == 21) {
                            // Column V: Survey Name
                            var Check = NonRequiredString(pCell, 50);

                            if (Check.InError)
                                RowErrors.AddRange(Check.Errors);
                            else
                                staged.SurveyName = Check.Result;
                        } else if (c == 22) {
                            // Column W: Location
                            var Check = DelimetedDigitsOnly(pCell, new List<char> { ',', '.', '-' }, 30, truncateSpaces: true);
                            int Parts = string.IsNullOrWhiteSpace(Check.Result) ? 0 : Check.Result.Split(',').Count();

                            if (Check.InError)
                                RowErrors.AddRange(Check.Errors);
                            else if ((Parts == 1) || (Parts > 2))
                                RowErrors.Add(new ParsingError(pCell, "Invalid Format for Location"));
                            else if (!string.IsNullOrWhiteSpace(Check.Result)) {
                                var Loc = new Location {
                                    LocationTypeID = LatLongTypeID,
                                    Latitude = Convert.ToDecimal(Check.Result.Split(',')[0]),
                                    Longitude = Convert.ToDecimal(Check.Result.Split(',')[1])
                                };
                                staged.Location = Loc;
                            }
                        } else if (c == 23) {
                            // Column X: Client
                            var Check = NonRequiredString(pCell, 50);

                            if (Check.InError)
                                RowErrors.AddRange(Check.Errors);
                            else
                                staged.ClientName = Check.Result;
                        } else if (c == 24) {
                            // Column Y: Notes
                            var Check = NonRequiredString(pCell, 2000);

                            if (Check.InError)
                                RowErrors.AddRange(Check.Errors);
                            else
                                staged.Notes = Check.Result; // notes
                        }
                    } // foreach of the Columns

                    var CommentCell = row.GetCell(0) ?? row.CreateCell(0);

                    if (RowErrors.Count > 0) {
                        CommentCell.SetCellValue(new XSSFRichTextString($"{RowErrors.Count} Errors:\n{string.Join("\n", RowErrors.Select(x => $"[{ColumnIndexToLetter(x.Cell.Column + 1)}] {x.Message}"))}"));
                        CommentCell.CellStyle = IsComment;

                        foreach (var error in RowErrors) {
                            var cell = error.Cell.Cell == null ? row.CreateCell(error.Cell.Column) : row.GetCell(error.Cell.Column);
                            cell.CellStyle = IsError;
                        }
                    } else {
                        NewRecords.Add(staged); // Only add the record if the row is valid
                        CommentCell.SetCellValue("Validation: OK");
                    }

                    Errors.AddRange(RowErrors);

                    if (RowErrors.Count > 0) {
                        TotalErrors += RowErrors.Count;
                    } else {
                        // The row has validated successfully, check to see if it exists
                        var HasChodeMatches = DataContext.Records
                            .Where(x => x.HashCode == staged.GetHashCode())
                            .Select(x => x.ID)
                            .ToList();

                        if (HasChodeMatches.Count > 0) {
                            // Hash code found, should be already in the database
                            // Add to validation column
                            TotalErrors++;
                            var MatchIDs = "";
                            foreach (var match in HasChodeMatches) {
                                MatchIDs += $"{match},";
                            }
                            CommentCell.SetCellValue($"Validation: OK - Found {HasChodeMatches.Count} Potential hashcode matches - IDs: {MatchIDs}");
                        } else {
                            var Matches = DataContext.Records
                                .Where(x =>
                                    x.MapImageName == staged.MapImageName &&
                                    x.CityVillageTownship == staged.CityVillageTownship &&
                                    x.State == staged.State &&
                                    x.County == staged.County &&
                                    x.DefunctTownship == staged.DefunctTownship &&
                                    x.SurveyDate == staged.SurveyDate &&
                                    x.Address == staged.Address
                                )
                                .Select(x => x.ID)
                                .ToList();

                            if (Matches.Count > 0) {
                                TotalErrors++;
                                var MatchIDs = "";
                                foreach (var match in Matches) {
                                    MatchIDs += $"{match},";
                                }
                                CommentCell.SetCellValue($"Validation: OK - Found {Matches.Count} Potential field matches - IDs: {MatchIDs}");
                            }
                        }
                    }
                } // foreach of the Rows
            } catch (Exception e) {
                Console.WriteLine($"Exceiption: {e.Message} - {e.StackTrace}");
            }

            if (TotalErrors > 0)
                Console.WriteLine($"There were a total of {TotalErrors} errors.");
            else {
                int WriteErrors = 0;
                var bob = WriteErrors;
                Console.WriteLine($"Saving {NewRecords.Count} Records to the database.");

                ISheet sheet = wb.GetSheetAt(0);

                for (int index = 0; index < NewRecords.Count; index++) {
                    IRow row = sheet.GetRow(3 + index);

                    var r = NewRecords[index];
                    r.HashCode = r.GetHashCode();

                    try {
                        DataContext.Records.Add(r);
                        DataContext.SaveChanges(1);
                        row.GetCell(0).SetCellValue($"{row.GetCell(0).StringCellValue}, Save: OK - {r.ID}");
                    } catch (Exception e) {
                        row.GetCell(0).SetCellValue($"{row.GetCell(0).StringCellValue}, Save: Failed - {(e.InnerException != null ? e.InnerException.Message : e.Message)}");
                        WriteErrors++;
                    }
                } // foreach of the Records

                if (WriteErrors > 0)
                    Console.WriteLine($"There were a total of {WriteErrors} Write errors.");
            }

            var TheStream = new NPOIMemoryStream {
                AllowClose = false
            };

            try {
                wb.Write(TheStream);
            } catch (Exception e) {
                TheStream.Dispose();
                logger.LogError(1, e, "Unable to export Blank Template to Excel");
            }

            TheStream.Seek(0, SeekOrigin.Begin);
            TheStream.AllowClose = true;

            return File(TheStream, "application/vnd.ms-excel", $"{DateTime.Now.ToString("yy-MMdd")}-Testing.xlsx");
        }

        private string CellValue(ParsingCell cell)
        {
            var Result = "";

            if (cell.Cell != null) {
                var cellType = cell.GetType();

                switch (cell.Cell.CellType) {
                    case CellType.String:
                        Result = cell.Cell.StringCellValue.Trim();
                        break;
                    case CellType.Numeric:
                        if (DateUtil.IsCellDateFormatted(cell.Cell)) {
                            DateTime date = cell.Cell.DateCellValue;
                            ICellStyle style = cell.Cell.CellStyle;
                            // Excel uses lowercase m for month whereas .Net uses uppercase
                            string format = style.GetDataFormatString().Replace('m', 'M');
                            Result = date.ToString(format);
                        } else {
                            Result = cell.Cell.NumericCellValue.ToString().Trim();
                        }
                        break;
                    case CellType.Boolean:
                        Result = cell.Cell.BooleanCellValue ? "TRUE" : "FALSE";
                        break;
                    case CellType.Error:
                        Result = FormulaError.ForInt(cell.Cell.ErrorCellValue).String;
                        break;
                }
            }

            return Result;
        }

        private ParsingError AddCellError(ParsingCell cell, string message)
        {
            return new ParsingError {
                Cell = cell,
                Message = message,
            };
        }

        private ParseResult RequiredDigitsOnly(ParsingCell cell, int maxLength = 0, int minLength = 0)
        {
            var input = CellValue(cell);
            var Result = new ParseResult();

            if (string.IsNullOrWhiteSpace(input))
                Result.Errors.Add(AddCellError(cell, $"Field is required"));
            if ((maxLength > 0) && (input.Length > maxLength))
                Result.Errors.Add(AddCellError(cell, $"Input string exceeds the maximum length of {maxLength}"));
            else if ((minLength > 0) && (input.Length < minLength))
                Result.Errors.Add(AddCellError(cell, $"Input string must be at least {minLength} characters"));

            if (!input.All(c => ((c >= '0') && (c <= '9'))))
                Result.Errors.Add(AddCellError(cell, $"Input string contains characters other than digits"));

            if (Result.InError == false)
                Result.Result = input;

            return Result;
        }

        private ParseResult NonRequiredDigitsOnly(ParsingCell cell, int maxLength = 0, int minLength = 0)
        {
            var input = CellValue(cell);
            var Result = new ParseResult();

            if ((maxLength > 0) && (input.Length > maxLength))
                Result.Errors.Add(AddCellError(cell, $"Input string exceeds the maximum length of {maxLength}"));
            else if ((minLength > 0) && (input.Length < minLength) && (input.Length > 0))
                Result.Errors.Add(AddCellError(cell, $"Input string must be at least {minLength} characters"));

            if (!input.All(c => ((c >= '0') && (c <= '9'))))
                Result.Errors.Add(AddCellError(cell, $"Input string contains characters other than digits"));

            if (Result.InError == false)
                Result.Result = string.IsNullOrWhiteSpace(input) ? null : input;

            return Result;
        }

        private ParseResult DelimetedDigitsOnly(ParsingCell cell, char delimeter, int maxLength = 0, int minLength = 0, bool truncateSpaces = false)
        {
            // As of now, none of the Delimeted fields are Required
            var input = CellValue(cell);
            var Result = new ParseResult();

            input = truncateSpaces ? input.Replace(" ", "") : input;

            if ((maxLength > 0) && (input.Length > maxLength))
                Result.Errors.Add(AddCellError(cell, $"Input string exceeds the maximum length of {maxLength}"));
            else if ((minLength > 0) && (input.Length < minLength) && (input.Length > 0))
                Result.Errors.Add(AddCellError(cell, $"Input string must be at least {minLength} characters"));

            if (!input.All(c => ((c >= '0') && (c <= '9')) || (c == delimeter)))
                Result.Errors.Add(AddCellError(cell, $"Input string contains characters other than digits and the desired delimeter"));

            if (Result.InError == false)
                Result.Result = string.IsNullOrWhiteSpace(input) ? null : input;

            return Result;
        }

        private ParseResult DelimetedDigitsOnly(ParsingCell cell, List<char> delimiters, int maxLength = 0, int minLength = 0, bool truncateSpaces = false)
        {
            // As of now, none of the Delimeted fields are Required
            var input = CellValue(cell);
            var Result = new ParseResult();

            input = truncateSpaces ? input.Replace(" ", "") : input;

            if ((maxLength > 0) && (input.Length > maxLength))
                Result.Errors.Add(AddCellError(cell, $"Input string exceeds the maximum length of {maxLength}"));
            else if ((minLength > 0) && (input.Length < minLength) && (input.Length > 0))
                Result.Errors.Add(AddCellError(cell, $"Input string must be at least {minLength} characters"));

            if (!input.All(c => (((c >= '0') && (c <= '9')) || (delimiters.Contains(c)))))
                Result.Errors.Add(AddCellError(cell, $"Input string contains characters other than digits and any of the desired delimiters"));

            if (Result.InError == false)
                Result.Result = string.IsNullOrWhiteSpace(input) ? null : input;

            return Result;
        }

        private ParseResult RequiredString(ParsingCell cell, int maxLength = 0, int minLength = 0)
        {
            var input = CellValue(cell);
            var Result = new ParseResult();

            if (string.IsNullOrWhiteSpace(input))
                Result.Errors.Add(AddCellError(cell, $"Field is required"));
            else if ((maxLength > 0) && (input.Length > maxLength))
                Result.Errors.Add(AddCellError(cell, $"Input string exceeds the maximum length of {maxLength}"));
            else if ((minLength > 0) && (input.Length < minLength))
                Result.Errors.Add(AddCellError(cell, $"Input string must be at least {minLength} characters"));

            if (Result.InError == false)
                Result.Result = input;

            return Result;
        }

        private ParseResult NonRequiredString(ParsingCell cell, int maxLength = 0, int minLength = 0)
        {
            var input = CellValue(cell);
            var Result = new ParseResult();

            if ((maxLength > 0) && (input.Length > maxLength))
                Result.Errors.Add(AddCellError(cell, $"Input string exceeds the maximum length of {maxLength}"));
            else if ((minLength > 0) && (input.Length < minLength) && (input.Length > 0))
                Result.Errors.Add(AddCellError(cell, $"Input string must be at least {minLength} characters"));

            if (Result.InError == false)
                Result.Result = string.IsNullOrWhiteSpace(input) ? null : input;

            return Result;
        }

        private ParseResult RequiredDate(ParsingCell cell)
        {
            var input = CellValue(cell);
            var Result = new ParseResult();
            DateTime TheDate = DateTime.Now;

            if (string.IsNullOrWhiteSpace(input))
                Result.Errors.Add(AddCellError(cell, $"Field is required"));
            else if (!DateTime.TryParse(input, out TheDate)) {
                Result.Errors.Add(AddCellError(cell, $"Unable to parse the given date"));
            }

            if (Result.InError == false)
                Result.Result = TheDate.ToString("MM/dd/yyyy");

            return Result;
        }



        private static string ColumnIndexToLetter(int colIndex)
        {
            string Result = "";
            int div = colIndex;
            int mod = 0;

            while (div > 0) {
                mod = (div - 1) % 26;
                Result = (char)(65 + mod) + Result;
                div = (int)((div - mod) / 26);
            }

            return Result;
        }

        private static ICellStyle CreateBoldStyle(IWorkbook workbook)
        {
            var Result = workbook.CreateCellStyle();
            var Font = workbook.CreateFont();

            Font.Boldweight = (short)FontBoldWeight.Bold;
            Result.SetFont(Font);
            Result.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");

            return Result;
        }

        private static ICellStyle CreateTopAlignmentStyle(IWorkbook workbook)
        {
            var Result = workbook.CreateCellStyle();

            Result.VerticalAlignment = VerticalAlignment.Top;

            return Result;
        }

        private static ICellStyle CreateBlueStyle(IWorkbook workbook)
        {
            var Result = workbook.CreateCellStyle();
            Result.FillForegroundColor = HSSFColor.Automatic.Index;
            Result.FillPattern = FillPattern.NoFill;

            var UniversalFont = workbook.CreateFont();
            UniversalFont.Color = 21;
            Result.SetFont(UniversalFont);

            return Result;
        }

        private static ICellStyle CreateErrorStyle(IWorkbook workbook)
        {
            var Result = workbook.CreateCellStyle();
            Result.FillForegroundColor = HSSFColor.Red.Index;
            Result.FillPattern = FillPattern.ThickBackwardDiagonals;
            return Result;
        }

        private static ICellStyle CreateCommentStyle(IWorkbook workbook)
        {
            var Result = workbook.CreateCellStyle();
            Result.Alignment = HorizontalAlignment.Left;
            Result.VerticalAlignment = VerticalAlignment.Top;
            Result.WrapText = true;
            return Result;
        }

        private static ICellStyle CreateIntegerStyle (IWorkbook workbook)
        {
            var Result = workbook.CreateCellStyle();
            Result.DataFormat = workbook.CreateDataFormat().GetFormat("###0");
            return Result;
        }

        private static ICellStyle CreateValidationStyle(IWorkbook workbook)
        {
            var Result = workbook.CreateCellStyle();

            Result.FillForegroundColor = HSSFColor.Automatic.Index;
            Result.FillPattern = FillPattern.NoFill;
            Result.VerticalAlignment = VerticalAlignment.Top;

            var UniversalFont = workbook.CreateFont();

            UniversalFont.Color = IndexedColors.DarkRed.Index;
            Result.SetFont(UniversalFont);

            return Result;
        }

    }
}

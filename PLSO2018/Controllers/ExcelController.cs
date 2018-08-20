using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PLSO2018.Website.Controllers;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using PLSO2018.Website.Support;
using DataContext.Repositories;
using PLSO2018.Entities.Support;
using PLSO2018.Entities;
using System.IO;
using PLSO2018.Website.Models;

namespace PLSO2018.Controllers {

	[Produces("application/json")]
	[Route("api/Excel")]
	public class ExcelController : BaseController {

		private ExcelTemplateRepo excelTemplateRepo;
		private ICellStyle IsBold;
		private ICellStyle IsTopAlign;
		private ICellStyle IsBlue;
		private ICellStyle IsValidation;

		public ExcelController(ExcelTemplateRepo excelTemplateRepo, ILoggerFactory loggerFactory) {
			this.excelTemplateRepo = excelTemplateRepo;
			base.logger = loggerFactory.CreateLogger<ExcelController>();
		}

		[Route("GenerateBlankTemplate")]
		public async Task<IActionResult> GenerateBlankTemplateAsync() {
			var TheStream = new NPOIMemoryStream {
				AllowClose = false
			};

			var FileName = $"PLSO_Upload_Template {DateTime.Now.ToString("MM-dd-yyyy")}-{DateTime.Now.Ticks}.xlsx";

			try {
				var Columns = await excelTemplateRepo.GetTemplateColumnsAsync();

				IWorkbook workbook = new XSSFWorkbook();
				ISheet sheet1 = workbook.CreateSheet("PLSO Record Import");
				IsBold = CreateBoldStyle(workbook);
				IsBlue = CreateBlueStyle(workbook);
				IsValidation = CreateValidationStyle(workbook);

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
		public async Task<IActionResult> GetTemplateColumnsAsync() {
			var Result = new PLSOResponse<List<ExcelTemplate>>();

			if (ModelState.IsValid) {
				Result = await excelTemplateRepo.GetTemplateColumnsAsync();
			} else {
				return BadRequest(Result);
			}

			return Ok(Result);
		}

		private ICellStyle IsError = null;

		public FileStreamResult ProcessExcelFile(Stream stream) {
			var size = stream.Length;
			var Errors = new List<ParsingError>();
			var Records = new List<StagedRecord>();
			IWorkbook wb = WorkbookFactory.Create(stream);

			try {
				IsError = CreateErrorStyle(wb);
				ISheet sheet = wb.GetSheetAt(0);

				//long RowIndex = 1;
				//long ColumnIndex = 1; // A - X == 24

				// Loop through each Row
				// Loop through each Column in the current row
				var UseDate = DateTime.UtcNow;
				var UserID = 1; // Me

				// Row is zero based, skip row 0 (title row), 1 = Example, 2 = Validation
				for (var r = 3; r <= sheet.LastRowNum; r++) {
					IRow row = sheet.GetRow(r);
					var staged = new StagedRecord {
						CreatedByID = UserID,
						CreationDate = UseDate,
					};
					var RowErrors = new List<ParsingError>();

					for (var c = 0; c <= 23; c++) {
						// # - Req - Title          - Validation
						// -----------------------------------------------
						// A - No  - Comments       - None
						// B - Yes - Map Image Name - 5 digits only
						// C - Yes - City, Village  - None (max 50)
						// D - Yes - County         - Minimum length = 5 (max 50)
						// E - Yes - Defunct Twp    - None (max 50)
						// F - No  - Lot Number     - Numbers only, seperated by comma's, no spaces
						// G - No  - Section        - "
						// H - No  - Tract          - None
						// I - No  - Range          - None
						// J - Yes - Survey Date    - MM/DD/YYYY
						// K - Yes - Surveyor Name  - None
						// L - No  - Surveyor No.   - Digits only
						// M - Yes - Address        - Full address
						// N - No  - Cross Street   - None
						// O - No  - Parcel Numbers - ###-##-###,###-##-###...
						// P - No  - Volume         - Digits only
						// Q - No  - Page           - "
						// R - No  - AFN...         - None
						// S - No  - Subdivision    - None
						// T - No  - Subd. Lot      - Digits only, seperated by commas
						// U - No  - Survey Name    - None
						// V - No  - Location       - Latitude on Longitude seperated by commas
						// W - No  - Client         - None
						// X - No  - Notes          - None

						var pCell = new ParsingCell(row.GetCell(c), r, c);

						if (c == 1) {
							// Column B: Map Image Name
							var Check = RequiredDigitsOnly(pCell, 5);

							if (Check.InError) {
								RowErrors.AddRange(Check.Errors);
							} else {
								staged.ImageFileName = Check.Result;
							}
						} else if (c == 2) {
							// Column C: City, Village, Township
							var Check = RequiredString(pCell, 50, 5);

							if (Check.InError) {
								RowErrors.AddRange(Check.Errors);
							} else {
								staged.City = Check.Result;
							}
						} else if (c == 3) {
							// Column D: County
							var Check = RequiredString(pCell, 50, 5);

							if (Check.InError) {
								RowErrors.AddRange(Check.Errors);
							} else {
								staged.County = Check.Result;
							}
						} else if (c == 4) {
							// Column E: Defunct Township
							var Check = RequiredString(pCell, 50, 5);

							if (Check.InError)
								RowErrors.AddRange(Check.Errors);
							else
								staged.Township = Check.Result; // TODO: Record.cs is not represeting the Excel file
						} else if (c == 5) {
							// Column F: Lot Numbers
							var Check = DelimetedDigitsOnly(pCell, ',', 50, 0, true);

							if (Check.InError)
								RowErrors.AddRange(Check.Errors);
							else
								staged.OriginalLot = Check.Result; // TODO: Record.cs is not represeting the Excel file
						} else if (c == 6) {
							// Column G: Section
							var Check = DelimetedDigitsOnly(pCell, ',', 50, 0, true);

							if (Check.InError)
								RowErrors.AddRange(Check.Errors);
							else
								staged.Section = Check.Result;
						} else if (c == 7) {
							// Column H: Tract
							var Check = NonRequiredString(pCell, 50);

							if (Check.InError)
								RowErrors.AddRange(Check.Errors);
							else
								staged.Tract = Check.Result;
						} else if (c == 8) {
							// Column I: Range
							var Check = NonRequiredString(pCell, 50);

							if (Check.InError)
								RowErrors.AddRange(Check.Errors);
							else
								staged.Range = Check.Result;
						} else if (c == 9) {
							// Column J: Survey Date
							var Check = RequiredDate(pCell);

							if (Check.InError)
								RowErrors.AddRange(Check.Errors);
							else
								staged.SurveyDate = DateTime.Parse(Check.Result);
						} else if (c == 10) {
							// Column K: Surveyor Name
							var Check = RequiredString(pCell, 50, 5);

							if (Check.InError)
								RowErrors.AddRange(Check.Errors);
							else
								staged.SurveyName = Check.Result; // TODO: Record.cs is not represeting the Excel file
						} else if (c == 11) {
							// Column L: Surveyor Number
							var Check = NonRequiredDigitsOnly(pCell, 15, 2);

							if (Check.InError)
								RowErrors.AddRange(Check.Errors);
							else
								staged.ParcelNumber = Check.Result; // TODO: Record.cs is not represeting the Excel file
						} else if (c == 12) {
							// Column M: Address
							var Check = RequiredString(pCell, 50, 5);

							if (Check.InError)
								RowErrors.AddRange(Check.Errors);
							else
								staged.Address = Check.Result;
						} else if (c == 13) {
							// Column N: Cross Street Name
							var Check = NonRequiredString(pCell, 50);

							if (Check.InError)
								RowErrors.AddRange(Check.Errors);
							else
								staged.Address = Check.Result;
						} else if (c == 14) {
							// Column O: Parcel Numbers
							var Check = DelimetedDigitsOnly(pCell, new List<char> { ',', '-' }, 50);

							if (Check.InError)
								RowErrors.AddRange(Check.Errors);
							else
								staged.ParcelNumber = Check.Result;
						} else if (c == 15) {
							// Column P: Volume
							var Check = NonRequiredDigitsOnly(pCell, 50, 2);

							if (Check.InError)
								RowErrors.AddRange(Check.Errors);
							else {
								if (int.TryParse(Check.Result, out int value))
									staged.DeedVolume = value; // TODO: This needs to be a TryParse so we can handle overflows to long
							}
							// TODO: Idealy, the method (in this case) NonRequiredDigitsOnly will return a Null or push an Error
						} else if (c == 16) {
							// Column Q: Page
							var Check = NonRequiredDigitsOnly(pCell, 50, 2);

							if (Check.InError)
								RowErrors.AddRange(Check.Errors);
							else if (int.TryParse(Check.Result, out int value))
								staged.DeedPage = value; // TODO: This needs to be a TryParse so we can handle overflows to long
						} else if (c == 17) {
							// Column R: AFN
							var Check = NonRequiredString(pCell, 50, 3);

							if (Check.InError)
								RowErrors.AddRange(Check.Errors);
							else
								staged.AutomatedFileNumber = Check.Result;
						} else if (c == 18) {
							// Column S: Subdivision
							var Check = NonRequiredString(pCell, 50, 5);

							if (Check.InError)
								RowErrors.AddRange(Check.Errors);
							else
								staged.Subdivision = Check.Result;
						} else if (c == 19) {
							// Column T: Subdivision-Sublot
							var Check = DelimetedDigitsOnly(pCell, ',', 50, 0, true);

							if (Check.InError)
								RowErrors.AddRange(Check.Errors);
							else
								staged.Sublot = Check.Result; // TODO: Record.cs is not represeting the Excel file
						} else if (c == 20) {
							// Column U: Survey Name
							var Check = NonRequiredString(pCell, 50);

							if (Check.InError)
								RowErrors.AddRange(Check.Errors);
							else
								staged.SurveyName = Check.Result;
						} else if (c == 21) {
							// Column V: Location
							var Check = DelimetedDigitsOnly(pCell, new List<char> { ',', '.' }, 50);

							if (Check.InError)
								RowErrors.AddRange(Check.Errors);
							else {
								var Loc = new Location();
								// TODO: Ensure that there are 2 and only 2 decimals
								Loc.Latitude = Convert.ToDecimal(Check.Result.Split(',')[0]);
								Loc.Longitude = Convert.ToDecimal(Check.Result.Split(',')[1]);
							}
						} else if (c == 22) {
							// Column W: Client
							var Check = NonRequiredString(pCell, 50);

							if (Check.InError)
								RowErrors.AddRange(Check.Errors);
							else
								staged.ClientName = Check.Result;
						} else if (c == 23) {
							// Column X: Notes
							var Check = NonRequiredString(pCell, 50);

							if (Check.InError)
								RowErrors.AddRange(Check.Errors);
							else
								staged.ClientName = Check.Result; // notes
						}
					} // foreach of the Columns

					var CommentCell = row.GetCell(0) ?? row.CreateCell(0);

					if (RowErrors.Count > 0) {
						CommentCell.SetCellValue($"{RowErrors.Count}: {string.Join(", ", RowErrors.Select(x => $"[{ColumnIndexToLetter(x.Cell.Column + 1)}] {x.Message}"))}");

						foreach (var error in RowErrors) {
							var cell = error.Cell.Cell == null ? row.CreateCell(error.Cell.Column) : row.GetCell(error.Cell.Column);
							cell.CellStyle = IsError;
						}
					} else {
						Records.Add(staged); // Only add the record if the row is valid
						CommentCell.SetCellValue("OK");
					}

					Errors.AddRange(RowErrors);
				} // foreach of the Rows
			} catch (Exception e) {
				Console.WriteLine($"Exceiption: {e.Message} - {e.StackTrace}");
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

			return File(TheStream, "application/vnd.ms-excel", "Testing.xlsx");
		}

		private string CellValue(ParsingCell cell) {
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

		private ParsingError AddCellError(ParsingCell cell, string message) {
			return new ParsingError {
				Cell = cell,
				Message = message,
			};
		}

		private ParseResult RequiredDigitsOnly(ParsingCell cell, int maxLength = 0, int minLength = 0) {
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

		private ParseResult NonRequiredDigitsOnly(ParsingCell cell, int maxLength = 0, int minLength = 0) {
			var input = CellValue(cell);
			var Result = new ParseResult();

			if ((maxLength > 0) && (input.Length > maxLength))
				Result.Errors.Add(AddCellError(cell, $"Input string exceeds the maximum length of {maxLength}"));
			else if ((minLength > 0) && (input.Length < minLength) && (input.Length > 0))
				Result.Errors.Add(AddCellError(cell, $"Input string must be at least {minLength} characters"));

			if (!input.All(c => ((c >= '0') && (c <= '9'))))
				Result.Errors.Add(AddCellError(cell, $"Input string contains characters other than digits"));

			if (Result.InError == false)
				Result.Result = input;

			return Result;
		}

		private ParseResult DelimetedDigitsOnly(ParsingCell cell, char delimeter, int maxLength = 0, int minLength = 0, bool truncateSpaces = false) {
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
				Result.Result = input;

			return Result;
		}

		private ParseResult DelimetedDigitsOnly(ParsingCell cell, List<char> delimeters, int maxLength = 0, int minLength = 0, bool truncateSpaces = false) {
			// As of now, none of the Delimeted fields are Required
			var input = CellValue(cell);
			var Result = new ParseResult();

			input = truncateSpaces ? input.Replace(" ", "") : input;

			if ((maxLength > 0) && (input.Length > maxLength))
				Result.Errors.Add(AddCellError(cell, $"Input string exceeds the maximum length of {maxLength}"));
			else if ((minLength > 0) && (input.Length < minLength) && (input.Length > 0))
				Result.Errors.Add(AddCellError(cell, $"Input string must be at least {minLength} characters"));

			if (!input.All(c => ((c >= '0') && (c <= '9')) || (delimeters.Contains(c))))
				Result.Errors.Add(AddCellError(cell, $"Input string contains characters other than digits and any of the desired delimeters"));

			if (Result.InError == false)
				Result.Result = input;

			return Result;
		}

		private ParseResult RequiredString(ParsingCell cell, int maxLength = 0, int minLength = 0) {
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

		private ParseResult NonRequiredString(ParsingCell cell, int maxLength = 0, int minLength = 0) {
			var input = CellValue(cell);
			var Result = new ParseResult();

			if ((maxLength > 0) && (input.Length > maxLength))
				Result.Errors.Add(AddCellError(cell, $"Input string exceeds the maximum length of {maxLength}"));
			else if ((minLength > 0) && (input.Length < minLength))
				Result.Errors.Add(AddCellError(cell, $"Input string must be at least {minLength} characters"));

			if (Result.InError == false)
				Result.Result = input;

			return Result;
		}

		private ParseResult RequiredDate(ParsingCell cell) {
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



		private static string ColumnIndexToLetter(int colIndex) {
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

		private static ICellStyle CreateBoldStyle(IWorkbook workbook) {
			var Result = workbook.CreateCellStyle();
			var Font = workbook.CreateFont();

			Font.Boldweight = (short)FontBoldWeight.Bold;
			Result.SetFont(Font);
			Result.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");

			return Result;
		}

		private static ICellStyle CreateTopAlignmentStyle(IWorkbook workbook) {
			var Result = workbook.CreateCellStyle();

			Result.VerticalAlignment = VerticalAlignment.Top;

			return Result;
		}

		private static ICellStyle CreateBlueStyle(IWorkbook workbook) {
			var Result = workbook.CreateCellStyle();
			Result.FillForegroundColor = HSSFColor.Automatic.Index;
			Result.FillPattern = FillPattern.NoFill;

			var UniversalFont = workbook.CreateFont();
			UniversalFont.Color = 21;
			Result.SetFont(UniversalFont);

			return Result;
		}

		private static ICellStyle CreateErrorStyle(IWorkbook workbook) {
			var Result = workbook.CreateCellStyle();
			Result.FillForegroundColor = HSSFColor.Red.Index;
			Result.FillPattern = FillPattern.ThickBackwardDiagonals;

			return Result;
		}

		private static ICellStyle CreateValidationStyle(IWorkbook workbook) {
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

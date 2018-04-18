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

namespace PLSO2018.Controllers {

	[Produces("application/json")]
	[Route("api/Excel")]
	public class ExcelController : BaseController {

		private ExcelTemplateRepo excelTemplateRepo;
		private ICellStyle IsBold;

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

				int RowIndex = 0;

				IRow row = sheet1.CreateRow(RowIndex++);

				foreach (var col in Columns.Result) {
					var Cell = row.CreateCell(col.ColumnIndex - 1);

					if (col.Validation.StartsWith("REQUIRED:"))
						Cell.CellStyle = IsBold;

					Cell.SetCellType(CellType.String);
					Cell.SetCellValue(col.DisplayName);
				} // foreach of the columns on the Display Name row

				row = sheet1.CreateRow(RowIndex++);

				foreach (var col in Columns.Result) {
					var Cell = row.CreateCell(col.ColumnIndex - 1);
					Cell.SetCellType(CellType.String);
					Cell.SetCellValue(col.ExampleData);
				} // foreach of the columns on the Example Data row

				row = sheet1.CreateRow(RowIndex++);

				foreach (var col in Columns.Result) {
					var Cell = row.CreateCell(col.ColumnIndex - 1);
					Cell.SetCellType(CellType.String);
					Cell.SetCellValue(col.Validation);
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


		private static ICellStyle CreateBoldStyle(IWorkbook workbook) {
			var Result = workbook.CreateCellStyle();

			var Font = workbook.CreateFont();
			Font.Boldweight = (short)FontBoldWeight.Bold;
			Result.SetFont(Font);
			Result.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");

			return Result;
		}

	}
}

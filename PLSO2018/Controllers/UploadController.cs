using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using PLSO2018.Website.Controllers;

namespace PLSO2018.Controllers {

	public class UploadController : BaseController {

		private readonly ExcelController excelController;

		public UploadController(ExcelController excelController) {
			this.excelController = excelController;
		}

		public IActionResult Upload() {
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Post(List<IFormFile> files) {
			var size = files.Sum(f => f.Length);
			var filePath = Path.GetRandomFileName();
			string firstCell = "";

			foreach (var formFile in files) {
				if (formFile.Length > 0) {
					using (var stream = new MemoryStream()) {
						await formFile.CopyToAsync(stream);
						var bob = excelController.ProcessExcelFile(stream);
						return bob; // File(bob, "application/vnd.ms-excel", "Testing.xlsx");
					}
				}
			}

			return Ok(new { count = files.Count, size, filePath, firstCell });
		}

	}
}

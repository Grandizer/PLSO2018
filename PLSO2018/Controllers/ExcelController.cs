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

namespace PLSO2018.Controllers {

	[Produces("application/json")]
	[Route("api/Excel")]
	public class ExcelController : BaseController {

		public ExcelController(ILoggerFactory loggerFactory) {
			base.logger = loggerFactory.CreateLogger<ExcelController>();
		}

		public IActionResult GenerateBlankTemplate() {
			var TheStream = new NPOIMemoryStream {
				AllowClose = false
			};

			var FileName = $"PLSO_Upload_Template {DateTime.Now.ToString("MM-dd-yyyy")}-{DateTime.Now.Ticks}.xlsx";

			return File(TheStream, "application/vnd.ms-excel", FileName);
		}

	}
}

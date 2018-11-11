using DataContext.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PLSO2018.Website.Controllers;
using PLSO2018.Website.Models.API;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PLSO2018.Controllers.API {

	[Route("api/records")]
	[ApiController]
	public class RecordsController : BaseController {

		private readonly RecordRepo recordRepo;

		public RecordsController(RecordRepo recordRepo, IHttpContextAccessor contextAccessor, ILoggerFactory loggerFactory) : base(contextAccessor) {
			this.recordRepo = recordRepo;
			base.logger = loggerFactory.CreateLogger<RecordsController>();
		}

		[HttpPost]
		[Route("approve")]
		public async Task<IActionResult> ApproveRecordsAsync([FromBody] RecordApprovalModel model) {
			var Result = await recordRepo.ApproveRecords(model.IDs);

			if (Result.WasSuccessful)
				return Ok(Result);
			else
				return Ok(Result);
		}

	}
}

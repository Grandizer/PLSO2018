using DataContext.Repositories;
using Microsoft.AspNetCore.Mvc;
using PLSO2018.Entities;
using PLSO2018.Website.Controllers;
using PLSO2018.Website.Models;
using System.Threading.Tasks;

namespace PLSO2018.Controllers {

	public class RecordController : BaseController {

		private readonly RecordRepo recordRepo;

		public RecordController(RecordRepo recordRepo) {
			this.recordRepo = recordRepo;
		}

		public IActionResult Index() {
			return View();
		}

		public async Task<IActionResult> AwaitingApproval() {
			var Result = new RecordApprovalModel();
			var Records = await recordRepo.GetRecordsAwaitingApproval();

			if (Records.WasSuccessful)
				Result.Records = Records.Result;

			return View(Result);
		}

		public async Task<IActionResult> Edit(int id)
		{
			var Result = new EditRecordModel();
			var Response = await recordRepo.GetRecordById(id);

			if (Response.WasSuccessful)
				Result = new EditRecordModel(Response.Result);

			return View(Result);
		}

	}
}

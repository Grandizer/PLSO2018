using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PLSO2018.Entities;
using PLSO2018.Entities.Common;
using PLSO2018.Entities.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Repositories {

	public class RecordRepo : BaseRepo {

		int UserID = 0;

		public RecordRepo(PLSODb context, IHttpContextAccessor contextAccessor, ILoggerFactory loggerFactory) : base(context, contextAccessor) {
			logger = loggerFactory.CreateLogger<ExcelTemplateRepo>();

			// TODO: The following can/should be put in a common method like the BaseController does
			var user = contextAccessor.HttpContext.User;

			UserID = user.HasClaim(x => x.Type == Constants.Claims.User.ID) ? Convert.ToInt32(user.FindFirst(Constants.Claims.User.ID).Value) : 0;
		}

		public async Task<PLSOResponse<List<Record>>> GetRecordsAwaitingApproval() {
			var Result = new PLSOResponse<List<Record>>();

			try {
				Result.Result = await DataContext.Records
					.Where(x => x.Approved == false)
					.ToListAsync();

				Result.WasSuccessful = true;
			} catch (Exception e) {
				Result.AddMessage(e, nameof(GetRecordsAwaitingApproval));
				logger.LogError(1, e, "Unable to gather records awaiting approval");
			}

			return Result;
		}

		public async Task<PLSOResponse> ApproveRecords(List<int> ids) {
			var Result = new PLSOResponse();

			try {
				foreach (var id in ids) {
					Record r = await DataContext.Records.FindAsync(id);
					r.Approved = true;
					r.Active = true;
				}

				DataContext.SaveChanges(UserID);

				Result.WasSuccessful = true;
			} catch (Exception e) {
				Result.AddMessage(e, nameof(ApproveRecords));
				logger.LogError(2, e, "Unable to approve record(s)");
			}

			return Result;
		}

	}
}

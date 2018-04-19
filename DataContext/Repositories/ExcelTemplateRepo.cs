using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PLSO2018.Entities;
using PLSO2018.Entities.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataContext.Repositories {

	public class ExcelTemplateRepo : BaseRepo {

		// TODO: Column E (Record.Township) is Required
		// Then at least one of the following 4 columns are required as well, but not on the object.  Must be business logic.
		// Record.OriginalLot, Record.Section, Record.Tract or Record.Range

		public ExcelTemplateRepo(PLSODb context, IHttpContextAccessor contextAccessor, ILoggerFactory loggerFactory) : base(context, contextAccessor) {
			logger = loggerFactory.CreateLogger<ExcelTemplateRepo>();
		}

		public async Task<PLSOResponse<List<ExcelTemplate>>> GetTemplateColumnsAsync() {
			var Result = new PLSOResponse<List<ExcelTemplate>>();

			try {
				Result.Result = await DataContext.ExcelTemplates
					.OrderBy(x => x.ColumnIndex)
					.ToListAsync();

				Result.WasSuccessful = true;
			} catch (Exception e) {
				Result.AddMessage(e, nameof(GetTemplateColumnsAsync));
				logger.LogError(1, e, "Unable to gather ExcelTemplate records");
			}

			return Result;
		}

	}
}

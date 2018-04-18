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

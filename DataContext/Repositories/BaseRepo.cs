using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DataContext.Repositories {

	public class BaseRepo {

		// NOTE: Do not forget to add the following line to the Startup.ConfigureServices method:
		// services.AddScoped(typeof(xxxRepo));

		internal PLSODb DataContext;
		internal ILogger logger;
		internal IHttpContextAccessor contextAccessor;

		public BaseRepo() { }

		public BaseRepo(PLSODb context, IHttpContextAccessor contextAccessor) {
			this.DataContext = context;
			this.contextAccessor = contextAccessor;
		}

	}
}

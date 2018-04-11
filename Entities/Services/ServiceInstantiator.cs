using System;

namespace PLSO2018.Entities.Services {

	public static class ServiceInstantiator {

		// Note: When you HAVE to use this, you will need the following using statement in order for it to work:
		// using Microsoft.Extensions.DependencyInjection;
		// And then your code would be:
		// var lf = ServiceInstantiator.Instance.GetService<ILoggerFactory>();

		public static IServiceProvider Instance { get; set; }

	}
}

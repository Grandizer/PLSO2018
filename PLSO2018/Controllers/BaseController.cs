using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PLSO2018.Entities.Common;
using System;
using System.Security.Claims;

namespace PLSO2018.Website.Controllers {

	public class BaseController : Controller {

		internal ILogger logger;

		public BaseController() {

		}

		public string UsersFirstName => User.HasClaim(x => x.Type == Constants.Claims.User.FirstName) ? User.FindFirstValue(Constants.Claims.User.FirstName) : "";
		public string UsersLastName => User.HasClaim(x => x.Type == Constants.Claims.User.LastName) ? User.FindFirstValue(Constants.Claims.User.LastName) : "";
		public int UsersID => User.HasClaim(x => x.Type == Constants.Claims.User.ID) ? Convert.ToInt32(User.FindFirst(Constants.Claims.User.ID).Value) : 0;
		public string UsersEMail => User.HasClaim(x => x.Type == Constants.Claims.User.EMail) ? User.FindFirstValue(Constants.Claims.User.EMail) : "";

	}
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PLSO2018.Entities.Common;
using System;
using System.Security.Claims;

namespace PLSO2018.Website.Controllers {

	public class BaseController : Controller {

		internal ILogger logger;
		internal IHttpContextAccessor contextAccessor;
		private string _UsersFirstName = null;
		private string _UsersLastName = null;
		private int _UsersID = 0;
		private string _UsersEMail = null;

		public BaseController() {

		}

		public BaseController(IHttpContextAccessor contextAccessor) {
			this.contextAccessor = contextAccessor;
			SetCommonValues();
		}

		private void SetCommonValues() {
			var user = (User ?? contextAccessor.HttpContext.User);

			_UsersFirstName = user.HasClaim(x => x.Type == Constants.Claims.User.FirstName) ? user.FindFirstValue(Constants.Claims.User.FirstName) : "";
			_UsersLastName = user.HasClaim(x => x.Type == Constants.Claims.User.LastName) ? user.FindFirstValue(Constants.Claims.User.LastName) : "";
			_UsersID = user.HasClaim(x => x.Type == Constants.Claims.User.ID) ? Convert.ToInt32(user.FindFirst(Constants.Claims.User.ID).Value) : 0;
			_UsersEMail = user.HasClaim(x => x.Type == Constants.Claims.User.EMail) ? user.FindFirstValue(Constants.Claims.User.EMail) : "";
		}

		public string UsersFirstName {
			get {
				if (string.IsNullOrWhiteSpace(_UsersFirstName))
					throw new Exception("The UsersFirstName has not been set.");

				return _UsersFirstName;
			}
			set { _UsersFirstName = value; }
		}

		public string UsersLastName {
			get {
				if (string.IsNullOrWhiteSpace(_UsersLastName))
					throw new Exception("The UsersLastName has not been set.");

				return _UsersLastName;
			}
			set { _UsersLastName = value; }
		}

		public string UsersEMail {
			get {
				if (string.IsNullOrWhiteSpace(_UsersEMail))
					throw new Exception("The UsersEMail has not been set.");

				return _UsersEMail;
			}
			set { _UsersEMail = value; }
		}

		public int UsersID {
			get {
				if (_UsersID <= 0)
					throw new Exception("The UsersID has not been set.");

				return _UsersID;
			}
			set { _UsersID = value; }
		}

	}
}

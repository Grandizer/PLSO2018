using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace PLSO2018.Entities.Services {

	public class UserResolver {

		private readonly IHttpContextAccessor contextAccessor;

		public UserResolver(IHttpContextAccessor contextAccessor) {
			this.contextAccessor = contextAccessor;
		}

		public ClaimsPrincipal User => contextAccessor.HttpContext.User;

		// TODO: Create a Constants in Entities and put the Claims name in there
		public int UsersID => User.HasClaim(x => x.Type == "") ? Convert.ToInt32(User.FindFirstValue("")) : 0;

	}
}

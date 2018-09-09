using DataContext;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using PLSO2018.Entities.Common;

namespace PLSO2018.Website.Services {

	// This helped a bit to get here: https://www.stevejgordon.co.uk/extending-the-asp-net-core-identity-signinmanager
	// But this was the key: http://stackoverflow.com/a/40791230/373438

	public class PLSOSignInManager<TUser> : SignInManager<TUser> where TUser : class {

		private readonly UserManager<TUser> _userManager;
		private readonly IHttpContextAccessor _contextAccessor;
		private readonly PLSODb _dataContext;

		public PLSOSignInManager(
			UserManager<TUser> userManager,
			IHttpContextAccessor contextAccessor,
			IUserClaimsPrincipalFactory<TUser> claimsFactory,
			IOptions<IdentityOptions> optionsAccessor,
			ILogger<SignInManager<TUser>> logger,
			IAuthenticationSchemeProvider authenticationSchemeProvider,
			PLSODb dataContext)
			: base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, authenticationSchemeProvider) {
			_userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			_contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
			_dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
		}

		public override async Task<ClaimsPrincipal> CreateUserPrincipalAsync(TUser user) {
			var Principal = await base.CreateUserPrincipalAsync(user);
			var Identity = (ClaimsIdentity)Principal.Identity;

			// How To: Here is where you can add Custom/On the fly Claims
			// They would not be persisted in the database but are within the Cookie

			var User = _dataContext.AspNetUsers
				.Include(x => x.Claims)
				.Where(x => x.Email == Identity.Name)
				.SingleOrDefault();

			if (User != null) {
				Identity.AddClaim(new Claim(Constants.Claims.User.ID, User.Id.ToString()));
				//Identity.AddClaim(new Claim(Constants.Claims.User.Number, User.Number));
				Identity.AddClaim(new Claim(Constants.Claims.User.FirstName, User.FirstName));
				Identity.AddClaim(new Claim(Constants.Claims.User.LastName, User.LastName));
				Identity.AddClaim(new Claim(Constants.Claims.User.EMail, User.Email));
				//Identity.AddClaim(new Claim(Constants.Claims.User.TimeZoneID, User.TimeZoneID.ToString()));
				//Identity.AddClaim(new Claim(Constants.Claims.User.TimeZoneLookupKey, User.TimeZone.LookupKey));
				//Identity.AddClaim(new Claim(Constants.Claims.Tenant.ID, User.TenantID.ToString()));
				//Identity.AddClaim(new Claim(Constants.Claims.Tenant.Name, User?.Tenant.Name));
				//Identity.AddClaim(new Claim(Constants.Claims.Subscriber.ID, User?.Tenant.SubscriberID.ToString()));
				//Identity.AddClaim(new Claim(Constants.Claims.Subscriber.Name, User?.Tenant?.Subscriber.Name));
				//Identity.AddClaim(new Claim(Constants.Claims.Application.User, "true")); // Everyone is a User within the App

				foreach (var rc in User.Claims) {
					Identity.AddClaim(rc.ToClaim()); // Add the Claim normally
				} // foreach of the Claims that have been specifically assinged to me
			} // if we found the associated User record

			return Principal;
		}

	}
}

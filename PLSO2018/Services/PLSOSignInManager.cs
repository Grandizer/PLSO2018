
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
				//Identity.AddClaim(new Claim(Constants.Claims.User.ID, User.Id.ToString()));
				//Identity.AddClaim(new Claim(Constants.Claims.User.Number, User.Number));
				//Identity.AddClaim(new Claim(Constants.Claims.User.FirstName, User.FirstName));
				//Identity.AddClaim(new Claim(Constants.Claims.User.LastName, User.LastName));
				//Identity.AddClaim(new Claim(Constants.Claims.User.EMail, User.Email));
				//Identity.AddClaim(new Claim(Constants.Claims.User.TimeZoneID, User.TimeZoneID.ToString()));
				//Identity.AddClaim(new Claim(Constants.Claims.User.TimeZoneLookupKey, User.TimeZone.LookupKey));
				//Identity.AddClaim(new Claim(Constants.Claims.Tenant.ID, User.TenantID.ToString()));
				//Identity.AddClaim(new Claim(Constants.Claims.Tenant.Name, User?.Tenant.Name));
				//Identity.AddClaim(new Claim(Constants.Claims.Subscriber.ID, User?.Tenant.SubscriberID.ToString()));
				//Identity.AddClaim(new Claim(Constants.Claims.Subscriber.Name, User?.Tenant?.Subscriber.Name));
				//Identity.AddClaim(new Claim(Constants.Claims.Application.User, "true")); // Everyone is a User within the App

				foreach (var rc in User.Claims) {
					Identity.AddClaim(rc.ToClaim()); // Add the Claim normally

					//if ((rc.ClaimType == Constants.Claims.Subscriber.IsAdministrator) ||
					//	(rc.ClaimType == Constants.Claims.Subscriber.IsTimeSheetAdmin) ||
					//	(rc.ClaimType == Constants.Claims.Subscriber.IsTimeSheetReview)) {
					//	var mySubscriptionID = User.Tenant.Subscriber.ID;
					//	var myTenants = _dataContext.Tenants.Where(x => x.SubscriberID == mySubscriptionID).ToList();

					//	foreach (var t in myTenants) {
					//		if (rc.ClaimType == Constants.Claims.Subscriber.IsAdministrator)
					//			Identity.AddClaim(new Claim(Constants.Claims.Tenant.AdministratorOf, t.ID.ToString()));
					//		else if (rc.ClaimType == Constants.Claims.Subscriber.IsTimeSheetAdmin)
					//			Identity.AddClaim(new Claim(Constants.Claims.Tenant.TimeSheetAdminOf, t.ID.ToString()));
					//		else if (rc.ClaimType == Constants.Claims.Subscriber.IsTimeSheetReview)
					//			Identity.AddClaim(new Claim(Constants.Claims.Tenant.TimeSheetReviewOf, t.ID.ToString()));
					//	} // foreach of the Tenants within the Subscription
					//} // if I am the Administrator of a Subscription, I should also get the Tenants as well
				} // foreach of the Claims that have been specifically assinged to me

				// Add Claims for each of the Projects I am a Manager of or a Member of
				//var PM = _dataContext.ProjectRoles.Where(x => x.Name == Constants.ProjectRoleTypes.ProjectManager).FirstOrDefault();

				//if (PM != null) {
				//	// Get the Projects where I am the Project Manager
				//	var MyPMProjects = _dataContext.ProjectProjectRoleUsers.Where(x => x.UserID == User.Id && x.ProjectRoleID == PM.ID).ToList();

				//	foreach (var Project in MyPMProjects)
				//		Identity.AddClaim(new Claim(Constants.Claims.Project.ManagerOf, Project.ProjectID.ToString()));

				//	if (MyPMProjects.Count > 0)
				//		Identity.AddClaim(new Claim(Constants.Claims.Project.IsManager, "true"));

				//	// Get the Projects that I am ON but am NOT the Project Manager
				//	var MyProjects = _dataContext.ProjectProjectRoleUsers.Where(x => x.UserID == User.Id && x.ProjectRoleID != PM.ID).ToList();

				//	foreach (var Project in MyProjects)
				//		Identity.AddClaim(new Claim(Constants.Claims.Project.MemberOf, Project.ProjectID.ToString()));
				//} // if we found the Project Manager Role
			} // if we found the associated User record

			return Principal;
		}

	}
}

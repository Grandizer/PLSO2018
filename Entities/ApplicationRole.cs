using Microsoft.AspNetCore.Identity;

namespace PLSO2018.Entities {

	public class ApplicationRole : IdentityRole<int> {

		public ApplicationRole() { }

		public ApplicationRole(string roleName) : base(roleName) {
			base.Name = Name;
		}

	}
}

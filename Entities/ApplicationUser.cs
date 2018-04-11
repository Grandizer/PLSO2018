using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace PLSO2018.Entities {

	public class ApplicationUser : IdentityUser<int> {

		public string FirstName { get; set; }
		public string MiddleInitial { get; set; }
		public string LastName { get; set; }
		public string SuffixShort { get; set; }
		public int Number { get; set; }
		public DateTimeOffset LastActivityDate { get; set; }
		public string Suffix { get; set; }
		public bool IsActive { get; set; }

		public List<IdentityUserClaim<int>> Claims { get; set; }

		public List<Address> Addresses { get; set; }
		public List<Email> Emails { get; set; }
		public List<Phone> Phones { get; set; }

	}
}

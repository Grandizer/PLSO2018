using PLSO2018.Entities.Support;

namespace PLSO2018.Entities {

	public class Address : AuditableBase {

		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public string CompanyName { get; set; }
		public bool IsPrimary { get; set; }
		public EmailType AddressType { get; set; }
		public int AddressTypeID { get; set; }

	}
}

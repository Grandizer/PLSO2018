using PLSO2018.Entities.Support;

namespace PLSO2018.Entities {

	public class Email : AuditableBase {

		public string Address { get; set; }
		public bool IsPrimary { get; set; }
		public EmailType EmailType { get; set; }
		public int EmailTypeID { get; set; }

	}
}

using System;

namespace PLSO2018.Entities {

	public class Audit {

		public long ID { get; set; }
		public long EntityID { get; set; }
		public string EntityName { get; set; }

		public AuditAction AuditAction { get; set; }
		public int AuditActionID { get; set; }

		public string Data { get; set; }
		public DateTimeOffset CreationDate { get; set; } // Utc
		public long CreatedByID { get; set; }

	}
}

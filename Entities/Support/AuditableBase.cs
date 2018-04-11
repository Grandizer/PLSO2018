using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PLSO2018.Entities.Support {

	public class AuditableBase {

		[Column(Order = 0)]
		public int ID { get; set; }

		[Required]
		public DateTimeOffset CreationDate { get; set; } // Utc

		[Required]
		public int CreatedByID { get; set; }

	}
}

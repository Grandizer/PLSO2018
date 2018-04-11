using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PLSO2018.Entities {

	public class AuditAction {

		[Column(Order = 0)]
		[Key]
		public int ID { get; set; }

		[Required]
		[Column(Order = 1)]
		public bool IsActive { get; set; }

		[Required]
		[Column(Order = 2, TypeName = "varchar(30)")]
		public string EnumName { get; set; } // This is the EXACT name used in a local Enum

		[Required]
		[Column(Order = 3, TypeName = "varchar(50)")]
		public string Name { get; set; } // This is a user friendly name with spaces

		[Column(Order = 4, TypeName = "varchar(250)")]
		public string Description { get; set; }

		[Required]
		public DateTimeOffset CreationDate { get; set; }

		[Required]
		public long CreatedByID { get; set; }

	}
}

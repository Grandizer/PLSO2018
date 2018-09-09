using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PLSO2018.Entities.Common {

	public class EnumTriplet {

		[Key]
		public int ID { get; set; }

		[Column(TypeName = "varchar(50)")]
		public string EnumerationName { get; set; }

		[Column(TypeName = "varchar(30)")]
		public string KeyName { get; set; }

		[Column(TypeName = "varchar(255)")]
		public string DisplayName { get; set; }

		[Column(TypeName = "int")]
		public int Value { get; set; }

	}
}

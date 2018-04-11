using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PLSO2018.Entities.Support {

	public class TemporalBase {

		[Column(Order = 0)]
		public int ID { get; set; }

		[Required]
		public int ModifieddByID { get; set; }

	}
}

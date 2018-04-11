using PLSO2018.Entities.Support;

namespace PLSO2018.Entities {

	public class Location : AuditableBase {

		public decimal Latitude { get; set; }
		public decimal Longitude { get; set; }
		public string Address { get; set; }
		public LocationType LocationType { get; set; }
		public int LocationTypeID { get; set; }

	}
}

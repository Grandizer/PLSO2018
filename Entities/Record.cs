using PLSO2018.Entities.Support;

namespace PLSO2018.Entities {

	public class Record : TemporalBase {

		public string ImageFileName { get; set; }
		public string Township { get; set; }
		public string County { get; set; }
		public string City { get; set; }
		public string Street { get; set; }
		public string Tract { get; set; }
		public string OriginalLot { get; set; }
		public string Subdivision { get; set; }
		public string Sublot { get; set; }
		public string DeedVolume { get; set; }
		public string DeedPage { get; set; }
		public string ParcelNumber { get; set; }
		public string Description { get; set; }
		public string RecordingInfo { get; set; }
		public decimal SurveyYear { get; set; }
		public ApplicationUser Surveyor { get; set; }
		public int SurveyorID { get; set; }
		public SurveyType SurveyType { get; set; }
		public int SurveyTypeID { get; set; }
		public Plat Plat { get; set; }
		public int PlatID { get; set; }
		public Location Location { get; set; }
		public int LocationID { get; set; }
		public int MyProperty { get; set; }

	}
}

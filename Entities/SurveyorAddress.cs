namespace PLSO2018.Entities {

	public class SurveyorAddress {

		public int ID { get; set; }

		public ApplicationUser Surveyor { get; set; }
		public int SurveyorID { get; set; }
		public Address Address { get; set; }
		public int AddressID { get; set; }

	}
}

namespace PLSO2018.Entities {

	public class SurveyorEmail {

		public int ID { get; set; }

		public ApplicationUser Surveyor { get; set; }
		public int SurveyorID { get; set; }
		public Email Email { get; set; }
		public int EmailID { get; set; }

	}
}

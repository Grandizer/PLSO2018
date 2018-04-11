namespace PLSO2018.Entities {

	public class SurveyorPhone {

		public int ID { get; set; }

		public ApplicationUser Surveyor { get; set; }
		public int SurveyorID { get; set; }
		public Phone Phone { get; set; }
		public int PhoneID { get; set; }

	}
}

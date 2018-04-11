using System;

namespace PLSO2018.Entities {

	public class UserLogon {

		public int ID { get; set; }
		public ApplicationUser Surveyor { get; set; }
		public int SurveyorID { get; set; }
		public DateTimeOffset LoggedIn { get; set; }
		public string RemoteAddress { get; set; }
		public string LocalAddress { get; set; }
		public string HttpUserAgent { get; set; }
		public DateTimeOffset LoggedOff { get; set; }
		public int LoggedOffByID { get; set; }
		public int LoggedOffTypeID { get; set; }

	}
}

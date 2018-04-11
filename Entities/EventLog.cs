using System;

namespace PLSO2018.Entities {

	public class EventLog {

		public int ID { get; set; }
		public int? SurveyorID { get; set; }
		public int? EventID { get; set; }
		public string LogLevel { get; set; }
		public string Message { get; set; }
		public string StackTrace { get; set; }
		public string Page { get; set; }
		public DateTimeOffset Occurred { get; set; }

	}
}

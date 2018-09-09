using System.Collections.Generic;

namespace PLSO2018.Entities.Services {

	public class SourceTablePairing {

		public string TableName { get; set; }
		public string EnumerationName { get; set; }
		public List<string> Names { get; set; } = new List<string>();

	}
}

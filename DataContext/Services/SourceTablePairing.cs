using System.Collections.Generic;

namespace PLSO2018.DataContext.Services {

	public class SourceTablePairing {

		public string TableName { get; set; }
		public string EnumerationName { get; set; }
		public List<string> Names { get; set; } = new List<string>();

	}
}

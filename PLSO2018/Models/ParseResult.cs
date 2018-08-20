using System.Collections.Generic;

namespace PLSO2018.Website.Models {

	public class ParseResult {

		public List<ParsingError> Errors { get; set; } = new List<ParsingError>();
		public string Result { get; set; }

		public bool InError => Errors.Count > 0;

	}
}

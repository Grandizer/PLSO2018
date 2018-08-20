namespace PLSO2018.Website.Models {

	public class ParsingError {

		public string Message { get; set; }
		public ParsingCell Cell { get; set; }

		public ParsingError(ParsingCell cell, string message) {
			this.Cell = cell;
			this.Message = message;
		}

		public ParsingError() {

		}

	}
}

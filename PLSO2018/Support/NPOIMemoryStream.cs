using System.IO;

namespace PLSO2018.Website.Support {

	public class NPOIMemoryStream : MemoryStream {
		// https://stackoverflow.com/a/37398007/373438

		public NPOIMemoryStream() {
			AllowClose = true;
		}

		public bool AllowClose { get; set; }

		public override void Close() {
			if (AllowClose)
				base.Close();
		}

	}
}

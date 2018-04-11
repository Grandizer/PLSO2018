using PLSO2018.Entities.Support;

namespace PLSO2018.Entities {

	public class ImagePath : AuditableBase {

		public string Name { get; set; }
		public string ServerPath { get; set; }
		public string RelativeToRoot { get; set; }
		public bool IsDefault { get; set; }

	}
}

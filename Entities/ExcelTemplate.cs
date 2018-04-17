using PLSO2018.Entities.Support;

namespace PLSO2018.Entities {

	public class ExcelTemplate : TemporalBase {

		public int ColumnIndex { get; set; }
		public string DisplayName { get; set; }
		public string FieldName { get; set; } // This is the corresponding Record.xxx field name
		public string ExampleData { get; set; }
		public string Validation { get; set; }

	}
}

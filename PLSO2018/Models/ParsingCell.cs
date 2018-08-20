using NPOI.SS.UserModel;

namespace PLSO2018.Website.Models {

	public class ParsingCell {

		public int Row { get; set; }
		public int Column { get; set; }
		public ICell Cell { get; set; }

		public ParsingCell(ICell cell, int row, int column) {
			this.Row = row;
			this.Column = column;
			this.Cell = cell;
		}

	}
}

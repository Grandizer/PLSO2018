using PLSO2018.Entities.Support;
using System;

namespace PLSO2018.Entities {

	public class Plat : TemporalBase {

		public string Volumne { get; set; }
		public int Page { get; set; }
		public string Name { get; set; }
		public DateTime Date { get; set; }

	}
}

using PLSO2018.Entities.Support;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLSO2018.Entities {

	public class Phone : AuditableBase {

		public string Number { get; set; }
		public string Extension { get; set; }
		public bool IsPrimary { get; set; }
		public PhoneType PhoneType { get; set; }
		public int PhoneTypeID { get; set; }

	}
}

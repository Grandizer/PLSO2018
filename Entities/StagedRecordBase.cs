﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PLSO2018.Entities {

	public class StagedRecord : StagedRecordBase {

		// Yes, this is the same structre as AuditableBase but I do not now these to get Audited

		[Column(Order = 0)]
		public int ID { get; set; }
		[Required]
		public DateTimeOffset CreationDate { get; set; } // Utc
		[Required]
		public int CreatedByID { get; set; }

	}

	public class StagedRecordBase {

		// NOTE - If you add/remove fields from there PLEASE Update the Record as well

		public string AutomatedFileNumber { get; set; }
		public string State { get; set; } = "Ohio";
		public string City { get; set; }
		public string ClientName { get; set; }
		public string County { get; set; }
		public string CrossStreet { get; set; }
		public int? DeedPage { get; set; }
		public int? DeedVolume { get; set; }
		public string Description { get; set; }
		public string ImageFileName { get; set; }
		public Location Location { get; set; }
		public int LocationID { get; set; }
		public string OriginalLot { get; set; }
		public string ParcelNumber { get; set; }
		public string Section { get; set; }
		public string Address { get; set; }
		public string Subdivision { get; set; }
		public string Sublot { get; set; }
		public DateTime SurveyDate { get; set; }
		public string SurveyName { get; set; }
		public ApplicationUser Surveyor { get; set; }
		public int SurveyorID { get; set; }
		public string Township { get; set; }
		public string Tract { get; set; }
		public string Range { get; set; }

	}
}
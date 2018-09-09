using PLSO2018.Entities.Support;
using System;

namespace PLSO2018.Entities {

	public class Record : TemporalBase {

		// Map Image Name -- City, Village, Township -- County -- Defunct or Historic Township -- 
		// Lot No. -- Section -- Tract -- Range -- Survey Date -- Surveyor Name -- Surveyor Number -- 
		// Address -- Cross Street Name -- Parcel Numbers -- Volume -- Page -- AFN/Inst#/Recorder#/Doc# -- 
		// Subdivision -- Subdivision-Sublot -- Survey Name -- Location -- Client -- Notes
		public string MapImageName { get; set; }
		public string CityVillageTownship { get; set; } // New
		public string State { get; set; } = "Ohio";
		public string County { get; set; }
		public string DefunctTownship { get; set; } // New
		public string LotNumbers { get; set; } // New
		public string Section { get; set; }
		public string Tract { get; set; }
		public string Range { get; set; }
		public DateTime SurveyDate { get; set; }
		public string SurveyorName { get; set; }
		public ApplicationUser Surveyor { get; set; }
		public string SurveyorNumber { get; set; }
		public int? SurveyorID { get; set; }
		public string Address { get; set; }
		public string CrossStreet { get; set; }
		public string ParcelNumbers { get; set; }
		public int? DeedPage { get; set; }
		public int? DeedVolume { get; set; }
		public string AutomatedFileNumber { get; set; }
		public string Subdivision { get; set; }
		public string Sublot { get; set; }
		public string SurveyName { get; set; }
		public Location Location { get; set; }
		public int? LocationID { get; set; }
		public string ClientName { get; set; }
		public string Notes { get; set; } // Was Description

		public string ImagePath { get; set; }
		public bool Active { get; set; }
		public int UploadedByID { get; set; }
		public DateTimeOffset UploadedDate { get; set; }
		public int HashCode { get; set; }

		public override int GetHashCode() {
			return $"{MapImageName}{CityVillageTownship}{State}{County}{DefunctTownship}{LotNumbers}{Section}{Tract}{Range}{SurveyDate.ToString()}{SurveyorName}{Address}{CrossStreet}{ParcelNumbers}{AutomatedFileNumber}{Subdivision}{Sublot}{SurveyName}{ClientName}{Notes}{(SurveyorID ?? 0)}{(LocationID ?? 0)}".GetHashCode();
		}

	}
}

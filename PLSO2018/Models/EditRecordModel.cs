using PLSO2018.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLSO2018.Website.Models
{
    public class EditRecordModel : Record
    {
        public EditRecordModel() { }
        public EditRecordModel(Record record)
        {
            this.Active = record.Active;
            this.Address = record.Address;
            this.Approved = record.Approved;
            this.AutomatedFileNumber = record.AutomatedFileNumber;
            this.CityVillageTownship = record.CityVillageTownship;
            this.ClientName = record.ClientName;
            this.County = record.County;
            this.CrossStreet = record.CrossStreet;
            this.DeedPage = record.DeedPage;
            this.DeedVolume = record.DeedVolume;
            this.DefunctTownship = record.DefunctTownship;
            this.ID = record.ID;
            this.ImagePath = record.ImagePath;
            this.Location = record.Location;
            this.LocationID = record.LocationID;
            this.LotNumbers = record.LotNumbers;
            this.MapImageName = record.MapImageName;
            this.Notes = record.Notes;
            this.ParcelNumbers = record.ParcelNumbers;
            this.Range = record.Range;
            this.Section = record.Section;
            this.State = record.State;
            this.Subdivision = record.Subdivision;
            this.Sublot = record.Sublot;
            this.SurveyDate = record.SurveyDate;
            this.SurveyName = record.SurveyName;
            this.Surveyor = record.Surveyor;
            this.SurveyorID = record.SurveyorID;
            this.SurveyorName = record.SurveyorName;
            this.SurveyorNumber = record.SurveyorNumber;
            this.Tract = record.Tract;
        }
        public int Something { get; set; }
    }
}

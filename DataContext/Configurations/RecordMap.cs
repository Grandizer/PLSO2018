using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSO2018.Entities;

namespace DataContext.Configurations {

	public class RecordMap : IEntityTypeConfiguration<Record> {

		public void Configure(EntityTypeBuilder<Record> builder) {
			builder.ToTable(nameof(Record), "data");
			builder.HasKey(x => x.ID);

			builder.Property(x => x.MapImageName)
				.HasMaxLength(25)
				.IsRequired()
				.IsUnicode(false);
			builder.Property(x => x.CityVillageTownship)
				.IsRequired()
				.HasMaxLength(30)
				.IsUnicode(false);
			builder.Property(x => x.State)
				.IsRequired()
				.HasMaxLength(30)
				.IsUnicode(false);
			builder.Property(x => x.County)
				.IsRequired()
				.HasMaxLength(25)
				.IsUnicode(false);
			builder.Property(x => x.DefunctTownship)
				.IsUnicode(false)
				.HasMaxLength(30)
				.IsRequired();
			builder.Property(x => x.LotNumbers)
				.HasMaxLength(25)
				.IsUnicode(false);
			builder.Property(x => x.Section)
				.HasMaxLength(25)
				.IsUnicode(false);
			builder.Property(x => x.Tract)
				.HasMaxLength(25)
				.IsUnicode(false);
			builder.Property(x => x.Range)
				.HasMaxLength(25)
				.IsUnicode(false);
			builder.Property(x => x.SurveyDate)
				.IsRequired();
			builder.Property(x => x.SurveyorID)
				.IsRequired();
			builder.Property(x => x.SurveyorNumber)
				.HasMaxLength(5)
				.IsUnicode(false);
			builder.Property(x => x.Address)
				.IsRequired()
				.HasMaxLength(100)
				.IsUnicode(false);
			builder.Property(x => x.CrossStreet)
				.HasMaxLength(30)
				.IsUnicode(false);
			builder.Property(x => x.ParcelNumbers)
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.AutomatedFileNumber)
				.HasMaxLength(18)
				.IsUnicode(false);
			builder.Property(x => x.Subdivision)
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.Sublot)
				.HasMaxLength(10)
				.IsUnicode(false);
			builder.Property(x => x.SurveyName)
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.ClientName)
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.Notes)
				.HasMaxLength(2000)
				.IsUnicode(false);

			builder.Property(x => x.MapImageName)
				.HasMaxLength(1000)
				.IsRequired()
				.IsUnicode(false);
			builder.Property(x => x.Active)
				.IsRequired();
			builder.Property(x => x.UploadedByID)
				.IsRequired();
			builder.Property(x => x.UploadedDate)
				.IsRequired();
		}

	}
}

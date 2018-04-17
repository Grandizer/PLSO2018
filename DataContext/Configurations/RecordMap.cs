using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSO2018.Entities;

namespace DataContext.Configurations {

	public class RecordMap : IEntityTypeConfiguration<Record> {

		public void Configure(EntityTypeBuilder<Record> builder) {
			builder.ToTable(nameof(Record), "data");
			builder.HasKey(x => x.ID);

			builder.Property(x => x.AutomatedFileNumber)
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.City)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.ClientName)
				.HasMaxLength(100)
				.IsUnicode(false);
			builder.Property(x => x.County)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.Description)
				.HasMaxLength(2000)
				.IsUnicode(false);
			builder.Property(x => x.ImageFileName)
				.HasMaxLength(255)
				.IsUnicode(false);
			builder.Property(x => x.OriginalLot)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.ParcelNumber)
				.HasMaxLength(255)
				.IsUnicode(false);
			builder.Property(x => x.RecordingInfo)
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.Section)
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.StreetName)
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.StreetNumber)
				.HasMaxLength(15)
				.IsUnicode(false);
			builder.Property(x => x.StreetSuffix)
				.HasMaxLength(4)
				.IsUnicode(false);
			builder.Property(x => x.Subdivision)
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.Sublot)
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.SurveyName)
				.HasMaxLength(100)
				.IsUnicode(false);
			builder.Property(x => x.Township)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.Tract)
				.HasMaxLength(50)
				.IsUnicode(false);
		}

	}
}

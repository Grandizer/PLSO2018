using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSO2018.Entities;

namespace DataContext.Configurations {

	public class RecordMap : IEntityTypeConfiguration<Record> {

		public void Configure(EntityTypeBuilder<Record> builder) {
			builder.ToTable(nameof(Record), "data");
			builder.HasKey(x => x.ID);

			builder.Property(x => x.ImageFileName)
				.HasMaxLength(255)
				.IsRequired()
				.IsUnicode(false);
			builder.Property(x => x.Township)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.County)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.SurveyDate)
				.IsRequired();
			builder.Property(x => x.City)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.State)
				.IsRequired()
				.HasMaxLength(30)
				.IsUnicode(false);
			builder.Property(x => x.Address)
				.IsRequired()
				.HasMaxLength(255)
				.IsUnicode(false);

			builder.Property(x => x.CrossStreet)
				.HasMaxLength(255)
				.IsUnicode(false);
			builder.Property(x => x.AutomatedFileNumber)
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.ClientName)
				.HasMaxLength(100)
				.IsUnicode(false);
			builder.Property(x => x.Description)
				.HasMaxLength(2000)
				.IsUnicode(false);
			builder.Property(x => x.OriginalLot)
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.ParcelNumber)
				.HasMaxLength(255)
				.IsUnicode(false);
			builder.Property(x => x.Section)
				.HasMaxLength(50)
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
			builder.Property(x => x.Tract)
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.Range)
				.HasMaxLength(20)
				.IsUnicode(false);
		}

	}
}

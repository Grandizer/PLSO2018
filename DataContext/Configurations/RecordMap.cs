using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSO2018.Entities;

namespace DataContext.Configurations {

	public class RecordMap : IEntityTypeConfiguration<Record> {

		public void Configure(EntityTypeBuilder<Record> builder) {
			builder.ToTable(nameof(Record), "data");
			builder.HasKey(x => x.ID);

			builder.Property(x => x.ImageFileName)
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.Township)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.County)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.City)
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.Street)
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.Tract)
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.OriginalLot)
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.Subdivision)
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.Sublot)
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.DeedVolume)
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.DeedPage)
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.ParcelNumber)
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.Description)
				.HasMaxLength(100)
				.IsUnicode(false);
			builder.Property(x => x.RecordingInfo)
				.HasMaxLength(50)
				.IsUnicode(false);
		}

	}
}

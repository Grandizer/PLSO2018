using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSO2018.Entities;

namespace DataContext.Configurations {

	public class LocationMap : IEntityTypeConfiguration<Location> {

		public void Configure(EntityTypeBuilder<Location> builder) {
			builder.ToTable(nameof(Location), "data");
			builder.HasKey(x => x.ID);

			builder.Property(x => x.Address)
				.HasMaxLength(100)
				.IsUnicode(false);
			builder.Property(x => x.Latitude)
				.HasColumnType("decimal(11, 8)");
			builder.Property(x => x.Longitude)
				.HasColumnType("decimal(11, 8)");
			builder.Property(x => x.LocationTypeID)
				.IsRequired();
		}

	}
}

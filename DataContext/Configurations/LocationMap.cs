using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSO2018.Entities;

namespace DataContext.Configurations {

	public class LocationMap : IEntityTypeConfiguration<Location> {

		public void Configure(EntityTypeBuilder<Location> builder) {
			builder.ToTable(nameof(Location), "data");
			builder.HasKey(x => x.ID);

			builder.Property(x => x.Address)
				.HasMaxLength(255)
				.IsUnicode(false);
			builder.Property(x => x.LocationTypeID)
				.IsRequired();
		}

	}
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSO2018.Entities;

namespace DataContext.Configurations {

	public class PlatMap : IEntityTypeConfiguration<Plat> {

		public void Configure(EntityTypeBuilder<Plat> builder) {
			builder.ToTable(nameof(Plat), "data");
			builder.HasKey(x => x.ID);

			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.Volumne)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.Page)
				.IsRequired()
				.IsUnicode(false);
			builder.Property(x => x.Date)
				.IsRequired();
		}

	}
}

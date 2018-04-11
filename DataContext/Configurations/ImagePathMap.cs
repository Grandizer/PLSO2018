using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSO2018.Entities;

namespace DataContext.Configurations {

	public class ImagePathMap : IEntityTypeConfiguration<ImagePath> {

		public void Configure(EntityTypeBuilder<ImagePath> builder) {
			builder.ToTable(nameof(ImagePath), "data");
			builder.HasKey(x => x.ID);

			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(20)
				.IsUnicode(false);
			builder.Property(x => x.IsDefault)
				.IsRequired();
			builder.Property(x => x.ServerPath)
				.IsRequired()
				.HasMaxLength(255)
				.IsUnicode(false);
			builder.Property(x => x.RelativeToRoot)
				.IsRequired()
				.HasMaxLength(255)
				.IsUnicode(false);
		}

	}
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSO2018.Entities;

namespace DataContext.Configurations {

	public class EmailMap : IEntityTypeConfiguration<Email> {

		public void Configure(EntityTypeBuilder<Email> builder) {
			builder.ToTable(nameof(Email), "data");
			builder.HasKey(x => x.ID);

			builder.Property(x => x.Address)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.IsPrimary)
				.IsRequired();
			builder.Property(x => x.EmailTypeID)
				.IsRequired();
		}

	}
}

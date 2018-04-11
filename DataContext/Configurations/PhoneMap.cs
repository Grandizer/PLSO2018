using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSO2018.Entities;

namespace DataContext.Configurations {

	public class PhoneMap : IEntityTypeConfiguration<Phone> {

		public void Configure(EntityTypeBuilder<Phone> builder) {
			builder.ToTable(nameof(Phone), "data");
			builder.HasKey(x => x.ID);

			builder.Property(x => x.Number)
				.IsRequired()
				.HasMaxLength(10)
				.IsUnicode(false);
			builder.Property(x => x.Extension)
				.HasMaxLength(6)
				.IsUnicode(false);
			builder.Property(x => x.IsPrimary)
				.IsRequired();
		}

	}
}

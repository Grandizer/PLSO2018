using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSO2018.Entities;

namespace DataContext.Configurations {

	public class PhoneTypeMap : IEntityTypeConfiguration<PhoneType> {

		public void Configure(EntityTypeBuilder<PhoneType> builder) {
			builder.ToTable(nameof(PhoneType), "ref");
			builder.HasKey(x => x.ID);

			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(30)
				.IsUnicode(false);
			builder.Property(x => x.Description)
				.HasMaxLength(255)
				.IsUnicode(false);
		}

	}
}

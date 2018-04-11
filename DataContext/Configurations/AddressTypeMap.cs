using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSO2018.Entities;

namespace DataContext.Configurations {

	public class AddressTypeMap : IEntityTypeConfiguration<AddressType> {

		public void Configure(EntityTypeBuilder<AddressType> builder) {
			builder.ToTable(nameof(AddressType), "ref");
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

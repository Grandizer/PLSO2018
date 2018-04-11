using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSO2018.Entities;

namespace DataContext.Configurations {

	public class AddressMap : IEntityTypeConfiguration<Address> {

		public void Configure(EntityTypeBuilder<Address> builder) {
			builder.ToTable(nameof(Address), "data");
			builder.HasKey(x => x.ID);

			builder.Property(x => x.CompanyName)
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.Address1)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.Address2)
				.HasMaxLength(40)
				.IsUnicode(false);
			builder.Property(x => x.City)
				.IsRequired()
				.HasMaxLength(30)
				.IsUnicode(false);
			builder.Property(x => x.State)
				.IsRequired()
				.HasMaxLength(2)
				.IsUnicode(false);
			builder.Property(x => x.Zip)
				.IsRequired()
				.HasMaxLength(10)
				.IsUnicode(false);
			builder.Property(x => x.IsPrimary)
				.IsRequired();
			builder.Property(x => x.AddressTypeID)
				.IsRequired();
		}

	}
}

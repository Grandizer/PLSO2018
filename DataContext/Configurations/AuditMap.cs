using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSO2018.Entities;

namespace DataContext.Configurations {

	public class AuditMap : IEntityTypeConfiguration<Audit> {

		public void Configure(EntityTypeBuilder<Audit> builder) {
			builder.ToTable(nameof(Audit), "log");
			builder.HasKey(x => x.ID);

			builder.Property(x => x.EntityID)
				.IsRequired();
			builder.Property(x => x.EntityName)
				.IsRequired()
				.IsUnicode(false)
				.HasMaxLength(150);
			builder.Property(x => x.AuditActionID)
				.IsRequired();
			builder.Property(x => x.Data)
				.IsUnicode(false); // This should be the only property with no Max limit. We may be able to come up with a decent number.
		}

	}
}

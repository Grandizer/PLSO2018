using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSO2018.Entities;

namespace DataContext.Configurations {

	public class AuditActionMap : IEntityTypeConfiguration<AuditAction> {

		public void Configure(EntityTypeBuilder<AuditAction> builder) {
			builder.ToTable(nameof(AuditAction), "ref");
			builder.HasKey(x => x.ID);
		}

	}
}

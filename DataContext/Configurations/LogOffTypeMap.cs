using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSO2018.Entities;

namespace DataContext.Configurations {

	public class LogOffTypeMap : IEntityTypeConfiguration<LogOffType> {

		public void Configure(EntityTypeBuilder<LogOffType> builder) {
			builder.ToTable(nameof(LogOffType), "ref");
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

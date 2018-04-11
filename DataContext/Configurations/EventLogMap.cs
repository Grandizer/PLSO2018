using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSO2018.Entities;

namespace DataContext.Configurations {

	public class EventLogMap : IEntityTypeConfiguration<EventLog> {

		public void Configure(EntityTypeBuilder<EventLog> builder) {
			builder.ToTable(nameof(EventLog), "log");
			builder.HasKey(x => x.ID);

			builder.Property(x => x.Message)
				.HasMaxLength(2000)
				.IsRequired();
			builder.Property(x => x.Occurred)
				.IsRequired();
			builder.Property(x => x.LogLevel)
				.HasMaxLength(50);
			builder.Property(x => x.Page)
				.HasMaxLength(255);
		}

	}
}

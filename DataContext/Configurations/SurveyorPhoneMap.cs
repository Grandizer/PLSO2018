using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSO2018.Entities;

namespace DataContext.Configurations {

	public class SurveyorPhoneMap : IEntityTypeConfiguration<SurveyorPhone> {

		public void Configure(EntityTypeBuilder<SurveyorPhone> builder) {
			builder.ToTable(nameof(SurveyorPhone), "xref");
			builder.HasKey(x => x.ID);

			builder.Property(x => x.SurveyorID)
				.IsRequired();
			builder.Property(x => x.PhoneID)
				.IsRequired();
		}

	}
}

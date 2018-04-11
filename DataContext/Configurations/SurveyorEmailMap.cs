using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSO2018.Entities;

namespace DataContext.Configurations {

	public class SurveyorEmailMap : IEntityTypeConfiguration<SurveyorEmail> {

		public void Configure(EntityTypeBuilder<SurveyorEmail> builder) {
			builder.ToTable(nameof(SurveyorEmail), "xref");
			builder.HasKey(x => x.ID);

			builder.Property(x => x.SurveyorID)
				.IsRequired();
			builder.Property(x => x.EmailID)
				.IsRequired();
		}

	}
}

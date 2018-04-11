using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSO2018.Entities;

namespace DataContext.Configurations {

	public class SurveyorAddressMap : IEntityTypeConfiguration<SurveyorAddress> {

		public void Configure(EntityTypeBuilder<SurveyorAddress> builder) {
			builder.ToTable(nameof(SurveyorAddress), "xref");
			builder.HasKey(x => x.ID);

			builder.Property(x => x.SurveyorID)
				.IsRequired();
			builder.Property(x => x.AddressID)
				.IsRequired();
		}

	}
}

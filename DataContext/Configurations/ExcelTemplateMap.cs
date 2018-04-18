using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSO2018.Entities;

namespace DataContext.Configurations {

	public class ExcelTemplateMap : IEntityTypeConfiguration<ExcelTemplate> {

		public void Configure(EntityTypeBuilder<ExcelTemplate> builder) {
			builder.ToTable(nameof(ExcelTemplate), "ref");
			builder.HasKey(x => x.ID);

			builder.Property(x => x.IsCalculated)
				.IsRequired();
			builder.Property(x => x.IsRequired)
				.IsRequired();
			builder.Property(x => x.ColumnWidth)
				.IsRequired();
			builder.Property(x => x.ColumnIndex)
				.IsRequired();
			builder.Property(x => x.DisplayName)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false);
			builder.Property(x => x.FieldName)
				.IsRequired()
				.HasMaxLength(20)
				.IsUnicode(false);
			builder.Property(x => x.ExampleData)
				.IsRequired()
				.HasMaxLength(100)
				.IsUnicode(false);
			builder.Property(x => x.Validation)
				.IsRequired()
				.HasMaxLength(1000)
				.IsUnicode(false);
		}

	}
}

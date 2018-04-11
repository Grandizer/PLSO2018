using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSO2018.Entities;

namespace DataContext.Configurations {

	public class ApplicationUserMap : IEntityTypeConfiguration<ApplicationUser> {

		public void Configure(EntityTypeBuilder<ApplicationUser> builder) {
			builder.ToTable("AspNetUser", "security");
			builder.HasKey(x => x.Id);

			builder.HasMany(x => x.Claims).WithOne().HasForeignKey(x => x.UserId);

			builder.Property(x => x.FirstName)
				.IsRequired()
				.HasMaxLength(20)
				.IsUnicode(false);
			builder.Property(x => x.MiddleInitial)
				.HasMaxLength(2)
				.IsUnicode(false);
			builder.Property(x => x.LastName)
				.IsRequired()
				.HasMaxLength(30)
				.IsUnicode(false);
			builder.Property(x => x.SuffixShort)
				.HasMaxLength(3)
				.IsUnicode(false);
			builder.Property(x => x.Suffix)
				.HasMaxLength(20)
				.IsUnicode(false);
			builder.Property(x => x.IsActive)
				.IsRequired();
		}

	}
}

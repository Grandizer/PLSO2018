using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLSO2018.Entities;

namespace DataContext.Configurations {

	public class UserLogonMap : IEntityTypeConfiguration<UserLogon> {

		public void Configure(EntityTypeBuilder<UserLogon> builder) {
			builder.ToTable(nameof(UserLogon), "log");
			builder.HasKey(x => x.ID);

			builder.Property(x => x.SurveyorID)
				.IsRequired();
			builder.Property(x => x.LoggedIn)
				.IsRequired();
			builder.Property(x => x.RemoteAddress)
				.HasMaxLength(25)
				.IsUnicode(false);
			builder.Property(x => x.LocalAddress)
				.HasMaxLength(25)
				.IsUnicode(false);
			builder.Property(x => x.HttpUserAgent)
				.HasMaxLength(255)
				.IsUnicode(false);
		}

	}
}

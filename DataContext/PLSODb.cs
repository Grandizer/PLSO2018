using PLSO2018.Entities.Support;
using DataContext.Configurations;
using DataContext.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using PLSO2018.Entities;
using PLSO2018.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DataContext {

	public class PLSODb : IdentityDbContext<ApplicationUser, ApplicationRole, int> {

		private readonly ILogger logger;
		private readonly UserResolver userResolver;

		public PLSODb(DbContextOptions<PLSODb> options) : base(options) {

		}

		public PLSODb(DbContextOptions<PLSODb> options, UserResolver userResolver, ILoggerFactory loggerFactory) : base(options) {
			this.logger = loggerFactory.CreateLogger<PLSODb>();
			this.userResolver = userResolver;
		}

		public override int SaveChanges() {
			return SaveChanges(userResolver.UsersID);
		}

		public int SaveChanges(int userID) {
			int Result = 0;

			// UPDATES - Add Audit records for each Modified entity
			var areUps = (from e in this.ChangeTracker.Entries()
										where e.State == EntityState.Modified
										select e).ToList();

			foreach (EntityEntry e in areUps) {
				Audit updateAudit = null;

				if (e.Entity is AuditableBase) {
					List<string> AlteredProperties = new List<string>();
					updateAudit = new Audit() {
						AuditActionID = (int)AuditActionTypes.Update, // enumProps.GetDBID(AuditActionTypes.Update),
						CreationDate = DateTime.Now,
						CreatedByID = userID,
						EntityName = GetEntityName(e),
						EntityID = ((AuditableBase)e.Entity).ID,
						Data = ParseChanges(e, out AlteredProperties)
					};

					if (!string.IsNullOrWhiteSpace(updateAudit.Data))
						this.Set<Audit>().Add(updateAudit);
				} else if (e.Entity is TemporalBase) {
					((TemporalBase)e.Entity).ModifieddByID = userID;
				} // if this is a AuditableBase derrived class
			} // foreach of the UPDATE'd entities


			// DELETES - Add Audit records for each Removed entity
			var areDel = (from e in this.ChangeTracker.Entries()
										where e.State == EntityState.Deleted
										select e).ToList();

			foreach (EntityEntry e in areDel) {
				if (e.Entity is AuditableBase) {
					var DbValues = e.GetDatabaseValues().ToObject();
					Audit deleteAudit = new Audit() {
						AuditActionID = (int)AuditActionTypes.Delete, // enumProps.GetDBID(AuditActionTypes.Delete),
						CreationDate = DateTime.Now,
						CreatedByID = userID,
						EntityID = ((AuditableBase)e.Entity).ID,
						EntityName = GetEntityName(e),
						Data = JsonConvert.SerializeObject(DbValues)
					};

					deleteAudit.EntityID = ((AuditableBase)e.Entity).ID;

					this.Set<Audit>().Add(deleteAudit);
				} else if (e.Entity is TemporalBase) {
					var DbValues = e.GetDatabaseValues().ToObject();
					Audit deleteAudit = new Audit() {
						AuditActionID = (int)AuditActionTypes.Delete, // enumProps.GetDBID(AuditActionTypes.Delete),
						CreationDate = DateTime.Now,
						CreatedByID = userID,
						EntityID = ((TemporalBase)e.Entity).ID,
						EntityName = GetEntityName(e),
						Data = JsonConvert.SerializeObject(DbValues)
					};

					deleteAudit.EntityID = ((AuditableBase)e.Entity).ID;

					this.Set<Audit>().Add(deleteAudit);
				} // if this is an AuditableBase entity
			} // foreach of the Deleted entities


			List<KeyValuePair<Audit, AuditableBase>> NewAudits = new List<KeyValuePair<Audit, AuditableBase>>();

			// INSERTS - Make sure any Added entity has the default values set
			// NOTE: This loop should always be LAST since the above two may Add new entities
			var areNew = (from e in this.ChangeTracker.Entries()
										where e.State == EntityState.Added
										select e).ToList();

			foreach (EntityEntry e in areNew) {
				if (e.Entity is AuditableBase) {
					if (((AuditableBase)e.Entity).CreationDate <= new DateTime(1999, 1, 1, 0, 0, 0))
						((AuditableBase)e.Entity).CreationDate = DateTime.Now;
					if (((AuditableBase)e.Entity).CreatedByID == 0)
						((AuditableBase)e.Entity).CreatedByID = userID;
				} else if (e.Entity is TemporalBase) {
					if (((TemporalBase)e.Entity).ModifieddByID == 0)
						((TemporalBase)e.Entity).ModifieddByID = userID;
				} // if this is a common AuditableBase'd entity
			} // foreach of the INSERT entities

			try {
				Result = base.SaveChanges();

				foreach (var pair in NewAudits) {
					pair.Key.EntityID = pair.Value.ID;
				}

				if (NewAudits.Count > 0)
					base.SaveChanges();
			} catch (Exception e) {
				logger.LogError(1, e, "SaveChanges error");
				throw e;
			} // try-catch

			return Result;
		}

		private static string GetEntityName(EntityEntry e) {
			string Result = "";

			if (e.Entity.GetType().GetTypeInfo() != null && e.Entity.GetType().Namespace == "System.Data.Entity.DynamicProxies")
				Result = e.GetType().GetTypeInfo().BaseType.FullName;
			else
				Result = e.Entity.GetType().FullName;

			return Result;
		} // GetEntityName

		private static string ParseChanges(EntityEntry e, out List<string> whatChanged) {
			StringBuilder Result = new StringBuilder();

			var DbValues = e.GetDatabaseValues().ToObject();
			((AuditableBase)DbValues).EqualTo((AuditableBase)e.Entity, out List<string> WhatChanged);
			whatChanged = WhatChanged;

			foreach (PropertyInfo pi in DbValues.GetType().GetProperties().OrderBy(x => x.Name).Where(x => x.DeclaringType != typeof(AuditableBase))) {
				if (WhatChanged.Contains(pi.Name)) {
					var was = pi.GetValue(DbValues, null) ?? string.Empty;
					var now = pi.GetValue(e.Entity, null) ?? string.Empty;

					Result.AppendLineFormat(Constants.AuditTrail.PropertyChangedFormat, pi.Name, was, now);
				} // if this is a Property that changed
			} // foreach of the Properties on the object

			if (Result.Length > 0) {
				Result.Insert(0, Constants.AuditTrail.DataPrefix);
				Result.Append(Constants.AuditTrail.DataSuffix);
			} // if there was a change

			return Result.ToString();
		} // ParseChanges - Method

		#region Tables

		// Data schema
		public DbSet<Address> Addresses { get; set; }
		public DbSet<Email> Emails { get; set; }
		public DbSet<ImagePath> ImagePaths { get; set; }
		public DbSet<Location> Locations { get; set; }
		public DbSet<Phone> Phones { get; set; }
		public DbSet<Plat> Plats { get; set; }
		public DbSet<Record> Records { get; set; }
		public DbSet<ApplicationUser> Surveyors { get; set; }

		// Log schema
		public DbSet<EventLog> Errors { get; set; }
		public DbSet<UserLogon> UserLogons { get; set; }

		// Ref schema
		public DbSet<AddressType> AddressTypes { get; set; }
		public DbSet<EmailType> EmailTypes { get; set; }
		public DbSet<LocationType> LocationTypes { get; set; }
		public DbSet<LogOffType> LogOffTypes { get; set; }
		public DbSet<PhoneType> PhoneTypes { get; set; }
		public DbSet<SurveyType> SurveyTypes { get; set; }

		// Security schema
		public DbSet<ApplicationUser> AspNetUsers { get; set; }
		public DbSet<ApplicationRole> AspNetRoles { get; set; }
		public DbSet<IdentityRoleClaim<int>> AspNetRoleClaims { get; set; }
		public DbSet<IdentityUserClaim<int>> AspNetUserClaims { get; set; }
		public DbSet<IdentityUserLogin<int>> AspNetUserLogins { get; set; }
		public DbSet<IdentityUserRole<int>> AspNetUserRoles { get; set; }
		public DbSet<IdentityUserToken<int>> AspNetUserTokens { get; set; }

		// XRef schema
		public DbSet<SurveyorAddress> SurveyorAddresses { get; set; }
		public DbSet<SurveyorEmail> SurveyorEmails { get; set; }
		public DbSet<SurveyorPhone> SurveyorPhones { get; set; }

		#endregion Tables

		protected override void OnModelCreating(ModelBuilder builder) {
			base.OnModelCreating(builder);

			// Data schema
			builder.ApplyConfiguration(new AddressMap());
			builder.ApplyConfiguration(new EmailMap());
			builder.ApplyConfiguration(new ImagePathMap());
			builder.ApplyConfiguration(new LocationMap());
			builder.ApplyConfiguration(new PhoneMap());
			builder.ApplyConfiguration(new PlatMap());
			builder.ApplyConfiguration(new RecordMap());
			builder.ApplyConfiguration(new ApplicationUserMap());

			// Log schema
			builder.ApplyConfiguration(new AuditMap());
			builder.ApplyConfiguration(new EventLogMap());
			builder.ApplyConfiguration(new UserLogonMap());

			// Ref schema
			builder.ApplyConfiguration(new AddressTypeMap());
			builder.ApplyConfiguration(new AuditActionMap());
			builder.ApplyConfiguration(new EmailTypeMap());
			builder.ApplyConfiguration(new LocationTypeMap());
			builder.ApplyConfiguration(new LogOffTypeMap());
			builder.ApplyConfiguration(new PhoneTypeMap());
			builder.ApplyConfiguration(new SurveyTypeMap());

			// XRef schema
			builder.ApplyConfiguration(new SurveyorAddressMap());
			builder.ApplyConfiguration(new SurveyorEmailMap());
			builder.ApplyConfiguration(new SurveyorPhoneMap());


			// Security schema
			// https://medium.com/@goodealsnow/asp-net-core-identity-3-0-6018fc151b4
			// Essentially what changed here was that the default dbo schema was changed to security for Identity.
			// To follow the article as well as stay like the rest of the application, the string/GUID ID fields were altered to be Integers
			// NOTE: Each of the following .HasMaxLength values are assumed
			builder.ApplyConfiguration(new ApplicationUserMap());

			builder.Entity<ApplicationRole>(entity => {
				entity.ToTable(name: "AspNetRole", schema: "security");
				entity.Property(x => x.ConcurrencyStamp).HasMaxLength(50);
			});

			builder.Entity<IdentityUserClaim<int>>(entity => {
				entity.ToTable("AspNetUserClaim", "security");
				entity.Property(x => x.ClaimType).HasMaxLength(1000);
				entity.Property(x => x.ClaimValue).HasMaxLength(1000);
			});

			builder.Entity<IdentityUserLogin<int>>(entity => {
				entity.ToTable("AspNetUserLogin", "security");
				entity.HasKey(x => new { x.LoginProvider, x.ProviderKey });
				entity.Property(x => x.ProviderDisplayName).HasMaxLength(1000);
			});

			builder.Entity<IdentityRoleClaim<int>>(entity => {
				entity.ToTable("AspNetRoleClaim", "security");
				entity.Property(x => x.ClaimType).HasMaxLength(1000);
				entity.Property(x => x.ClaimValue).HasMaxLength(1000);
			});

			builder.Entity<IdentityUserRole<int>>(entity => {
				entity.ToTable("AspNetUserRole", "security");
				entity.HasKey(x => new { x.UserId, x.RoleId });
			});

			builder.Entity<IdentityUserToken<int>>(entity => {
				entity.ToTable("AspNetUserToken", "security");
				entity.HasKey(x => new { x.UserId, x.LoginProvider, x.Name });
				entity.Property(x => x.Value).HasMaxLength(1000);
			});
		}

	}
}

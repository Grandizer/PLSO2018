﻿// <auto-generated />
using System;
using DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataContext.Migrations
{
    [DbContext(typeof(PLSODb))]
    partial class PLSODbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasMaxLength(1000);

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(1000);

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaim","security");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasMaxLength(1000);

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(1000);

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaim","security");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName")
                        .HasMaxLength(1000);

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogin","security");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRole","security");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value")
                        .HasMaxLength(1000);

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserToken","security");
                });

            modelBuilder.Entity("PLSO2018.Entities.Address", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Address2")
                        .HasMaxLength(40)
                        .IsUnicode(false);

                    b.Property<int>("AddressTypeID");

                    b.Property<int?>("ApplicationUserId");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("CompanyName")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("CreatedByID");

                    b.Property<DateTimeOffset>("CreationDate");

                    b.Property<bool>("IsPrimary");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<string>("Zip")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.HasKey("ID");

                    b.HasIndex("AddressTypeID");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Address","data");
                });

            modelBuilder.Entity("PLSO2018.Entities.AddressType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.HasKey("ID");

                    b.ToTable("AddressType","ref");
                });

            modelBuilder.Entity("PLSO2018.Entities.ApplicationRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRole","security");
                });

            modelBuilder.Entity("PLSO2018.Entities.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<bool>("IsActive");

                    b.Property<DateTimeOffset>("LastActivityDate");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MiddleInitial")
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<int>("Number");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Suffix")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("SuffixShort")
                        .HasMaxLength(3)
                        .IsUnicode(false);

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUser","security");
                });

            modelBuilder.Entity("PLSO2018.Entities.Audit", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuditActionID");

                    b.Property<long>("CreatedByID");

                    b.Property<DateTimeOffset>("CreationDate");

                    b.Property<string>("Data")
                        .IsUnicode(false);

                    b.Property<long>("EntityID");

                    b.Property<string>("EntityName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.HasKey("ID");

                    b.HasIndex("AuditActionID");

                    b.ToTable("Audit","log");
                });

            modelBuilder.Entity("PLSO2018.Entities.AuditAction", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CreatedByID");

                    b.Property<DateTimeOffset>("CreationDate");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("EnumName")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("ID");

                    b.ToTable("AuditAction","ref");
                });

            modelBuilder.Entity("PLSO2018.Entities.Email", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int?>("ApplicationUserId");

                    b.Property<int>("CreatedByID");

                    b.Property<DateTimeOffset>("CreationDate");

                    b.Property<int>("EmailTypeID");

                    b.Property<bool>("IsPrimary");

                    b.HasKey("ID");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("EmailTypeID");

                    b.ToTable("Email","data");
                });

            modelBuilder.Entity("PLSO2018.Entities.EmailType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.HasKey("ID");

                    b.ToTable("EmailType","ref");
                });

            modelBuilder.Entity("PLSO2018.Entities.EventLog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EventID");

                    b.Property<string>("LogLevel")
                        .HasMaxLength(50);

                    b.Property<string>("Message")
                        .IsRequired();

                    b.Property<DateTimeOffset>("Occurred");

                    b.Property<string>("Page")
                        .HasMaxLength(255);

                    b.Property<string>("StackTrace");

                    b.Property<int?>("SurveyorID");

                    b.HasKey("ID");

                    b.ToTable("EventLog","log");
                });

            modelBuilder.Entity("PLSO2018.Entities.ExcelTemplate", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ColumnIndex");

                    b.Property<int>("ColumnWidth");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("ExampleData")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("FieldName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<bool>("IsCalculated");

                    b.Property<bool>("IsRequired");

                    b.Property<int>("MaximumLength");

                    b.Property<int>("MinimumLength");

                    b.Property<int>("ModifiedByID");

                    b.Property<string>("Validation")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(false);

                    b.HasKey("ID");

                    b.ToTable("ExcelTemplate","ref");
                });

            modelBuilder.Entity("PLSO2018.Entities.ImagePath", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedByID");

                    b.Property<DateTimeOffset>("CreationDate");

                    b.Property<bool>("IsDefault");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("RelativeToRoot")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<string>("ServerPath")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.HasKey("ID");

                    b.ToTable("ImagePath","data");
                });

            modelBuilder.Entity("PLSO2018.Entities.Location", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<int>("CreatedByID");

                    b.Property<DateTimeOffset>("CreationDate");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("decimal(11, 8)");

                    b.Property<int>("LocationTypeID");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("decimal(11, 8)");

                    b.HasKey("ID");

                    b.HasIndex("LocationTypeID");

                    b.ToTable("Location","data");
                });

            modelBuilder.Entity("PLSO2018.Entities.LocationType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.HasKey("ID");

                    b.ToTable("LocationType","ref");
                });

            modelBuilder.Entity("PLSO2018.Entities.LogOffType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.HasKey("ID");

                    b.ToTable("LogOffType","ref");
                });

            modelBuilder.Entity("PLSO2018.Entities.Phone", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ApplicationUserId");

                    b.Property<int>("CreatedByID");

                    b.Property<DateTimeOffset>("CreationDate");

                    b.Property<string>("Extension")
                        .HasMaxLength(6)
                        .IsUnicode(false);

                    b.Property<bool>("IsPrimary");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<int>("PhoneTypeID");

                    b.HasKey("ID");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("PhoneTypeID");

                    b.ToTable("Phone","data");
                });

            modelBuilder.Entity("PLSO2018.Entities.PhoneType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.HasKey("ID");

                    b.ToTable("PhoneType","ref");
                });

            modelBuilder.Entity("PLSO2018.Entities.Record", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<bool>("Approved");

                    b.Property<string>("AutomatedFileNumber")
                        .HasMaxLength(18)
                        .IsUnicode(false);

                    b.Property<string>("CityVillageTownship")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("ClientName")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("County")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.Property<string>("CrossStreet")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<int?>("DeedPage");

                    b.Property<int?>("DeedVolume");

                    b.Property<string>("DefunctTownship")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<int>("HashCode");

                    b.Property<string>("ImagePath")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<int?>("LocationID");

                    b.Property<string>("LotNumbers")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.Property<string>("MapImageName")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(false);

                    b.Property<int>("ModifiedByID");

                    b.Property<string>("Notes")
                        .HasMaxLength(2000)
                        .IsUnicode(false);

                    b.Property<string>("ParcelNumbers")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Range")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.Property<string>("Section")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("Subdivision")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Sublot")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<DateTime>("SurveyDate");

                    b.Property<string>("SurveyName")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int?>("SurveyorID");

                    b.Property<string>("SurveyorName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("SurveyorNumber")
                        .HasMaxLength(5)
                        .IsUnicode(false);

                    b.Property<string>("Tract")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.Property<int>("UploadedByID");

                    b.Property<DateTimeOffset>("UploadedDate");

                    b.HasKey("ID");

                    b.HasIndex("LocationID");

                    b.HasIndex("SurveyorID");

                    b.ToTable("Record","data");
                });

            modelBuilder.Entity("PLSO2018.Entities.SurveyorAddress", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressID");

                    b.Property<int>("SurveyorID");

                    b.HasKey("ID");

                    b.HasIndex("AddressID");

                    b.HasIndex("SurveyorID");

                    b.ToTable("SurveyorAddress","xref");
                });

            modelBuilder.Entity("PLSO2018.Entities.SurveyorEmail", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmailID");

                    b.Property<int>("SurveyorID");

                    b.HasKey("ID");

                    b.HasIndex("EmailID");

                    b.HasIndex("SurveyorID");

                    b.ToTable("SurveyorEmail","xref");
                });

            modelBuilder.Entity("PLSO2018.Entities.SurveyorPhone", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PhoneID");

                    b.Property<int>("SurveyorID");

                    b.HasKey("ID");

                    b.HasIndex("PhoneID");

                    b.HasIndex("SurveyorID");

                    b.ToTable("SurveyorPhone","xref");
                });

            modelBuilder.Entity("PLSO2018.Entities.SurveyType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.HasKey("ID");

                    b.ToTable("SurveyType","ref");
                });

            modelBuilder.Entity("PLSO2018.Entities.UserLogon", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HttpUserAgent")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<string>("LocalAddress")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.Property<DateTimeOffset>("LoggedIn");

                    b.Property<DateTimeOffset>("LoggedOff");

                    b.Property<int>("LoggedOffByID");

                    b.Property<int>("LoggedOffTypeID");

                    b.Property<string>("RemoteAddress")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.Property<int>("SurveyorID");

                    b.HasKey("ID");

                    b.HasIndex("SurveyorID");

                    b.ToTable("UserLogon","log");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("PLSO2018.Entities.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("PLSO2018.Entities.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("PLSO2018.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("PLSO2018.Entities.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PLSO2018.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("PLSO2018.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PLSO2018.Entities.Address", b =>
                {
                    b.HasOne("PLSO2018.Entities.EmailType", "AddressType")
                        .WithMany()
                        .HasForeignKey("AddressTypeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PLSO2018.Entities.ApplicationUser")
                        .WithMany("Addresses")
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("PLSO2018.Entities.Audit", b =>
                {
                    b.HasOne("PLSO2018.Entities.AuditAction", "AuditAction")
                        .WithMany()
                        .HasForeignKey("AuditActionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PLSO2018.Entities.Email", b =>
                {
                    b.HasOne("PLSO2018.Entities.ApplicationUser")
                        .WithMany("Emails")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("PLSO2018.Entities.EmailType", "EmailType")
                        .WithMany()
                        .HasForeignKey("EmailTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PLSO2018.Entities.Location", b =>
                {
                    b.HasOne("PLSO2018.Entities.LocationType", "LocationType")
                        .WithMany()
                        .HasForeignKey("LocationTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PLSO2018.Entities.Phone", b =>
                {
                    b.HasOne("PLSO2018.Entities.ApplicationUser")
                        .WithMany("Phones")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("PLSO2018.Entities.PhoneType", "PhoneType")
                        .WithMany()
                        .HasForeignKey("PhoneTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PLSO2018.Entities.Record", b =>
                {
                    b.HasOne("PLSO2018.Entities.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationID");

                    b.HasOne("PLSO2018.Entities.ApplicationUser", "Surveyor")
                        .WithMany()
                        .HasForeignKey("SurveyorID");
                });

            modelBuilder.Entity("PLSO2018.Entities.SurveyorAddress", b =>
                {
                    b.HasOne("PLSO2018.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PLSO2018.Entities.ApplicationUser", "Surveyor")
                        .WithMany()
                        .HasForeignKey("SurveyorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PLSO2018.Entities.SurveyorEmail", b =>
                {
                    b.HasOne("PLSO2018.Entities.Email", "Email")
                        .WithMany()
                        .HasForeignKey("EmailID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PLSO2018.Entities.ApplicationUser", "Surveyor")
                        .WithMany()
                        .HasForeignKey("SurveyorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PLSO2018.Entities.SurveyorPhone", b =>
                {
                    b.HasOne("PLSO2018.Entities.Phone", "Phone")
                        .WithMany()
                        .HasForeignKey("PhoneID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PLSO2018.Entities.ApplicationUser", "Surveyor")
                        .WithMany()
                        .HasForeignKey("SurveyorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PLSO2018.Entities.UserLogon", b =>
                {
                    b.HasOne("PLSO2018.Entities.ApplicationUser", "Surveyor")
                        .WithMany()
                        .HasForeignKey("SurveyorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

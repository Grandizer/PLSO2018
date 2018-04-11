using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataContext.Migrations {

	public partial class InitialMigration : Migration {

		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.EnsureSchema(name: "security");
			migrationBuilder.EnsureSchema(name: "data");
			migrationBuilder.EnsureSchema(name: "ref");
			migrationBuilder.EnsureSchema(name: "log");
			migrationBuilder.EnsureSchema(name: "xref");

			migrationBuilder.CreateTable(
					name: "ImagePath",
					schema: "data",
					columns: table => new {
						ID = table.Column<int>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						IsDefault = table.Column<bool>(nullable: false),
						Name = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
						ServerPath = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
						RelativeToRoot = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
						CreatedByID = table.Column<int>(nullable: false),
						CreationDate = table.Column<DateTimeOffset>(nullable: false),
					},
					constraints: table => {
						table.PrimaryKey("PK_ImagePath", x => x.ID);
					});

			migrationBuilder.CreateTable(
					name: "Plat",
					schema: "data",
					columns: table => new {
						ID = table.Column<int>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
						Date = table.Column<DateTime>(nullable: false),
						Page = table.Column<int>(unicode: false, nullable: false),
						Volumne = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
						ModifieddByID = table.Column<int>(nullable: false),
					},
					constraints: table => {
						table.PrimaryKey("PK_Plat", x => x.ID);
					});

			migrationBuilder.CreateTable(
					name: "EventLog",
					schema: "log",
					columns: table => new {
						ID = table.Column<int>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						EventID = table.Column<int>(nullable: true),
						SurveyorID = table.Column<int>(nullable: true),
						LogLevel = table.Column<string>(maxLength: 50, nullable: true),
						Occurred = table.Column<DateTimeOffset>(nullable: false),
						Page = table.Column<string>(maxLength: 255, nullable: true),
						Message = table.Column<string>(maxLength: 2000, nullable: false),
						StackTrace = table.Column<string>(nullable: true),
					},
					constraints: table => {
						table.PrimaryKey("PK_EventLog", x => x.ID);
					});

			migrationBuilder.CreateTable(
					name: "AddressType",
					schema: "ref",
					columns: table => new {
						ID = table.Column<int>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						Name = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
						Description = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
					},
					constraints: table => {
						table.PrimaryKey("PK_AddressType", x => x.ID);
					});

			migrationBuilder.CreateTable(
					name: "AuditAction",
					schema: "ref",
					columns: table => new {
						ID = table.Column<int>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						IsActive = table.Column<bool>(nullable: false),
						Name = table.Column<string>(type: "varchar(50)", nullable: false),
						EnumName = table.Column<string>(type: "varchar(30)", nullable: false),
						Description = table.Column<string>(type: "varchar(250)", nullable: true),
						CreatedByID = table.Column<long>(nullable: false),
						CreationDate = table.Column<DateTimeOffset>(nullable: false),
					},
					constraints: table => {
						table.PrimaryKey("PK_AuditAction", x => x.ID);
					});

			migrationBuilder.CreateTable(
					name: "EmailType",
					schema: "ref",
					columns: table => new {
						ID = table.Column<int>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						Name = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
						Description = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
					},
					constraints: table => {
						table.PrimaryKey("PK_EmailType", x => x.ID);
					});

			migrationBuilder.CreateTable(
					name: "LocationType",
					schema: "ref",
					columns: table => new {
						ID = table.Column<int>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						Name = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
						Description = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
					},
					constraints: table => {
						table.PrimaryKey("PK_LocationType", x => x.ID);
					});

			migrationBuilder.CreateTable(
					name: "LogOffType",
					schema: "ref",
					columns: table => new {
						ID = table.Column<int>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						Name = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
						Description = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
					},
					constraints: table => {
						table.PrimaryKey("PK_LogOffType", x => x.ID);
					});

			migrationBuilder.CreateTable(
					name: "PhoneType",
					schema: "ref",
					columns: table => new {
						ID = table.Column<int>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						Name = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
						Description = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
					},
					constraints: table => {
						table.PrimaryKey("PK_PhoneType", x => x.ID);
					});

			migrationBuilder.CreateTable(
					name: "SurveyType",
					schema: "ref",
					columns: table => new {
						ID = table.Column<int>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						Name = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
						Description = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
					},
					constraints: table => {
						table.PrimaryKey("PK_SurveyType", x => x.ID);
					});

			migrationBuilder.CreateTable(
					name: "AspNetRole",
					schema: "security",
					columns: table => new {
						Id = table.Column<int>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						Name = table.Column<string>(maxLength: 256, nullable: true),
						NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
						ConcurrencyStamp = table.Column<string>(maxLength: 50, nullable: true),
					},
					constraints: table => {
						table.PrimaryKey("PK_AspNetRole", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "AspNetUser",
					schema: "security",
					columns: table => new {
						Id = table.Column<int>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						IsActive = table.Column<bool>(nullable: false),
						FirstName = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
						MiddleInitial = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
						LastName = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
						UserName = table.Column<string>(maxLength: 256, nullable: true),
						Number = table.Column<int>(nullable: false),
						Email = table.Column<string>(maxLength: 256, nullable: true),
						PhoneNumber = table.Column<string>(nullable: true),
						Suffix = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
						SuffixShort = table.Column<string>(unicode: false, maxLength: 3, nullable: true),
						LastActivityDate = table.Column<DateTimeOffset>(nullable: false),
						LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
						AccessFailedCount = table.Column<int>(nullable: false),
						LockoutEnabled = table.Column<bool>(nullable: false),
						NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
						NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
						PasswordHash = table.Column<string>(nullable: true),
						EmailConfirmed = table.Column<bool>(nullable: false),
						PhoneNumberConfirmed = table.Column<bool>(nullable: false),
						TwoFactorEnabled = table.Column<bool>(nullable: false),
						SecurityStamp = table.Column<string>(nullable: true),
						ConcurrencyStamp = table.Column<string>(nullable: true),
					},
					constraints: table => {
						table.PrimaryKey("PK_AspNetUser", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "Audit",
					schema: "log",
					columns: table => new {
						ID = table.Column<long>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						AuditActionID = table.Column<int>(nullable: false),
						EntityID = table.Column<long>(nullable: false),
						EntityName = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
						Data = table.Column<string>(unicode: false, nullable: true),
						CreatedByID = table.Column<long>(nullable: false),
						CreationDate = table.Column<DateTimeOffset>(nullable: false),
					},
					constraints: table => {
						table.PrimaryKey("PK_Audit", x => x.ID);
						table.ForeignKey(
											name: "FK_Audit_AuditAction_AuditActionID",
											column: x => x.AuditActionID,
											principalSchema: "ref",
											principalTable: "AuditAction",
											principalColumn: "ID",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "Location",
					schema: "data",
					columns: table => new {
						ID = table.Column<int>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						LocationTypeID = table.Column<int>(nullable: false),
						Address = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
						Latitude = table.Column<decimal>(nullable: false),
						Longitude = table.Column<decimal>(nullable: false),
						CreatedByID = table.Column<int>(nullable: false),
						CreationDate = table.Column<DateTimeOffset>(nullable: false),
					},
					constraints: table => {
						table.PrimaryKey("PK_Location", x => x.ID);
						table.ForeignKey(
											name: "FK_Location_LocationType_LocationTypeID",
											column: x => x.LocationTypeID,
											principalSchema: "ref",
											principalTable: "LocationType",
											principalColumn: "ID",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "AspNetRoleClaim",
					schema: "security",
					columns: table => new {
						Id = table.Column<int>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						RoleId = table.Column<int>(nullable: false),
						ClaimType = table.Column<string>(maxLength: 1000, nullable: true),
						ClaimValue = table.Column<string>(maxLength: 1000, nullable: true),
					},
					constraints: table => {
						table.PrimaryKey("PK_AspNetRoleClaim", x => x.Id);
						table.ForeignKey(
											name: "FK_AspNetRoleClaim_AspNetRole_RoleId",
											column: x => x.RoleId,
											principalSchema: "security",
											principalTable: "AspNetRole",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "Address",
					schema: "data",
					columns: table => new {
						ID = table.Column<int>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						ApplicationUserId = table.Column<int>(nullable: true),
						AddressTypeID = table.Column<int>(nullable: false),
						IsPrimary = table.Column<bool>(nullable: false),
						CompanyName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
						Address1 = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
						Address2 = table.Column<string>(unicode: false, maxLength: 40, nullable: true),
						City = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
						State = table.Column<string>(unicode: false, maxLength: 2, nullable: false),
						Zip = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
						CreatedByID = table.Column<int>(nullable: false),
						CreationDate = table.Column<DateTimeOffset>(nullable: false),
					},
					constraints: table => {
						table.PrimaryKey("PK_Address", x => x.ID);
						table.ForeignKey(
											name: "FK_Address_EmailType_AddressTypeID",
											column: x => x.AddressTypeID,
											principalSchema: "ref",
											principalTable: "EmailType",
											principalColumn: "ID",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_Address_AspNetUser_ApplicationUserId",
											column: x => x.ApplicationUserId,
											principalSchema: "security",
											principalTable: "AspNetUser",
											principalColumn: "Id",
											onDelete: ReferentialAction.Restrict);
					});

			migrationBuilder.CreateTable(
					name: "Email",
					schema: "data",
					columns: table => new {
						ID = table.Column<int>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						ApplicationUserId = table.Column<int>(nullable: true),
						EmailTypeID = table.Column<int>(nullable: false),
						IsPrimary = table.Column<bool>(nullable: false),
						Address = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
						CreatedByID = table.Column<int>(nullable: false),
						CreationDate = table.Column<DateTimeOffset>(nullable: false),
					},
					constraints: table => {
						table.PrimaryKey("PK_Email", x => x.ID);
						table.ForeignKey(
											name: "FK_Email_AspNetUser_ApplicationUserId",
											column: x => x.ApplicationUserId,
											principalSchema: "security",
											principalTable: "AspNetUser",
											principalColumn: "Id",
											onDelete: ReferentialAction.Restrict);
						table.ForeignKey(
											name: "FK_Email_EmailType_EmailTypeID",
											column: x => x.EmailTypeID,
											principalSchema: "ref",
											principalTable: "EmailType",
											principalColumn: "ID",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "Phone",
					schema: "data",
					columns: table => new {
						ID = table.Column<int>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						ApplicationUserId = table.Column<int>(nullable: true),
						PhoneTypeID = table.Column<int>(nullable: false),
						IsPrimary = table.Column<bool>(nullable: false),
						Number = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
						Extension = table.Column<string>(unicode: false, maxLength: 6, nullable: true),
						CreatedByID = table.Column<int>(nullable: false),
						CreationDate = table.Column<DateTimeOffset>(nullable: false),
					},
					constraints: table => {
						table.PrimaryKey("PK_Phone", x => x.ID);
						table.ForeignKey(
											name: "FK_Phone_AspNetUser_ApplicationUserId",
											column: x => x.ApplicationUserId,
											principalSchema: "security",
											principalTable: "AspNetUser",
											principalColumn: "Id",
											onDelete: ReferentialAction.Restrict);
						table.ForeignKey(
											name: "FK_Phone_PhoneType_PhoneTypeID",
											column: x => x.PhoneTypeID,
											principalSchema: "ref",
											principalTable: "PhoneType",
											principalColumn: "ID",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "UserLogon",
					schema: "log",
					columns: table => new {
						ID = table.Column<int>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						SurveyorID = table.Column<int>(nullable: false),
						LoggedOffTypeID = table.Column<int>(nullable: false),
						LoggedOffByID = table.Column<int>(nullable: false),
						LoggedIn = table.Column<DateTimeOffset>(nullable: false),
						LoggedOff = table.Column<DateTimeOffset>(nullable: false),
						LocalAddress = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
						HttpUserAgent = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
						RemoteAddress = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
					},
					constraints: table => {
						table.PrimaryKey("PK_UserLogon", x => x.ID);
						table.ForeignKey(
											name: "FK_UserLogon_AspNetUser_SurveyorID",
											column: x => x.SurveyorID,
											principalSchema: "security",
											principalTable: "AspNetUser",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "AspNetUserClaim",
					schema: "security",
					columns: table => new {
						Id = table.Column<int>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						UserId = table.Column<int>(nullable: false),
						ClaimType = table.Column<string>(maxLength: 1000, nullable: true),
						ClaimValue = table.Column<string>(maxLength: 1000, nullable: true),
					},
					constraints: table => {
						table.PrimaryKey("PK_AspNetUserClaim", x => x.Id);
						table.ForeignKey(
											name: "FK_AspNetUserClaim_AspNetUser_UserId",
											column: x => x.UserId,
											principalSchema: "security",
											principalTable: "AspNetUser",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "AspNetUserLogin",
					schema: "security",
					columns: table => new {
						LoginProvider = table.Column<string>(nullable: false),
						ProviderKey = table.Column<string>(nullable: false),
						UserId = table.Column<int>(nullable: false),
						ProviderDisplayName = table.Column<string>(maxLength: 1000, nullable: true),
					},
					constraints: table => {
						table.PrimaryKey("PK_AspNetUserLogin", x => new { x.LoginProvider, x.ProviderKey });
						table.ForeignKey(
											name: "FK_AspNetUserLogin_AspNetUser_UserId",
											column: x => x.UserId,
											principalSchema: "security",
											principalTable: "AspNetUser",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "AspNetUserRole",
					schema: "security",
					columns: table => new {
						UserId = table.Column<int>(nullable: false),
						RoleId = table.Column<int>(nullable: false),
					},
					constraints: table => {
						table.PrimaryKey("PK_AspNetUserRole", x => new { x.UserId, x.RoleId });
						table.ForeignKey(
											name: "FK_AspNetUserRole_AspNetRole_RoleId",
											column: x => x.RoleId,
											principalSchema: "security",
											principalTable: "AspNetRole",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_AspNetUserRole_AspNetUser_UserId",
											column: x => x.UserId,
											principalSchema: "security",
											principalTable: "AspNetUser",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "AspNetUserToken",
					schema: "security",
					columns: table => new {
						UserId = table.Column<int>(nullable: false),
						LoginProvider = table.Column<string>(nullable: false),
						Name = table.Column<string>(nullable: false),
						Value = table.Column<string>(maxLength: 1000, nullable: true),
					},
					constraints: table => {
						table.PrimaryKey("PK_AspNetUserToken", x => new { x.UserId, x.LoginProvider, x.Name });
						table.ForeignKey(
											name: "FK_AspNetUserToken_AspNetUser_UserId",
											column: x => x.UserId,
											principalSchema: "security",
											principalTable: "AspNetUser",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "Record",
					schema: "data",
					columns: table => new {
						ID = table.Column<int>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						SurveyorID = table.Column<int>(nullable: false),
						SurveyTypeID = table.Column<int>(nullable: false),
						LocationID = table.Column<int>(nullable: false),
						SurveyYear = table.Column<decimal>(nullable: false),
						DeedVolume = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
						DeedPage = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
						County = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
						Township = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
						PlatID = table.Column<int>(nullable: false),
						OriginalLot = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
						City = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
						Subdivision = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
						Tract = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
						ParcelNumber = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
						Sublot = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
						Street = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
						ImageFileName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
						RecordingInfo = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
						Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
						MyProperty = table.Column<int>(nullable: false),
						ModifieddByID = table.Column<int>(nullable: false),
					},
					constraints: table => {
						table.PrimaryKey("PK_Record", x => x.ID);
						table.ForeignKey(
											name: "FK_Record_Location_LocationID",
											column: x => x.LocationID,
											principalSchema: "data",
											principalTable: "Location",
											principalColumn: "ID",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_Record_Plat_PlatID",
											column: x => x.PlatID,
											principalSchema: "data",
											principalTable: "Plat",
											principalColumn: "ID",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_Record_SurveyType_SurveyTypeID",
											column: x => x.SurveyTypeID,
											principalSchema: "ref",
											principalTable: "SurveyType",
											principalColumn: "ID",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_Record_AspNetUser_SurveyorID",
											column: x => x.SurveyorID,
											principalSchema: "security",
											principalTable: "AspNetUser",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "SurveyorAddress",
					schema: "xref",
					columns: table => new {
						ID = table.Column<int>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						SurveyorID = table.Column<int>(nullable: false),
						AddressID = table.Column<int>(nullable: false),
					},
					constraints: table => {
						table.PrimaryKey("PK_SurveyorAddress", x => x.ID);
						table.ForeignKey(
											name: "FK_SurveyorAddress_Address_AddressID",
											column: x => x.AddressID,
											principalSchema: "data",
											principalTable: "Address",
											principalColumn: "ID",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_SurveyorAddress_AspNetUser_SurveyorID",
											column: x => x.SurveyorID,
											principalSchema: "security",
											principalTable: "AspNetUser",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "SurveyorEmail",
					schema: "xref",
					columns: table => new {
						ID = table.Column<int>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						SurveyorID = table.Column<int>(nullable: false),
						EmailID = table.Column<int>(nullable: false),
					},
					constraints: table => {
						table.PrimaryKey("PK_SurveyorEmail", x => x.ID);
						table.ForeignKey(
											name: "FK_SurveyorEmail_Email_EmailID",
											column: x => x.EmailID,
											principalSchema: "data",
											principalTable: "Email",
											principalColumn: "ID",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_SurveyorEmail_AspNetUser_SurveyorID",
											column: x => x.SurveyorID,
											principalSchema: "security",
											principalTable: "AspNetUser",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "SurveyorPhone",
					schema: "xref",
					columns: table => new {
						ID = table.Column<int>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						SurveyorID = table.Column<int>(nullable: false),
						PhoneID = table.Column<int>(nullable: false),
					},
					constraints: table => {
						table.PrimaryKey("PK_SurveyorPhone", x => x.ID);
						table.ForeignKey(
											name: "FK_SurveyorPhone_Phone_PhoneID",
											column: x => x.PhoneID,
											principalSchema: "data",
											principalTable: "Phone",
											principalColumn: "ID",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_SurveyorPhone_AspNetUser_SurveyorID",
											column: x => x.SurveyorID,
											principalSchema: "security",
											principalTable: "AspNetUser",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateIndex(
					name: "IX_Address_AddressTypeID",
					schema: "data",
					table: "Address",
					column: "AddressTypeID");

			migrationBuilder.CreateIndex(
					name: "IX_Address_ApplicationUserId",
					schema: "data",
					table: "Address",
					column: "ApplicationUserId");

			migrationBuilder.CreateIndex(
					name: "IX_Email_ApplicationUserId",
					schema: "data",
					table: "Email",
					column: "ApplicationUserId");

			migrationBuilder.CreateIndex(
					name: "IX_Email_EmailTypeID",
					schema: "data",
					table: "Email",
					column: "EmailTypeID");

			migrationBuilder.CreateIndex(
					name: "IX_Location_LocationTypeID",
					schema: "data",
					table: "Location",
					column: "LocationTypeID");

			migrationBuilder.CreateIndex(
					name: "IX_Phone_ApplicationUserId",
					schema: "data",
					table: "Phone",
					column: "ApplicationUserId");

			migrationBuilder.CreateIndex(
					name: "IX_Phone_PhoneTypeID",
					schema: "data",
					table: "Phone",
					column: "PhoneTypeID");

			migrationBuilder.CreateIndex(
					name: "IX_Record_LocationID",
					schema: "data",
					table: "Record",
					column: "LocationID");

			migrationBuilder.CreateIndex(
					name: "IX_Record_PlatID",
					schema: "data",
					table: "Record",
					column: "PlatID");

			migrationBuilder.CreateIndex(
					name: "IX_Record_SurveyTypeID",
					schema: "data",
					table: "Record",
					column: "SurveyTypeID");

			migrationBuilder.CreateIndex(
					name: "IX_Record_SurveyorID",
					schema: "data",
					table: "Record",
					column: "SurveyorID");

			migrationBuilder.CreateIndex(
					name: "IX_Audit_AuditActionID",
					schema: "log",
					table: "Audit",
					column: "AuditActionID");

			migrationBuilder.CreateIndex(
					name: "IX_UserLogon_SurveyorID",
					schema: "log",
					table: "UserLogon",
					column: "SurveyorID");

			migrationBuilder.CreateIndex(
					name: "RoleNameIndex",
					schema: "security",
					table: "AspNetRole",
					column: "NormalizedName",
					unique: true,
					filter: "[NormalizedName] IS NOT NULL");

			migrationBuilder.CreateIndex(
					name: "IX_AspNetRoleClaim_RoleId",
					schema: "security",
					table: "AspNetRoleClaim",
					column: "RoleId");

			migrationBuilder.CreateIndex(
					name: "EmailIndex",
					schema: "security",
					table: "AspNetUser",
					column: "NormalizedEmail");

			migrationBuilder.CreateIndex(
					name: "UserNameIndex",
					schema: "security",
					table: "AspNetUser",
					column: "NormalizedUserName",
					unique: true,
					filter: "[NormalizedUserName] IS NOT NULL");

			migrationBuilder.CreateIndex(
					name: "IX_AspNetUserClaim_UserId",
					schema: "security",
					table: "AspNetUserClaim",
					column: "UserId");

			migrationBuilder.CreateIndex(
					name: "IX_AspNetUserLogin_UserId",
					schema: "security",
					table: "AspNetUserLogin",
					column: "UserId");

			migrationBuilder.CreateIndex(
					name: "IX_AspNetUserRole_RoleId",
					schema: "security",
					table: "AspNetUserRole",
					column: "RoleId");

			migrationBuilder.CreateIndex(
					name: "IX_SurveyorAddress_AddressID",
					schema: "xref",
					table: "SurveyorAddress",
					column: "AddressID");

			migrationBuilder.CreateIndex(
					name: "IX_SurveyorAddress_SurveyorID",
					schema: "xref",
					table: "SurveyorAddress",
					column: "SurveyorID");

			migrationBuilder.CreateIndex(
					name: "IX_SurveyorEmail_EmailID",
					schema: "xref",
					table: "SurveyorEmail",
					column: "EmailID");

			migrationBuilder.CreateIndex(
					name: "IX_SurveyorEmail_SurveyorID",
					schema: "xref",
					table: "SurveyorEmail",
					column: "SurveyorID");

			migrationBuilder.CreateIndex(
					name: "IX_SurveyorPhone_PhoneID",
					schema: "xref",
					table: "SurveyorPhone",
					column: "PhoneID");

			migrationBuilder.CreateIndex(
					name: "IX_SurveyorPhone_SurveyorID",
					schema: "xref",
					table: "SurveyorPhone",
					column: "SurveyorID");
		}

		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
					name: "ImagePath",
					schema: "data");

			migrationBuilder.DropTable(
					name: "Record",
					schema: "data");

			migrationBuilder.DropTable(
					name: "Audit",
					schema: "log");

			migrationBuilder.DropTable(
					name: "EventLog",
					schema: "log");

			migrationBuilder.DropTable(
					name: "UserLogon",
					schema: "log");

			migrationBuilder.DropTable(
					name: "AddressType",
					schema: "ref");

			migrationBuilder.DropTable(
					name: "LogOffType",
					schema: "ref");

			migrationBuilder.DropTable(
					name: "AspNetRoleClaim",
					schema: "security");

			migrationBuilder.DropTable(
					name: "AspNetUserClaim",
					schema: "security");

			migrationBuilder.DropTable(
					name: "AspNetUserLogin",
					schema: "security");

			migrationBuilder.DropTable(
					name: "AspNetUserRole",
					schema: "security");

			migrationBuilder.DropTable(
					name: "AspNetUserToken",
					schema: "security");

			migrationBuilder.DropTable(
					name: "SurveyorAddress",
					schema: "xref");

			migrationBuilder.DropTable(
					name: "SurveyorEmail",
					schema: "xref");

			migrationBuilder.DropTable(
					name: "SurveyorPhone",
					schema: "xref");

			migrationBuilder.DropTable(
					name: "Location",
					schema: "data");

			migrationBuilder.DropTable(
					name: "Plat",
					schema: "data");

			migrationBuilder.DropTable(
					name: "SurveyType",
					schema: "ref");

			migrationBuilder.DropTable(
					name: "AuditAction",
					schema: "ref");

			migrationBuilder.DropTable(
					name: "AspNetRole",
					schema: "security");

			migrationBuilder.DropTable(
					name: "Address",
					schema: "data");

			migrationBuilder.DropTable(
					name: "Email",
					schema: "data");

			migrationBuilder.DropTable(
					name: "Phone",
					schema: "data");

			migrationBuilder.DropTable(
					name: "LocationType",
					schema: "ref");

			migrationBuilder.DropTable(
					name: "EmailType",
					schema: "ref");

			migrationBuilder.DropTable(
					name: "AspNetUser",
					schema: "security");

			migrationBuilder.DropTable(
					name: "PhoneType",
					schema: "ref");
		}

	}
}

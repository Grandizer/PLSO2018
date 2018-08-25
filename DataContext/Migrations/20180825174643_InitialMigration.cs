using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PLSO2018.Entities;

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
						CreationDate = table.Column<DateTimeOffset>(nullable: false),
						CreatedByID = table.Column<int>(nullable: false),
					},
					constraints: table => {
						table.PrimaryKey("PK_ImagePath", x => x.ID);
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
						Page = table.Column<string>(maxLength: 255, nullable: true),
						Message = table.Column<string>(nullable: false),
						StackTrace = table.Column<string>(nullable: true),
						Occurred = table.Column<DateTimeOffset>(nullable: false),
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
						EnumName = table.Column<string>(type: "varchar(30)", nullable: false),
						Name = table.Column<string>(type: "varchar(50)", nullable: false),
						Description = table.Column<string>(type: "varchar(250)", nullable: true),
						CreationDate = table.Column<DateTimeOffset>(nullable: false),
						CreatedByID = table.Column<long>(nullable: false),
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
					name: "ExcelTemplate",
					schema: "ref",
					columns: table => new {
						ID = table.Column<int>(nullable: false)
									.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						ColumnIndex = table.Column<int>(nullable: false),
						IsCalculated = table.Column<bool>(nullable: false),
						IsRequired = table.Column<bool>(nullable: false),
						ColumnWidth = table.Column<int>(nullable: false),
						MaximumLength = table.Column<int>(nullable: false),
						FieldName = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
						DisplayName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
						Validation = table.Column<string>(unicode: false, maxLength: 1000, nullable: false),
						ExampleData = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
						ModifiedByID = table.Column<int>(nullable: false),
					},
					constraints: table => {
						table.PrimaryKey("PK_ExcelTemplate", x => x.ID);
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
						UserName = table.Column<string>(maxLength: 256, nullable: true),
						FirstName = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
						MiddleInitial = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
						LastName = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
						Number = table.Column<int>(nullable: false),
						Email = table.Column<string>(maxLength: 256, nullable: true),
						LastActivityDate = table.Column<DateTimeOffset>(nullable: false),
						PhoneNumber = table.Column<string>(nullable: true),
						Suffix = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
						SuffixShort = table.Column<string>(unicode: false, maxLength: 3, nullable: true),
						TwoFactorEnabled = table.Column<bool>(nullable: false),
						LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
						LockoutEnabled = table.Column<bool>(nullable: false),
						AccessFailedCount = table.Column<int>(nullable: false),
						PhoneNumberConfirmed = table.Column<bool>(nullable: false),
						EmailConfirmed = table.Column<bool>(nullable: false),
						PasswordHash = table.Column<string>(nullable: true),
						NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
						NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
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
						CreationDate = table.Column<DateTimeOffset>(nullable: false),
						CreatedByID = table.Column<long>(nullable: false),
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
						Latitude = table.Column<decimal>(type: "decimal(11, 8)", nullable: false),
						Longitude = table.Column<decimal>(type: "decimal(11, 8)", nullable: false),
						Address = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
						CreationDate = table.Column<DateTimeOffset>(nullable: false),
						CreatedByID = table.Column<int>(nullable: false),
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
						AddressTypeID = table.Column<int>(nullable: false),
						ApplicationUserId = table.Column<int>(nullable: true),
						IsPrimary = table.Column<bool>(nullable: false),
						CompanyName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
						Address1 = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
						Address2 = table.Column<string>(unicode: false, maxLength: 40, nullable: true),
						City = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
						State = table.Column<string>(unicode: false, maxLength: 2, nullable: false),
						Zip = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
						CreationDate = table.Column<DateTimeOffset>(nullable: false),
						CreatedByID = table.Column<int>(nullable: false),
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
						EmailTypeID = table.Column<int>(nullable: false),
						ApplicationUserId = table.Column<int>(nullable: true),
						IsPrimary = table.Column<bool>(nullable: false),
						Address = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
						CreationDate = table.Column<DateTimeOffset>(nullable: false),
						CreatedByID = table.Column<int>(nullable: false),
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
						PhoneTypeID = table.Column<int>(nullable: false),
						ApplicationUserId = table.Column<int>(nullable: true),
						IsPrimary = table.Column<bool>(nullable: false),
						Number = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
						Extension = table.Column<string>(unicode: false, maxLength: 6, nullable: true),
						CreationDate = table.Column<DateTimeOffset>(nullable: false),
						CreatedByID = table.Column<int>(nullable: false),
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
						LoggedOffByID = table.Column<int>(nullable: false),
						LoggedOffTypeID = table.Column<int>(nullable: false),
						LoggedIn = table.Column<DateTimeOffset>(nullable: false),
						LoggedOff = table.Column<DateTimeOffset>(nullable: false),
						RemoteAddress = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
						LocalAddress = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
						HttpUserAgent = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
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
						LocationID = table.Column<int>(nullable: true),
						UploadedByID = table.Column<int>(nullable: false),
						Active = table.Column<bool>(nullable: false),
						UploadedDate = table.Column<DateTimeOffset>(nullable: false),
						MapImageName = table.Column<string>(unicode: false, maxLength: 1000, nullable: false),
						ImageFileName = table.Column<string>(nullable: true),
						CityVillageTownship = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
						State = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
						County = table.Column<string>(unicode: false, maxLength: 25, nullable: false),
						DefunctTownship = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
						LotNumbers = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
						Section = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
						Tract = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
						Range = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
						SurveyDate = table.Column<DateTime>(nullable: false),
						SurveyorNumber = table.Column<string>(unicode: false, maxLength: 5, nullable: true),
						Address = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
						CrossStreet = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
						ParcelNumbers = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
						DeedPage = table.Column<int>(nullable: true),
						DeedVolume = table.Column<int>(nullable: true),
						AutomatedFileNumber = table.Column<string>(unicode: false, maxLength: 18, nullable: true),
						Subdivision = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
						Sublot = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
						SurveyName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
						ClientName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
						Notes = table.Column<string>(unicode: false, maxLength: 2000, nullable: true),
						ModifiedByID = table.Column<int>(nullable: false),
					},
					constraints: table => {
						table.PrimaryKey("PK_Record", x => x.ID);
						table.ForeignKey(
											name: "FK_Record_Location_LocationID",
											column: x => x.LocationID,
											principalSchema: "data",
											principalTable: "Location",
											principalColumn: "ID",
											onDelete: ReferentialAction.Restrict);
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


			// Seed some of the data
			DateTime Now = DateTime.Now;

			// Address Types
			migrationBuilder.InsertData(schema: "ref", table: nameof(AddressType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 1, "Unknown", null });
			migrationBuilder.InsertData(schema: "ref", table: nameof(AddressType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 2, "Residential", null });
			migrationBuilder.InsertData(schema: "ref", table: nameof(AddressType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 3, "Commercial", null });
			migrationBuilder.InsertData(schema: "ref", table: nameof(AddressType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 4, "Business", null });

			// Audit Actions
			migrationBuilder.InsertData(schema: "ref", table: nameof(AuditAction), columns: new[] { "ID", "IsActive", "Name", "EnumName", "Description", "CreatedByID", "CreationDate" }, values: new object[] { 1, true, "Add", "Add", null, 0, Now });
			migrationBuilder.InsertData(schema: "ref", table: nameof(AuditAction), columns: new[] { "ID", "IsActive", "Name", "EnumName", "Description", "CreatedByID", "CreationDate" }, values: new object[] { 2, true, "Update", "Update", null, 0, Now });
			migrationBuilder.InsertData(schema: "ref", table: nameof(AuditAction), columns: new[] { "ID", "IsActive", "Name", "EnumName", "Description", "CreatedByID", "CreationDate" }, values: new object[] { 3, true, "Delete", "Delete", null, 0, Now });
			migrationBuilder.InsertData(schema: "ref", table: nameof(AuditAction), columns: new[] { "ID", "IsActive", "Name", "EnumName", "Description", "CreatedByID", "CreationDate" }, values: new object[] { 4, true, "Manual Delete", "ManualDelete", null, 0, Now });

			// Email Types
			migrationBuilder.InsertData(schema: "ref", table: nameof(EmailType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 1, "Unknown", null });
			migrationBuilder.InsertData(schema: "ref", table: nameof(EmailType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 2, "Business", null });
			migrationBuilder.InsertData(schema: "ref", table: nameof(EmailType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 3, "Personal", null });
			migrationBuilder.InsertData(schema: "ref", table: nameof(EmailType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 4, "Other", null });

			// Location Types
			migrationBuilder.InsertData(schema: "ref", table: nameof(LocationType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 1, "Unknown", "Undetermined type of Location" });
			migrationBuilder.InsertData(schema: "ref", table: nameof(LocationType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 2, "LatLong", "Latitude and Lognitude" });
			migrationBuilder.InsertData(schema: "ref", table: nameof(LocationType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 3, "Address", "Address entred to get a LatLong" });

			// Log Off Types
			migrationBuilder.InsertData(schema: "ref", table: nameof(LogOffType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 1, "Unknown", "Undetermined style of Log Off" });
			migrationBuilder.InsertData(schema: "ref", table: nameof(LogOffType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 2, "Normal", "The user clicked the Log Off link" });
			migrationBuilder.InsertData(schema: "ref", table: nameof(LogOffType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 3, "Timed Out", "The user timed out and was automatically logged off" });
			migrationBuilder.InsertData(schema: "ref", table: nameof(LogOffType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 4, "Lazy Timeout", "The user was logged out of an old session at the time of their new log in" });
			migrationBuilder.InsertData(schema: "ref", table: nameof(LogOffType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 5, "Forced Log Off", "The UserLogon record was updated by an admin to allow them to log back in" });

			// Phone Types
			migrationBuilder.InsertData(schema: "ref", table: nameof(PhoneType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 1, "Unknown", null });
			migrationBuilder.InsertData(schema: "ref", table: nameof(PhoneType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 2, "Business", null });
			migrationBuilder.InsertData(schema: "ref", table: nameof(PhoneType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 3, "Home", null });
			migrationBuilder.InsertData(schema: "ref", table: nameof(PhoneType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 4, "Mobile", null });
			migrationBuilder.InsertData(schema: "ref", table: nameof(PhoneType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 5, "Facsimile", null });

			// Survey Types
			migrationBuilder.InsertData(schema: "ref", table: nameof(SurveyType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 1, "Unknown", null });
			migrationBuilder.InsertData(schema: "ref", table: nameof(SurveyType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 2, "Boundary", null });
			migrationBuilder.InsertData(schema: "ref", table: nameof(SurveyType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 3, "Topo", null });
			migrationBuilder.InsertData(schema: "ref", table: nameof(SurveyType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 4, "ALTA", null });
			migrationBuilder.InsertData(schema: "ref", table: nameof(SurveyType), columns: new[] { "ID", "Name", "Description" }, values: new object[] { 5, "MLS", null });

			// Excel Template Fields
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 1, 1, 0, 25, true, false, "Internal Use Only", "*** Parsing Messages", "Do Not Use", "This is the field where parsing messages will go when this file is uploaded.", 0 });
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 2, 2, 0, 25, true, false, "Comments", "*** Comments", "Anything", "Any row with contents in this field will be IGNORED", 0 });
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 3, 3, 15, 25, false, true, "Map Image Name", nameof(Record.ImageFileName), "KRAUSE00099", "No spaces, hyphens. 5 digit numerical counter. MUST BE EXACT", 0 });
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 4, 4, 25, 30, false, true, "City, Village, Township", nameof(Record.CityVillageTownship), "Bay Village", "Spaces are OK", 0 });
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 5, 5, 15, 25, false, true, "County", nameof(Record.County), "Cuyahoga", "Full county name because first 3 letters are duplicated", 0 });
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 6, 6, 15, 30, false, true, "Defunct or Historic Township", nameof(Record.DefunctTownship), "Dover", "Full name. No need to include the word township", 0 });
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 7, 7, 8, 25, false, false, "Lot No.", nameof(Record.LotNumbers), "10,11,12", "Only Numbers with multiples seperated by commas, no spaces", 0 });
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 8, 8, 8, 25, false, false, "Section", nameof(Record.Section), "10,11,12", "Only Numbers with multiples seperated by commas, no spaces", 0 });
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 9, 9, 8, 25, false, false, "Tract", nameof(Record.Tract), "10,11,12,Blake,Ely", "", 0 });
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 10, 10, 8, 25, false, false, "Range", nameof(Record.Range), "2N", "Enter if known", 0 });
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 11, 11, 10, 20, true, true, "Survey Date", nameof(Record.SurveyDate), "10/1/2018", "In the format of MM/DD/YYYY.  If the Day is not know, put a 1", 0 });
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 12, 12, 25, 25, true, true, "Surveyor Name", "*** Surveyor Name", "Frank B Krause Jr", "Full name because there are repeated names like Krause", 0 });
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 13, 13, 5, 25, true, false, "Surveyor Number", "*** Surveyor Number", "567", "Only Numbers", 0 });
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 14, 14, 100, 20, false, true, "Address", nameof(Record.Address), "12345A Southwest New Philly", "This should contain the full street address", 0 });
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 15, 15, 30, 25, false, false, "Cross Street Name", nameof(Record.CrossStreet), "NW MAIN ST", "In case of fronting more than one street or corner lots, place second street name here. Entire name can be in this field. If there are more streets like a subdivision plat, those can be listed under the Notes fields.", 0 });
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 16, 16, 50, 25, false, false, "Parcel Numbers", nameof(Record.ParcelNumbers), "123-45-678,123-45-679", "Numbers and letters depending on county. Separate multiple parcel numbers with commas. Enter entire parcel number, not just ending suffix, no spaces", 0 });
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 17, 17, 8, 20, false, false, "Volume", nameof(Record.DeedVolume), "123", "Numbers Only", 0 });
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 18, 18, 15, 20, false, false, "Page", nameof(Record.DeedPage), "45", "Numbers Only", 0 });
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 19, 19, 18, 20, false, false, "AFN/Inst#/Recorder#/Doc#", nameof(Record.AutomatedFileNumber), "20111965485", "Enter the full Recorder/Auditor unique identifier", 0 });
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 20, 20, 50, 25, false, false, "Subdivision", nameof(Record.Subdivision), "Barrington", "Full subdivision name", 0 });
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 21, 21, 10, 20, false, false, "Subdivision-Sublot", nameof(Record.Sublot), "10,11,12", "Numbers only. Multiples separated by commas. No spaces", 0 });
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 22, 22, 50, 25, false, false, "Survey Name", nameof(Record.SurveyName), "Lot split for Lustri", "This should be the title as it appears on the map, we shouldn't interpret mistakes here. Include lables like Boundary, Topo, ALTA, MLS, etc.", 0 });
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 23, 23, 30, 25, true, false, "Location", nameof(Record.Location), "41.498924,-81.689951", "If known, enter the Latitude and Longitude separated by commas", 0 });
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 24, 24, 50, 25, false, false, "Client", nameof(Record.ClientName), "Dino A F Lustri", "Full Client name as it appears on the Survey Map. No periods. Include middle initials when applicable", 0 });
			migrationBuilder.InsertData(schema: "ref", table: nameof(ExcelTemplate), columns: new[] { "ID", "ColumnIndex", "MaximumLength", "ColumnWidth", "IsCalculated", "IsRequired", "DisplayName", "FieldName", "ExampleData", "Validation", "ModifiedByID" }, values: new object[] { 25, 25, 1000, 25, false, false, "Notes", nameof(Record.Notes), "Just behind the McDonald's", "Place any other info here that would aid in searching", 0 });

			/*

			The following SQL will make the data.Record table a Temporal table.
			https://msdn.microsoft.com/en-us/magazine/mt795184.aspx?f=255&MSPPError=-2147217396

			ALTER TABLE
				data.Record
			ADD
				SysStartTime datetime2(0)
			GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT DF_Record_SysStart DEFAULT SYSUTCDATETIME(),
				SysEndTime datetime2(0)
			GENERATED ALWAYS AS ROW END HIDDEN CONSTRAINT DF_Record_SysEnd DEFAULT CONVERT(datetime2 (0), '9999-12-31 23:59:59'),
				PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime)
			ALTER TABLE
				data.Record
			SET
				(SYSTEM_VERSIONING = ON (HISTORY_TABLE = log.RecordHistory))

			*/
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
					name: "ExcelTemplate",
					schema: "ref");

			migrationBuilder.DropTable(
					name: "LogOffType",
					schema: "ref");

			migrationBuilder.DropTable(
					name: "SurveyType",
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

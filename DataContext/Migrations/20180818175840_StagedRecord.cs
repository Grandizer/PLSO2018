using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations {

	public partial class StagedRecord : Migration {

		protected override void Up(MigrationBuilder migrationBuilder) {

			migrationBuilder.EnsureSchema(
					name: "temp");

			migrationBuilder.AlterColumn<string>(
					name: "Message",
					schema: "log",
					table: "EventLog",
					nullable: false,
					oldClrType: typeof(string),
					oldMaxLength: 2000);

			migrationBuilder.CreateTable(
					name: "StagedRecord",
					schema: "temp",
					columns: table => new {
						ID = table.Column<int>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
						SurveyorID = table.Column<int>(nullable: false),
						LocationID = table.Column<int>(nullable: false),
						ImageFileName = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
						City = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
						County = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
						State = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
						Township = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
						OriginalLot = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
						Section = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
						Tract = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
						Range = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
						SurveyDate = table.Column<DateTime>(nullable: false),
						Address = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
						CrossStreet = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
						ParcelNumber = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
						DeedVolume = table.Column<int>(nullable: true),
						DeedPage = table.Column<int>(nullable: true),
						AutomatedFileNumber = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
						Subdivision = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
						Sublot = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
						SurveyName = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
						ClientName = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
						Description = table.Column<string>(unicode: false, maxLength: 2000, nullable: true),
						CreationDate = table.Column<DateTimeOffset>(nullable: false),
						CreatedByID = table.Column<int>(nullable: false),
					},
					constraints: table => {
						table.PrimaryKey("PK_StagedRecord", x => x.ID);
						table.ForeignKey(
											name: "FK_StagedRecord_Location_LocationID",
											column: x => x.LocationID,
											principalSchema: "data",
											principalTable: "Location",
											principalColumn: "ID",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_StagedRecord_AspNetUser_SurveyorID",
											column: x => x.SurveyorID,
											principalSchema: "security",
											principalTable: "AspNetUser",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateIndex(
					name: "IX_StagedRecord_LocationID",
					schema: "temp",
					table: "StagedRecord",
					column: "LocationID");

			migrationBuilder.CreateIndex(
					name: "IX_StagedRecord_SurveyorID",
					schema: "temp",
					table: "StagedRecord",
					column: "SurveyorID");
		}

		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
					name: "StagedRecord",
					schema: "temp");

			migrationBuilder.AlterColumn<string>(
					name: "Message",
					schema: "log",
					table: "EventLog",
					maxLength: 2000,
					nullable: false,
					oldClrType: typeof(string));
		}

	}
}

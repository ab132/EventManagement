using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventManagement.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfGuests = table.Column<int>(type: "int", nullable: true),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LegalPersons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfGuests = table.Column<int>(type: "int", nullable: false),
                    PaymentOption = table.Column<int>(type: "int", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    EventId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalPersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalPersons_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivateGuests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalIdentificationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentOption = table.Column<int>(type: "int", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    EventId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateGuests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivateGuests_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AdditionalInfo", "Date", "EventName", "NumberOfGuests", "Venue" },
                values: new object[,]
                {
                    { 1, "Additional info 1", new DateTime(2021, 10, 27, 20, 6, 36, 830, DateTimeKind.Local).AddTicks(5391), "Event1", 0, "Venue1" },
                    { 2, "Additional info 2", new DateTime(2021, 10, 30, 20, 6, 36, 838, DateTimeKind.Local).AddTicks(6834), "Event2", 0, "Venue2" },
                    { 3, "Additional info 3", new DateTime(2021, 11, 1, 20, 6, 36, 838, DateTimeKind.Local).AddTicks(6915), "Event3", 0, "Venue3" },
                    { 4, "Additional info 4", new DateTime(2021, 11, 4, 20, 6, 36, 838, DateTimeKind.Local).AddTicks(6924), "Event4", 0, "Venue4" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LegalPersons_EventId",
                table: "LegalPersons",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateGuests_EventId",
                table: "PrivateGuests",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LegalPersons");

            migrationBuilder.DropTable(
                name: "PrivateGuests");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}

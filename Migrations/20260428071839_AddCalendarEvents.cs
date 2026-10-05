using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class AddCalendarEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalendarEvents",
                columns: table => new
                {
                    CalendarEventId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EventDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EventTime = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEvents", x => x.CalendarEventId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: "APT001",
                column: "CreationDate",
                value: new DateTime(2026, 4, 28, 8, 18, 38, 600, DateTimeKind.Local).AddTicks(8313));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: "APT002",
                column: "CreationDate",
                value: new DateTime(2026, 4, 28, 8, 18, 38, 600, DateTimeKind.Local).AddTicks(8358));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarEvents");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: "APT001",
                column: "CreationDate",
                value: new DateTime(2026, 4, 28, 7, 58, 19, 485, DateTimeKind.Local).AddTicks(9899));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: "APT002",
                column: "CreationDate",
                value: new DateTime(2026, 4, 28, 7, 58, 19, 485, DateTimeKind.Local).AddTicks(9946));
        }
    }
}

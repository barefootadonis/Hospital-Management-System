using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDateTimetoString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EventDate",
                table: "CalendarEvents",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: "APT001",
                column: "CreationDate",
                value: new DateTime(2026, 4, 28, 8, 21, 52, 718, DateTimeKind.Local).AddTicks(3493));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: "APT002",
                column: "CreationDate",
                value: new DateTime(2026, 4, 28, 8, 21, 52, 718, DateTimeKind.Local).AddTicks(3553));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EventDate",
                table: "CalendarEvents",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)")
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
    }
}

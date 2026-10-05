using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class AddResultDetailsToLabTests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResultDetails",
                table: "LabTestResults",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: "APT001",
                column: "CreationDate",
                value: new DateTime(2026, 4, 27, 20, 38, 1, 171, DateTimeKind.Local).AddTicks(3863));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: "APT002",
                column: "CreationDate",
                value: new DateTime(2026, 4, 27, 20, 38, 1, 171, DateTimeKind.Local).AddTicks(3916));

            migrationBuilder.UpdateData(
                table: "LabTestResults",
                keyColumn: "TestId",
                keyValue: "LAB001",
                column: "ResultDetails",
                value: "Blood pressure markers reviewed. No urgent abnormality detected.");

            migrationBuilder.UpdateData(
                table: "LabTestResults",
                keyColumn: "TestId",
                keyValue: "LAB002",
                column: "ResultDetails",
                value: "Draft result pending final review before doctor access.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultDetails",
                table: "LabTestResults");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: "APT001",
                column: "CreationDate",
                value: new DateTime(2026, 4, 27, 18, 53, 14, 806, DateTimeKind.Local).AddTicks(6106));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: "APT002",
                column: "CreationDate",
                value: new DateTime(2026, 4, 27, 18, 53, 14, 806, DateTimeKind.Local).AddTicks(6156));
        }
    }
}

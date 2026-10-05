using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class AddPrescriptionStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Prescriptions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "PrescriptionId",
                keyValue: "PRE001",
                column: "Status",
                value: "Pending");

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "PrescriptionId",
                keyValue: "PRE002",
                column: "Status",
                value: "Pending");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Prescriptions");

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
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class NotificationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendToAllStaff",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverType",
                table: "Notifications",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SenderType",
                table: "Notifications",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: "APT001",
                column: "CreationDate",
                value: new DateTime(2026, 4, 27, 17, 57, 4, 157, DateTimeKind.Local).AddTicks(8450));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: "APT002",
                column: "CreationDate",
                value: new DateTime(2026, 4, 27, 17, 57, 4, 157, DateTimeKind.Local).AddTicks(8499));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverType",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "SenderType",
                table: "Notifications");

            migrationBuilder.AddColumn<bool>(
                name: "SendToAllStaff",
                table: "Notifications",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: "APT001",
                column: "CreationDate",
                value: new DateTime(2026, 4, 27, 17, 50, 8, 370, DateTimeKind.Local).AddTicks(9522));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: "APT002",
                column: "CreationDate",
                value: new DateTime(2026, 4, 27, 17, 50, 8, 370, DateTimeKind.Local).AddTicks(9573));
        }
    }
}

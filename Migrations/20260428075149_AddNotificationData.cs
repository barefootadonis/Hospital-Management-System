using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: "APT001",
                column: "CreationDate",
                value: new DateTime(2026, 4, 28, 8, 51, 48, 758, DateTimeKind.Local).AddTicks(8014));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: "APT002",
                column: "CreationDate",
                value: new DateTime(2026, 4, 28, 8, 51, 48, 758, DateTimeKind.Local).AddTicks(8069));

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "NotificationId", "CreatedAt", "IsRead", "Message", "ReceiverId", "ReceiverType", "SenderId", "SenderType", "Title" },
                values: new object[,]
                {
                    { "NOT001", new DateTime(2026, 4, 28, 9, 0, 0, 0, DateTimeKind.Unspecified), false, "Staff meeting on Friday at 10am.", null, "AllStaff", "SMA001", "Staff", "Staff Meeting" },
                    { "NOT002", new DateTime(2026, 4, 28, 10, 0, 0, 0, DateTimeKind.Unspecified), false, "Please remember your upcoming appointment.", "PAT001", "Patient", "DOC001", "Staff", "Appointment Reminder" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "NotificationId",
                keyValue: "NOT001");

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "NotificationId",
                keyValue: "NOT002");

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
    }
}

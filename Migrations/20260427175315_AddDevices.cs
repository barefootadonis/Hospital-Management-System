using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class AddDevices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeviceName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeviceType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AssignedStaffId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Room = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastUpdated = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_Devices_Staff_AssignedStaffId",
                        column: x => x.AssignedStaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "DeviceId", "AssignedStaffId", "DeviceName", "DeviceType", "LastUpdated", "Room", "Status" },
                values: new object[,]
                {
                    { "DEV001", "DOC001", "Dell Latitude 7420", "Laptop", new DateTime(2026, 4, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), null, "In Use" },
                    { "DEV002", "NUR001", "HP EliteBook 840", "Laptop", new DateTime(2026, 4, 20, 10, 30, 0, 0, DateTimeKind.Unspecified), null, "In Use" },
                    { "DEV003", null, "iPad Pro 12.9", "Tablet", new DateTime(2026, 4, 21, 11, 0, 0, 0, DateTimeKind.Unspecified), "Ward A - Room 1", "In Use" },
                    { "DEV004", null, "iPad Mini", "Tablet", new DateTime(2026, 4, 21, 12, 15, 0, 0, DateTimeKind.Unspecified), "Ward B - Room 3", "Available" },
                    { "DEV005", null, "Lenovo ThinkPad X1", "Laptop", new DateTime(2026, 4, 22, 8, 45, 0, 0, DateTimeKind.Unspecified), null, "Available" },
                    { "DEV006", null, "Patient Vitals Monitor", "Medical Device", new DateTime(2026, 4, 22, 14, 0, 0, 0, DateTimeKind.Unspecified), "ICU - Bed 2", "In Use" },
                    { "DEV007", null, "Samsung Tablet S8", "Tablet", new DateTime(2026, 4, 23, 9, 30, 0, 0, DateTimeKind.Unspecified), "Ward A - Room 2", "Faulty" },
                    { "DEV008", null, "Barcode Scanner ZX-300", "Scanner", new DateTime(2026, 4, 23, 13, 10, 0, 0, DateTimeKind.Unspecified), "Pharmacy", "Available" },
                    { "DEV009", "ITP001", "MacBook Air M2", "Laptop", new DateTime(2026, 4, 24, 10, 0, 0, 0, DateTimeKind.Unspecified), null, "In Use" },
                    { "DEV010", "DOC002", "Surface Pro 9", "Tablet/Laptop", new DateTime(2026, 4, 24, 15, 20, 0, 0, DateTimeKind.Unspecified), null, "Faulty" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_AssignedStaffId",
                table: "Devices",
                column: "AssignedStaffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");

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
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class MoreDummyData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDraft",
                table: "Vitals",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: "APT001",
                column: "CreationDate",
                value: new DateTime(2026, 4, 23, 12, 32, 52, 796, DateTimeKind.Local).AddTicks(412));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: "APT002",
                column: "CreationDate",
                value: new DateTime(2026, 4, 23, 12, 32, 52, 796, DateTimeKind.Local).AddTicks(463));

            migrationBuilder.InsertData(
                table: "Visits",
                columns: new[] { "VisitId", "AppointmentId", "DoctorId", "Notes", "PatientId", "VisitDateTime" },
                values: new object[,]
                {
                    { "VIS001", "APT001", "DOC001", "Initial consultation after patient arrival.", "PAT001", new DateTime(2026, 9, 18, 9, 30, 0, 0, DateTimeKind.Unspecified) },
                    { "VIS002", "APT002", "DOC001", "Follow-up consultation.", "PAT002", new DateTime(2027, 4, 18, 11, 30, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Diagnoses",
                columns: new[] { "DiagnosisId", "Description", "DiagnosisDate", "DiagnosisName", "VisitId" },
                values: new object[,]
                {
                    { "DIA001", "Patient advised to monitor blood pressure and reduce salt intake.", new DateTime(2026, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mild Hypertension", "VIS001" },
                    { "DIA002", "Symptoms consistent with seasonal allergic rhinitis.", new DateTime(2027, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seasonal Allergies", "VIS002" }
                });

            migrationBuilder.InsertData(
                table: "LabTestResults",
                columns: new[] { "TestId", "LabTechId", "ResultDate", "Status", "TestType", "UploadDate", "VisitId" },
                values: new object[,]
                {
                    { "LAB001", "LBT001", new DateTime(2026, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", "Blood Test", new DateTime(2026, 9, 19, 14, 0, 0, 0, DateTimeKind.Unspecified), "VIS001" },
                    { "LAB002", "LBT001", new DateTime(2027, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "Allergy Panel", new DateTime(2027, 4, 19, 10, 15, 0, 0, DateTimeKind.Unspecified), "VIS002" }
                });

            migrationBuilder.InsertData(
                table: "Prescriptions",
                columns: new[] { "PrescriptionId", "Notes", "PharmacistId", "PrescriptionText", "VisitId" },
                values: new object[,]
                {
                    { "PRE001", "Review needed in 2 weeks.", "PHA001", "Boscopan 5mg once daily", "VIS001" },
                    { "PRE002", "Take as needed during allergy season.", "PHA001", "Cetirizine 10mg once daily", "VIS002" }
                });

            migrationBuilder.InsertData(
                table: "Vitals",
                columns: new[] { "VitalsId", "BloodPressure", "HeartRate", "IsDraft", "Notes", "NurseId", "RecordedAt", "RespiratoryRate", "Temperature", "VisitId" },
                values: new object[,]
                {
                    { "VIT001", "140/90", "82 bpm", false, "Patient stable but blood pressure slightly elevated.", "NUR001", new DateTime(2026, 9, 18, 9, 15, 0, 0, DateTimeKind.Unspecified), "18 breaths/min", 37, "VIS001" },
                    { "VIT002", "118/76", "74 bpm", true, "Vitals within normal range.", "NUR001", new DateTime(2027, 4, 18, 11, 15, 0, 0, DateTimeKind.Unspecified), "16 breaths/min", 36, "VIS002" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Diagnoses",
                keyColumn: "DiagnosisId",
                keyValue: "DIA001");

            migrationBuilder.DeleteData(
                table: "Diagnoses",
                keyColumn: "DiagnosisId",
                keyValue: "DIA002");

            migrationBuilder.DeleteData(
                table: "LabTestResults",
                keyColumn: "TestId",
                keyValue: "LAB001");

            migrationBuilder.DeleteData(
                table: "LabTestResults",
                keyColumn: "TestId",
                keyValue: "LAB002");

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "PrescriptionId",
                keyValue: "PRE001");

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "PrescriptionId",
                keyValue: "PRE002");

            migrationBuilder.DeleteData(
                table: "Vitals",
                keyColumn: "VitalsId",
                keyValue: "VIT001");

            migrationBuilder.DeleteData(
                table: "Vitals",
                keyColumn: "VitalsId",
                keyValue: "VIT002");

            migrationBuilder.DeleteData(
                table: "Visits",
                keyColumn: "VisitId",
                keyValue: "VIS001");

            migrationBuilder.DeleteData(
                table: "Visits",
                keyColumn: "VisitId",
                keyValue: "VIS002");

            migrationBuilder.DropColumn(
                name: "IsDraft",
                table: "Vitals");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: "APT001",
                column: "CreationDate",
                value: new DateTime(2026, 4, 17, 12, 34, 24, 513, DateTimeKind.Local).AddTicks(3468));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: "APT002",
                column: "CreationDate",
                value: new DateTime(2026, 4, 17, 12, 34, 24, 513, DateTimeKind.Local).AddTicks(3518));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    StaffId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StaffFirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StaffLastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StaffOtherNames = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StaffRole = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StaffDateOfBirth = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    StaffAddress = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StaffPhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StaffEmail = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StaffEmergencyContactName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StaffEmergencyContactPhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ActiveStatus = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.StaffId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PatientFirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PatientLastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PatientOtherNames = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PatientDateOfBirth = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PatientAddress = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PatientPhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PatientEmail = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PatientEmergencyContactName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PatientEmergencyContactPhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AssignedDoctorId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patients_Staff_AssignedDoctorId",
                        column: x => x.AssignedDoctorId,
                        principalTable: "Staff",
                        principalColumn: "StaffId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AppointmentDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AppointmentTime = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AppointmentStatus = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ArrivalConfirmation = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Notes = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PatientId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DoctorId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Staff_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountStatus = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastLogin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    StaffId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PatientId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_UserAccounts_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId");
                    table.ForeignKey(
                        name: "FK_UserAccounts_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    VisitId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VisitDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Notes = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PatientId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DoctorId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AppointmentId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.VisitId);
                    table.ForeignKey(
                        name: "FK_Visits_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_Staff_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Diagnoses",
                columns: table => new
                {
                    DiagnosisId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DiagnosisName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DiagnosisDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    VisitId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnoses", x => x.DiagnosisId);
                    table.ForeignKey(
                        name: "FK_Diagnoses_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LabTestResults",
                columns: table => new
                {
                    TestId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TestType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ResultDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UploadDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    VisitId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LabTechId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTestResults", x => x.TestId);
                    table.ForeignKey(
                        name: "FK_LabTestResults_Staff_LabTechId",
                        column: x => x.LabTechId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabTestResults_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    PrescriptionId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PrescriptionText = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VisitId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PharmacistId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.PrescriptionId);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Staff_PharmacistId",
                        column: x => x.PharmacistId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Vitals",
                columns: table => new
                {
                    VitalsId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Temperature = table.Column<int>(type: "int", nullable: false),
                    BloodPressure = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HeartRate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RespiratoryRate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RecordedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    VisitId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NurseId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vitals", x => x.VitalsId);
                    table.ForeignKey(
                        name: "FK_Vitals_Staff_NurseId",
                        column: x => x.NurseId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vitals_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "StaffId", "ActiveStatus", "StaffAddress", "StaffDateOfBirth", "StaffEmail", "StaffEmergencyContactName", "StaffEmergencyContactPhoneNumber", "StaffFirstName", "StaffLastName", "StaffOtherNames", "StaffPhoneNumber", "StaffRole", "StartDate" },
                values: new object[,]
                {
                    { "DOC001", "Active", "1 Hospital Way, London", new DateTime(1985, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "amelia.hart@gmail.com", "John Hart", "07987654321", "Amelia", "Hart", "Gracie", "07123456789", "Doctor", new DateTime(2020, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "DOC002", "Active", "12 Westminster street, London", new DateTime(2000, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "JamesAdeoye@hms.co.uk", "Adeola Bankole", "00182936673", "James", "Adeoye", null, "07853295100", "Doctor", new DateTime(2025, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "ITP001", "Active", "123 Tech Lane, London", new DateTime(1992, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "DavidITWhiz@hsm.co.uk", "James Brown", "07987654321", "David", "Brown", "Alexander", "07123456789", "IT Personnel", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "LBT001", "Active", "34 The Maltings, London", new DateTime(1990, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "JacobBradford@yahoo.com", "Jackson Bradford", "03569829334", "Jacob", "Bradford", "Ezekiel", "09567345128", "Lab Technician", new DateTime(2015, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "NUR001", "Active", "12 Lockerbie road, London", new DateTime(1965, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "AmandaChigozeE@gmail.com", "Amarachi Anyagu", "07911282556", "Amanda", "Chigoze", "Eberechukwu", "07911873421", "Nurse", new DateTime(2000, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "PHA001", "Active", "90 Elm Street, London", new DateTime(1985, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "GraceWilliams@hsm.co.uk", "Sophia Williams", "07987654321", "Olivia", "Williams", "Grace", "07654321098", "Pharmacist", new DateTime(2019, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "REP001", "Active", "56 High Street, London", new DateTime(1995, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "EmilySmith@hms.co.uk", "Sarah Smith", "07987654321", "Emily", "Smith", "Rose", "07451239876", "Receptionist", new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "SMA001", "Active", "78 Park Avenue, London", new DateTime(1980, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "JohnsonJonson13@hsm.co.uk", "Laura Johnson", "07912345678", "Michael", "Johnson", null, "07345678901", "System Administrator", new DateTime(2018, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "AssignedDoctorId", "CreationDate", "PatientAddress", "PatientDateOfBirth", "PatientEmail", "PatientEmergencyContactName", "PatientEmergencyContactPhoneNumber", "PatientFirstName", "PatientLastName", "PatientOtherNames", "PatientPhoneNumber" },
                values: new object[,]
                {
                    { "PAT001", "DOC001", new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "123 Main Street, London", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Johndabomb@yahoo.com", "Jane Doe", "07987654321", "John", "Doe", "Michael", "07123456789" },
                    { "PAT002", "DOC002", new DateTime(2023, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "456 Elm Street, London", new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "MilleSmith3@gmail.com", "Emily Smith", "07123456789", "Millie", "Smith", null, "07987654321" }
                });

            migrationBuilder.InsertData(
                table: "UserAccounts",
                columns: new[] { "AccountId", "AccountStatus", "Email", "LastLogin", "Password", "PatientId", "Role", "StaffId" },
                values: new object[,]
                {
                    { 2, "Active", "GraceWilliams@hsm.co.uk", new DateTime(2026, 2, 5, 8, 10, 12, 0, DateTimeKind.Unspecified), "Gracie2456", null, "Pharmacist", "PHA001" },
                    { 3, "Active", "EmilySmith@hms.co.uk", new DateTime(2026, 2, 5, 9, 15, 30, 0, DateTimeKind.Unspecified), "Emily2021", null, "Receptionist", "REP001" },
                    { 5, "Active", "amelia.hart@gmail.com", new DateTime(2026, 2, 5, 11, 30, 0, 0, DateTimeKind.Unspecified), "nAkEdInMaNhAtTaN", null, "Doctor", "DOC001" },
                    { 6, "Active", "JamesAdeoye@hms.co.uk", new DateTime(2026, 2, 5, 12, 45, 15, 0, DateTimeKind.Unspecified), "2026hmsyay", null, "Doctor", "DOC002" },
                    { 7, "Active", "AmandaChigozeE@gmail.com", new DateTime(2026, 2, 5, 13, 50, 30, 0, DateTimeKind.Unspecified), "NurseAmanda2026", null, "Nurse", "NUR001" },
                    { 8, "Active", "JacobBradford@yahoo.com", new DateTime(2026, 2, 5, 14, 55, 45, 0, DateTimeKind.Unspecified), "AbbottonAbbottonAbbott", null, "Lab Technician", "LBT001" },
                    { 9, "Active", "JohnsonJonson13@hsm.co.uk", new DateTime(2026, 2, 5, 15, 0, 0, 0, DateTimeKind.Unspecified), "LuckyJohnson", null, "System Administrator", "SMA001" },
                    { 10, "Active", "DavidITWhiz@hsm.co.uk", new DateTime(2026, 2, 5, 16, 10, 15, 0, DateTimeKind.Unspecified), "+lol^^cantguessmypassword", null, "IT Personnel", "ITP001" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentId", "AppointmentDate", "AppointmentStatus", "AppointmentTime", "ArrivalConfirmation", "CreationDate", "DoctorId", "Notes", "PatientId" },
                values: new object[,]
                {
                    { "APT001", new DateTime(2026, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Scheduled", "09:00 AM", false, new DateTime(2026, 4, 17, 12, 34, 24, 513, DateTimeKind.Local).AddTicks(3468), "DOC001", "Routine checkup", "PAT001" },
                    { "APT002", new DateTime(2027, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Scheduled", "11:00 AM", false, new DateTime(2026, 4, 17, 12, 34, 24, 513, DateTimeKind.Local).AddTicks(3518), "DOC001", "Follow-up", "PAT002" }
                });

            migrationBuilder.InsertData(
                table: "UserAccounts",
                columns: new[] { "AccountId", "AccountStatus", "Email", "LastLogin", "Password", "PatientId", "Role", "StaffId" },
                values: new object[,]
                {
                    { 1, "Active", "Millie Smith", new DateTime(2026, 2, 5, 7, 0, 0, 0, DateTimeKind.Unspecified), "#90ndjj3", "PAT002", "Patient", null },
                    { 4, "Active", "Johndabomb@yahoo.com", new DateTime(2025, 2, 5, 10, 20, 45, 0, DateTimeKind.Unspecified), "LawandOrdersvu", "PAT001", "Patient", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_VisitId",
                table: "Diagnoses",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_LabTestResults_LabTechId",
                table: "LabTestResults",
                column: "LabTechId");

            migrationBuilder.CreateIndex(
                name: "IX_LabTestResults_VisitId",
                table: "LabTestResults",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_AssignedDoctorId",
                table: "Patients",
                column: "AssignedDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PharmacistId",
                table: "Prescriptions",
                column: "PharmacistId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_VisitId",
                table: "Prescriptions",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_PatientId",
                table: "UserAccounts",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_StaffId",
                table: "UserAccounts",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_AppointmentId",
                table: "Visits",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_DoctorId",
                table: "Visits",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_PatientId",
                table: "Visits",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Vitals_NurseId",
                table: "Vitals",
                column: "NurseId");

            migrationBuilder.CreateIndex(
                name: "IX_Vitals_VisitId",
                table: "Vitals",
                column: "VisitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.DropTable(
                name: "LabTestResults");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "UserAccounts");

            migrationBuilder.DropTable(
                name: "Vitals");

            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Staff");
        }
    }
}

using Hospital_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Staff>().HasData(
                new Staff
                {
                    StaffId = "DOC001",
                    StaffFirstName = "Amelia",
                    StaffLastName = "Hart",
                    StaffOtherNames = "Gracie",
                    StaffRole = "Doctor",
                    StaffDateOfBirth = new DateTime(1985, 6, 15),
                    StaffAddress = "1 Hospital Way, London",
                    StaffPhoneNumber = "07123456789",
                    StaffEmail = "amelia.hart@gmail.com",
                    StaffEmergencyContactName = "John Hart",
                    StaffEmergencyContactPhoneNumber = "07987654321",
                    ActiveStatus = "Active",
                    StartDate = new DateTime(2020, 1, 10)
                }
            );
            builder.Entity<Staff>().HasData(
                new Staff
                {
                    StaffId = "DOC002",
                    StaffFirstName = "James",
                    StaffLastName = "Adeoye",
                    StaffRole = "Doctor",
                    StaffDateOfBirth = new DateTime(2000, 5, 15),
                    StaffAddress = "12 Westminster street, London",
                    StaffPhoneNumber = "07853295100",
                    StaffEmail = "JamesAdeoye@hms.co.uk",
                    StaffEmergencyContactName = "Adeola Bankole",
                    StaffEmergencyContactPhoneNumber = "00182936673",
                    ActiveStatus = "Active",
                    StartDate = new DateTime(2025, 11, 29)
                }
                , new Staff
                {
                    StaffId = "NUR001",
                    StaffFirstName = "Amanda",
                    StaffLastName = "Chigoze",
                    StaffOtherNames = "Eberechukwu",
                    StaffRole = "Nurse",
                    StaffDateOfBirth = new DateTime(1965, 12, 24),
                    StaffAddress = "12 Lockerbie road, London",
                    StaffPhoneNumber = "07911873421",
                    StaffEmail = "AmandaChigozeE@gmail.com",
                    StaffEmergencyContactName = "Amarachi Anyagu",
                    StaffEmergencyContactPhoneNumber = "07911282556",
                    ActiveStatus = "Active",
                    StartDate = new DateTime(2000, 9, 24)
                }
                , new Staff
                {
                    StaffId = "LBT001",
                    StaffFirstName = "Jacob",
                    StaffLastName = "Bradford",
                    StaffOtherNames = "Ezekiel",
                    StaffRole = "Lab Technician",
                    StaffDateOfBirth = new DateTime(1990, 12, 25),
                    StaffAddress = "34 The Maltings, London",
                    StaffPhoneNumber = "09567345128",
                    StaffEmail = "JacobBradford@yahoo.com",
                    StaffEmergencyContactName = "Jackson Bradford",
                    StaffEmergencyContactPhoneNumber = "03569829334",
                    ActiveStatus = "Active",
                    StartDate = new DateTime(2015, 7, 2)
                }
                , new Staff
                {
                    StaffId = "REP001",
                    StaffFirstName = "Emily",
                    StaffLastName = "Smith",
                    StaffOtherNames = "Rose",
                    StaffRole = "Receptionist",
                    StaffDateOfBirth = new DateTime(1995, 3, 10),
                    StaffAddress = "56 High Street, London",
                    StaffPhoneNumber = "07451239876",
                    StaffEmail = "EmilySmith@hms.co.uk",
                    StaffEmergencyContactName = "Sarah Smith",
                    StaffEmergencyContactPhoneNumber = "07987654321",
                    ActiveStatus = "Active",
                    StartDate = new DateTime(2021, 5, 15)
                }
                , new Staff
                {
                    StaffId = "SMA001",
                    StaffFirstName = "Michael",
                    StaffLastName = "Johnson",
                    StaffRole = "System Administrator",
                    StaffDateOfBirth = new DateTime(1980, 8, 20),
                    StaffAddress = "78 Park Avenue, London",
                    StaffPhoneNumber = "07345678901",
                    StaffEmail = "JohnsonJonson13@hsm.co.uk",
                    StaffEmergencyContactName = "Laura Johnson",
                    StaffEmergencyContactPhoneNumber = "07912345678",
                    ActiveStatus = "Active",
                    StartDate = new DateTime(2018, 3, 1)
                }
                , new Staff
                {
                    StaffId = "PHA001",
                    StaffFirstName = "Olivia",
                    StaffLastName = "Williams",
                    StaffOtherNames = "Grace",
                    StaffRole = "Pharmacist",
                    StaffDateOfBirth = new DateTime(1985, 11, 5),
                    StaffAddress = "90 Elm Street, London",
                    StaffPhoneNumber = "07654321098",
                    StaffEmail = "GraceWilliams@hsm.co.uk",
                    StaffEmergencyContactName = "Sophia Williams",
                    StaffEmergencyContactPhoneNumber = "07987654321",
                    ActiveStatus = "Active",
                    StartDate = new DateTime(2019, 6, 10)
                }
                , new Staff
                {
                    StaffId = "ITP001",
                    StaffFirstName = "David",
                    StaffLastName = "Brown",
                    StaffOtherNames = "Alexander",
                    StaffRole = "IT Personnel",
                    StaffDateOfBirth = new DateTime(1992, 4, 18),
                    StaffAddress = "123 Tech Lane, London",
                    StaffPhoneNumber = "07123456789",
                    StaffEmail = "DavidITWhiz@hsm.co.uk",
                    StaffEmergencyContactName = "James Brown",
                    StaffEmergencyContactPhoneNumber = "07987654321",
                    ActiveStatus = "Active",
                    StartDate = new DateTime(2020, 1, 15)
                }
            );
            builder.Entity<Patient>().HasData(
                new Patient
                {
                    PatientId = "PAT001",
                    PatientFirstName = "John",
                    PatientLastName = "Doe",
                    PatientOtherNames = "Michael",
                    PatientDateOfBirth = new DateTime(1990, 1, 1),
                    PatientAddress = "123 Main Street, London",
                    PatientPhoneNumber = "07123456789",
                    PatientEmail = "Johndabomb@yahoo.com",
                    PatientEmergencyContactName = "Jane Doe",
                    PatientEmergencyContactPhoneNumber = "07987654321",
                    CreationDate = new DateTime(2023, 10, 1),
                    AssignedDoctorId = "DOC001"
                }
                , new Patient
                {
                    PatientId = "PAT002",
                    PatientFirstName = "Millie",
                    PatientLastName = "Smith",
                    PatientDateOfBirth = new DateTime(1985, 5, 15),
                    PatientAddress = "456 Elm Street, London",
                    PatientPhoneNumber = "07987654321",
                    PatientEmail = "MilleSmith3@gmail.com",
                    PatientEmergencyContactName = "Emily Smith",
                    PatientEmergencyContactPhoneNumber = "07123456789",
                    CreationDate = new DateTime(2023, 10, 2),
                    AssignedDoctorId = "DOC002"
                }
            );
            builder.Entity<UserAccount>().HasData(
                new UserAccount
                {
                    AccountId = 1,
                    Email = "Millie Smith",
                    Role = "Patient",
                    Password = "#90ndjj3",
                    AccountStatus = "Active",
                    LastLogin = new DateTime(2026, 2, 5, 7, 0, 0),
                    PatientId = "PAT002"
                }
                , new UserAccount
                {
                    AccountId = 2,
                    Email = "GraceWilliams@hsm.co.uk",
                    Role = "Pharmacist",
                    Password = "Gracie2456",
                    AccountStatus = "Active",
                    LastLogin = new DateTime(2026, 2, 5, 8, 10, 12),
                    StaffId = "PHA001"
                }
            );
            builder.Entity<UserAccount>().HasData(
                new UserAccount
                {
                    AccountId = 3,
                    Email = "EmilySmith@hms.co.uk",
                    Role = "Receptionist",
                    Password = "Emily2021",
                    AccountStatus = "Active",
                    LastLogin = new DateTime(2026, 2, 5, 9, 15, 30),
                    StaffId = "REP001"
                }
                , new UserAccount
                {
                    AccountId = 4,
                    Email = "Johndabomb@yahoo.com",
                    Role = "Patient",
                    Password = "LawandOrdersvu",
                    AccountStatus = "Active",
                    LastLogin = new DateTime(2025, 2, 5, 10, 20, 45),
                    PatientId = "PAT001"
                }
                , new UserAccount
                {
                    AccountId = 5,
                    Email = "amelia.hart@gmail.com",
                    Role = "Doctor",
                    Password = "nAkEdInMaNhAtTaN",
                    AccountStatus = "Active",
                    LastLogin = new DateTime(2026, 2, 5, 11, 30, 0),
                    StaffId = "DOC001"
                }
                , new UserAccount
                {
                    AccountId = 6,
                    Email = "JamesAdeoye@hms.co.uk",
                    Role = "Doctor",
                    Password = "2026hmsyay",
                    AccountStatus = "Active",
                    LastLogin = new DateTime(2026, 2, 5, 12, 45, 15),
                    StaffId = "DOC002"
                }
                , new UserAccount
                {
                    AccountId = 7,
                    Email = "AmandaChigozeE@gmail.com",
                    Role = "Nurse",
                    Password = "NurseAmanda2026",
                    AccountStatus = "Active",
                    LastLogin = new DateTime(2026, 2, 5, 13, 50, 30),
                    StaffId = "NUR001"
                }
                , new UserAccount
                {
                    AccountId = 8,
                    Email = "JacobBradford@yahoo.com",
                    Role = "Lab Technician",
                    Password = "AbbottonAbbottonAbbott",
                    AccountStatus = "Active",
                    LastLogin = new DateTime(2026, 2, 5, 14, 55, 45),
                    StaffId = "LBT001"
                }
                , new UserAccount
                {
                    AccountId = 9,
                    Email = "JohnsonJonson13@hsm.co.uk",
                    Role = "System Administrator",
                    Password = "LuckyJohnson",
                    AccountStatus = "Active",
                    LastLogin = new DateTime(2026, 2, 5, 15, 0, 0),
                    StaffId = "SMA001"
                }
                , new UserAccount
                {
                    AccountId = 10,
                    Email = "DavidITWhiz@hsm.co.uk",
                    Role = "IT Personnel",
                    Password = "+lol^^cantguessmypassword",
                    AccountStatus = "Active",
                    LastLogin = new DateTime(2026, 2, 5, 16, 10, 15),
                    StaffId = "ITP001"
                }
            );
            builder.Entity<Appointment>().HasData(
                new Appointment
                {
                    AppointmentId = "APT001",
                    AppointmentDate = new DateTime(2026, 9, 18),
                    AppointmentTime = "09:00 AM",
                    AppointmentStatus = "Scheduled",
                    ArrivalConfirmation = false,
                    Notes = "Routine checkup",
                    CreationDate = DateTime.Now,
                    PatientId = "PAT001",
                    DoctorId = "DOC001"
                },
                new Appointment
                {
                    AppointmentId = "APT002",
                    AppointmentDate = new DateTime(2027, 4, 18),
                    AppointmentTime = "11:00 AM",
                    AppointmentStatus = "Scheduled",
                    ArrivalConfirmation = false,
                    Notes = "Follow-up",
                    CreationDate = DateTime.Now,
                    PatientId = "PAT002", 
                    DoctorId = "DOC001"
                }
            );
            builder.Entity<Visit>().HasData(
                new Visit
                {
                    VisitId = "VIS001",
                    VisitDateTime = new DateTime(2026, 9, 18, 9, 30, 0),
                    Notes = "Initial consultation after patient arrival.",
                    PatientId = "PAT001",
                    DoctorId = "DOC001",
                    AppointmentId = "APT001"
                },
                new Visit
                {
                    VisitId = "VIS002",
                    VisitDateTime = new DateTime(2027, 4, 18, 11, 30, 0),
                    Notes = "Follow-up consultation.",
                    PatientId = "PAT002",
                    DoctorId = "DOC001",
                    AppointmentId = "APT002"
                }
            );

            builder.Entity<Diagnosis>().HasData(
                new Diagnosis
                {
                    DiagnosisId = "DIA001",
                    DiagnosisName = "Mild Hypertension",
                    Description = "Patient advised to monitor blood pressure and reduce salt intake.",
                    DiagnosisDate = new DateTime(2026, 9, 18),
                    VisitId = "VIS001"
                },
                new Diagnosis
                {
                    DiagnosisId = "DIA002",
                    DiagnosisName = "Seasonal Allergies",
                    Description = "Symptoms consistent with seasonal allergic rhinitis.",
                    DiagnosisDate = new DateTime(2027, 4, 18),
                    VisitId = "VIS002"
                }
            );

            builder.Entity<Prescription>().HasData(
                new Prescription
                {
                    PrescriptionId = "PRE001",
                    PrescriptionText = "Boscopan 5mg once daily",
                    Notes = "Review needed in 2 weeks.",
                    VisitId = "VIS001",
                    PharmacistId = "PHA001"
                },
                new Prescription
                {
                    PrescriptionId = "PRE002",
                    PrescriptionText = "Cetirizine 10mg once daily",
                    Notes = "Take as needed during allergy season.",
                    VisitId = "VIS002",
                    PharmacistId = "PHA001"
                }
            );

            builder.Entity<LabTestResult>().HasData(
                new LabTestResult
                {
                    TestId = "LAB001",
                    TestType = "Blood Test",
                    ResultDetails = "Blood pressure markers reviewed. No urgent abnormality detected.",
                    ResultDate = new DateTime(2026, 9, 19),
                    Status = "Completed",
                    UploadDate = new DateTime(2026, 9, 19, 14, 0, 0),
                    VisitId = "VIS001",
                    LabTechId = "LBT001"
                },
                new LabTestResult
                {
                    TestId = "LAB002",
                    TestType = "Allergy Panel",
                    ResultDetails = "Draft result pending final review before doctor access.",
                    ResultDate = new DateTime(2027, 4, 19),
                    Status = "Pending",
                    UploadDate = new DateTime(2027, 4, 19, 10, 15, 0),
                    VisitId = "VIS002",
                    LabTechId = "LBT001"
                }
            );

            builder.Entity<Vitals>().HasData(
                new Vitals
                {
                    VitalsId = "VIT001",
                    Temperature = 37,
                    BloodPressure = "140/90",
                    HeartRate = "82 bpm",
                    RespiratoryRate = "18 breaths/min",
                    Notes = "Patient stable but blood pressure slightly elevated.",
                    RecordedAt = new DateTime(2026, 9, 18, 9, 15, 0),
                    IsDraft = false,
                    VisitId = "VIS001",
                    NurseId = "NUR001"
                },
                new Vitals
                {
                    VitalsId = "VIT002",
                    Temperature = 36,
                    BloodPressure = "118/76",
                    HeartRate = "74 bpm",
                    RespiratoryRate = "16 breaths/min",
                    Notes = "Vitals within normal range.",
                    RecordedAt = new DateTime(2027, 4, 18, 11, 15, 0),
                    IsDraft = true,
                    VisitId = "VIS002",
                    NurseId = "NUR001"
                }
            );

            builder.Entity<Device>().HasData(
                new Device
                {
                    DeviceId = "DEV001",
                    DeviceName = "Dell Latitude 7420",
                    DeviceType = "Laptop",
                    Status = "In Use",
                    AssignedStaffId = "DOC001",
                    Room = null,
                    LastUpdated = new DateTime(2026, 4, 20, 9, 0, 0)
                },
                new Device
                {
                    DeviceId = "DEV002",
                    DeviceName = "HP EliteBook 840",
                    DeviceType = "Laptop",
                    Status = "In Use",
                    AssignedStaffId = "NUR001",
                    Room = null,
                    LastUpdated = new DateTime(2026, 4, 20, 10, 30, 0)
                },
                new Device
                {
                    DeviceId = "DEV003",
                    DeviceName = "iPad Pro 12.9",
                    DeviceType = "Tablet",
                    Status = "In Use",
                    AssignedStaffId = null,
                    Room = "Ward A - Room 1",
                    LastUpdated = new DateTime(2026, 4, 21, 11, 0, 0)
                },
                new Device
                {
                    DeviceId = "DEV004",
                    DeviceName = "iPad Mini",
                    DeviceType = "Tablet",
                    Status = "Available",
                    AssignedStaffId = null,
                    Room = "Ward B - Room 3",
                    LastUpdated = new DateTime(2026, 4, 21, 12, 15, 0)
                },
                new Device
                {
                    DeviceId = "DEV005",
                    DeviceName = "Lenovo ThinkPad X1",
                    DeviceType = "Laptop",
                    Status = "Available",
                    AssignedStaffId = null,
                    Room = null,
                    LastUpdated = new DateTime(2026, 4, 22, 8, 45, 0)
                },
                new Device
                {
                    DeviceId = "DEV006",
                    DeviceName = "Patient Vitals Monitor",
                    DeviceType = "Medical Device",
                    Status = "In Use",
                    AssignedStaffId = null,
                    Room = "ICU - Bed 2",
                    LastUpdated = new DateTime(2026, 4, 22, 14, 0, 0)
                },
                new Device
                {
                    DeviceId = "DEV007",
                    DeviceName = "Samsung Tablet S8",
                    DeviceType = "Tablet",
                    Status = "Faulty",
                    AssignedStaffId = null,
                    Room = "Ward A - Room 2",
                    LastUpdated = new DateTime(2026, 4, 23, 9, 30, 0)
                },
                new Device
                {
                    DeviceId = "DEV008",
                    DeviceName = "Barcode Scanner ZX-300",
                    DeviceType = "Scanner",
                    Status = "Available",
                    AssignedStaffId = null,
                    Room = "Pharmacy",
                    LastUpdated = new DateTime(2026, 4, 23, 13, 10, 0)
                },
                new Device
                {
                    DeviceId = "DEV009",
                    DeviceName = "MacBook Air M2",
                    DeviceType = "Laptop",
                    Status = "In Use",
                    AssignedStaffId = "ITP001",
                    Room = null,
                    LastUpdated = new DateTime(2026, 4, 24, 10, 0, 0)
                },
                new Device
                {
                    DeviceId = "DEV010",
                    DeviceName = "Surface Pro 9",
                    DeviceType = "Tablet/Laptop",
                    Status = "Faulty",
                    AssignedStaffId = "DOC002",
                    Room = null,
                    LastUpdated = new DateTime(2026, 4, 24, 15, 20, 0)
                }
            );

            builder.Entity<Notification>().HasData(
                new Notification
                {
                    NotificationId = "NOT001",
                    SenderId = "SMA001",
                    SenderType = "Staff",
                    ReceiverId = null,
                    ReceiverType = "AllStaff",
                    Title = "Staff Meeting",
                    Message = "Staff meeting on Friday at 10am.",
                    IsRead = false,
                    CreatedAt = new DateTime(2026, 4, 28, 9, 0, 0)
                },
                new Notification
                {
                    NotificationId = "NOT002",
                    SenderId = "DOC001",
                    SenderType = "Staff",
                    ReceiverId = "PAT001",
                    ReceiverType = "Patient",
                    Title = "Appointment Reminder",
                    Message = "Please remember your upcoming appointment.",
                    IsRead = false,
                    CreatedAt = new DateTime(2026, 4, 28, 10, 0, 0)
                }
            );
        }


        public DbSet<Staff> Staff {  get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<LabTestResult> LabTestResults { get; set; }
        public DbSet<Vitals> Vitals { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
    }
}

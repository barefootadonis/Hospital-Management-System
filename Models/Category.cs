using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Hospital_Management_System.Models
{
    public class Staff
    {
        [ValidateNever]
        public string StaffId { get; set; }

        public string StaffFirstName { get; set; }
        public string StaffLastName { get; set; }
        public string? StaffOtherNames { get; set; }
        public string StaffRole { get; set; }
        public DateTime StaffDateOfBirth { get; set; }
        public string StaffAddress { get; set; }
        public string StaffPhoneNumber { get; set; }
        public string StaffEmail { get; set; }
        public string StaffEmergencyContactName { get; set; }
        public string StaffEmergencyContactPhoneNumber { get; set; }

        [ValidateNever]
        public string ActiveStatus { get; set; }
        [ValidateNever]
        public DateTime StartDate { get; set; }

        [ValidateNever]
        public ICollection<Patient> Patients { get; set; }
    }

    public class Patient
    {
        [ValidateNever]
        public string PatientId { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string? PatientOtherNames { get; set; }
        public DateTime PatientDateOfBirth { get; set; }
        public string PatientAddress { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string PatientEmail { get; set; }
        public string PatientEmergencyContactName { get; set; }
        public string PatientEmergencyContactPhoneNumber { get; set; }
        public DateTime CreationDate { get; set; }

        // Foreign Key
        public string? AssignedDoctorId { get; set; }
        public Staff? AssignedDoctor { get; set; }
        [ValidateNever]
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }

    public class UserAccount
    {
        [Key]
        public int AccountId { get; set; }

        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public DateTime PasswordLastChanged { get; set; }
        public string AccountStatus { get; set; }
        public DateTime LastLogin { get; set; }

        // Foreign Keys
        public string? StaffId { get; set; }
        public Staff? Staff { get; set; }

        public string? PatientId { get; set; }
        public Patient? Patient { get; set; }
    }

    public class Appointment
    {
        public string AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string AppointmentStatus { get; set; }
        public bool ArrivalConfirmation { get; set; }
        public string Notes { get; set; }
        public DateTime CreationDate { get; set; }

        // Foreign Keys
        public string PatientId { get; set; }
        public Patient Patient { get; set; }

        public string DoctorId { get; set; }
        public Staff Doctor { get; set; }
    }

    public class Visit
    {
        public string VisitId { get; set; }
        public DateTime VisitDateTime { get; set; }
        public string Notes { get; set; }

        // Foreign Keys
        public string PatientId { get; set; }
        public Patient Patient { get; set; }

        public string DoctorId { get; set; }
        public Staff Doctor { get; set; }

        public string AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }

    public class Diagnosis
    {
        public string DiagnosisId { get; set; }
        public string DiagnosisName { get; set; }
        public string Description { get; set; }
        public DateTime DiagnosisDate { get; set; }

        // Foreign Key
        public string VisitId { get; set; }
        public Visit Visit { get; set; }
    }

    public class Prescription
    {
        public string PrescriptionId { get; set; }
        public string PrescriptionText { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; } = "Pending";

        // Foreign Keys
        public string VisitId { get; set; }
        public Visit Visit { get; set; }

        public string PharmacistId { get; set; }
        public Staff Pharmacist { get; set; }
    }

    public class LabTestResult
    {
        [Key]
        public string TestId { get; set; }

        public string TestType { get; set; }
        public DateTime ResultDate { get; set; }
        public string Status { get; set; }
        public DateTime UploadDate { get; set; }
        public string ResultDetails { get; set; }

        // Foreign Keys
        public string VisitId { get; set; }
        public Visit Visit { get; set; }

        public string LabTechId { get; set; }
        public Staff LabTech { get; set; }
    }

    public class Vitals
    {
        public string VitalsId { get; set; }
        public int Temperature { get; set; }
        public string BloodPressure { get; set; }
        public string HeartRate { get; set; }
        public string RespiratoryRate { get; set; }
        public string Notes { get; set; }
        public DateTime RecordedAt { get; set; }

        public bool IsDraft { get; set; }

        // Foreign Keys
        public string VisitId { get; set; }
        public Visit Visit { get; set; }

        public string NurseId { get; set; }
        public Staff Nurse { get; set; }
    }

    public class Ticket
    {
        public string TicketId { get; set; }

        public string Subject { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; } // Low, Medium, High
        public string Status { get; set; } // Open, In Progress, Resolved
        public DateTime CreatedAt { get; set; }

        public string SenderId { get; set; }
        public string SenderRole { get; set; }

        public string? AssignedToId { get; set; }
        public Staff? AssignedTo { get; set; }
    }

    public class Notification
    {
        [Key]
        public string NotificationId { get; set; }

        public string SenderId { get; set; }
        public string SenderType { get; set; }
        public string? ReceiverId { get; set; } // null means all staff
        public string ReceiverType { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }

        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class Device
    {
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; } 
        public string Status { get; set; } // Available, In Use, Faulty

        public string? AssignedStaffId { get; set; }
        public Staff? AssignedStaff { get; set; }

        public string? Room { get; set; } 

        public DateTime LastUpdated { get; set; }
    }

    public class CalendarEvent
    {
        public string CalendarEventId { get; set; }
        public string UserId { get; set; }
        public string UserType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string EventTime { get; set; }

        public DateTime CreatedAt { get; set; }
    }

}

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Hospital_Management_System.Models
{
    public class CreateAppointmentViewModel
    {
        public string PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string? Notes { get; set; }

        [ValidateNever]
        public List<Patient> Patients { get; set; } = new();
    }
}
namespace Hospital_Management_System.Models
{
    public class PatientTimelineViewModel
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string RecordId { get; set; }
        public string DoctorId { get; set; }
    }
}

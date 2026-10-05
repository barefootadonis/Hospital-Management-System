using Hospital_Management_System.Models;
using System.Collections.Generic;

public class LabTechDashboardViewModel
{
    public Staff Staff { get; set; }
    public List<LabTestResult> PendingTests { get; set; }
}

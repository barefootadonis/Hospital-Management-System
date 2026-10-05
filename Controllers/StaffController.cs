using Hospital_Management_System.Data;
using Hospital_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

public class StaffController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly PasswordHasher<UserAccount> _passwordHasher = new();

    public StaffController(ApplicationDbContext context)
    {
        _context = context;
    }

    // LOGIN PAGE
    public IActionResult Login()
    {
        return View();
    }

    // LOGIN POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        await DeactivateInactiveAccounts();
        if (!ModelState.IsValid)
            return View(model);

        var account = await _context.UserAccounts
            .Include(a => a.Staff)
            .FirstOrDefaultAsync(a =>
                a.Email == model.EmailOrId ||
                a.StaffId == model.EmailOrId
            );


        if (account == null)
        {
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        if (account.AccountStatus == "Deactivated")
        {
            ModelState.AddModelError("", "This account has been deactivated.");
            return View(model);
        }

        var role = account.Role?.Trim();

        bool passwordValid = false;

        try
        {
            var result = _passwordHasher.VerifyHashedPassword(
                account,
                account.Password,
                model.Password.Trim()
            );

            passwordValid = result != PasswordVerificationResult.Failed;
        }
        catch (FormatException)
        {
            passwordValid = account.Password == model.Password.Trim();

            if (passwordValid)
            {
                account.Password = _passwordHasher.HashPassword(account, model.Password.Trim());
                account.PasswordLastChanged = DateTime.Now;
            }
        }

        if (!passwordValid)
        {
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        account.LastLogin = DateTime.Now;
        await _context.SaveChangesAsync();

        HttpContext.Session.SetString("UserId", account.StaffId);
        HttpContext.Session.SetString("UserType", "Staff");


        if (role == "Receptionist")
        {
            return RedirectToAction("ReceptionistDashboard", new {id = account.StaffId });
        }
        else if (role == "Pharmacist")
        {
            return RedirectToAction("PharmacistDashboard", new { id = account.StaffId });
        }
        else if (role == "Lab Technician")
        {
            return RedirectToAction("LabTechDashboard", new { id = account.StaffId });
        }
        else if (role == "Nurse")
        {
            return RedirectToAction("NurseDashboard", new { id = account.StaffId });
        }
        else if (role == "Doctor")
        {
            return RedirectToAction("DoctorDashboard", new { id = account.StaffId });
        }
        else if (role == "IT Personnel")
        {
            return RedirectToAction("ITPersonnelDashboard", new { id = account.StaffId });
        }
        else if (role == "System Administrator")
        {
            return RedirectToAction("SystemAdminDashboard", new { id = account.StaffId });

        }
        else if (role == "Emergency Doctor")
        {
            return RedirectToAction("DoctorDashboard", new { id = account.StaffId });
        }
        else
        {
            return RedirectToAction("StaffDashboard");
        }
    }

    //VIEW ALL STAFF
    public async Task<IActionResult> Index()
    {
        return View(await _context.Staff.ToListAsync());
    }

    public async Task<IActionResult> Create()
{
    await SetNextStaffIds();
    return View();
}

    private async Task<string> GetNextStaffId(string prefix)
    {
        var lastStaff = await _context.Staff
            .Where(s => s.StaffId.StartsWith(prefix))
            .OrderByDescending(s => s.StaffId)
            .FirstOrDefaultAsync();

        int nextNumber = 1;

        if (lastStaff != null)
        {
            var numericPart = int.Parse(lastStaff.StaffId.Replace(prefix, ""));
            nextNumber = numericPart + 1;
        }

        return $"{prefix}{nextNumber:D3}";
    }

    private async Task SetNextStaffIds()
    {
        ViewBag.NextIds = new Dictionary<string, string>
    {
        { "Doctor", await GetNextStaffId("DOC") },
        { "Nurse", await GetNextStaffId("NUR") },
        { "Lab Technician", await GetNextStaffId("LBT") },
        { "Receptionist", await GetNextStaffId("REP") },
        { "System Administrator", await GetNextStaffId("SMA") },
        { "Pharmacist", await GetNextStaffId("PHA") },
        { "IT Personnel", await GetNextStaffId("ITP") },
        { "Emergency Doctor", await GetNextStaffId("EMD") },
    };
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Staff staff, string Password)
    {
        if (!ModelState.IsValid)
        {
            await SetNextStaffIds();
            return View(staff);
        }

        var existingAccount = await _context.UserAccounts
            .FirstOrDefaultAsync(a => a.Email == staff.StaffEmail);

        if (existingAccount != null)
        {
            ModelState.AddModelError("", "An account with this email already exists.");
            await SetNextStaffIds();
            return View(staff);
        }

        string prefix = staff.StaffRole switch
        {
            "Doctor" => "DOC",
            "Emergency Doctor" => "EMD",
            "Nurse" => "NUR",
            "Lab Technician" => "LBT",
            "Receptionist" => "REP",
            "System Administrator" => "SMA",
            "Pharmacist" => "PHA",
            "IT Personnel" => "ITP",
            _ => "STF"
        };

        var lastStaff = await _context.Staff
            .Where(s => s.StaffId.StartsWith(prefix))
            .OrderByDescending(s => s.StaffId)
            .FirstOrDefaultAsync();

        int nextNumber = 1;

        if (lastStaff != null)
        {
            var numericPart = int.Parse(lastStaff.StaffId.Replace(prefix, ""));
            nextNumber = numericPart + 1;
        }

        staff.StaffId = $"{prefix}{nextNumber:D3}";
        staff.StaffOtherNames ??= "";
        staff.ActiveStatus = "Active";
        staff.StartDate = DateTime.Now;

        _context.Staff.Add(staff);

        var account = new UserAccount
        {
            Email = staff.StaffEmail,
            Password = Password,
            Role = staff.StaffRole,
            AccountStatus = "Active",
            LastLogin = DateTime.Now,
            StaffId = staff.StaffId,
            PasswordLastChanged = DateTime.Now
        };

        account.Password = _passwordHasher.HashPassword(account, Password);

        _context.UserAccounts.Add(account);

        await _context.SaveChangesAsync();

        return RedirectToAction("StaffLookup");
    }



    public async Task<IActionResult> Notifications(string id, string userType)
    {
        var notifications = await _context.Notifications
            .Where(n =>
                (n.ReceiverId == id && n.ReceiverType == userType) ||
                (userType == "Staff" && n.ReceiverType == "AllStaff"))
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();

        ViewBag.UserType = userType;
        ViewBag.CurrentUserId = id;

        return View(notifications);
    }

    public async Task<IActionResult> SendMessage(string id)
    {
        ViewBag.SenderId = id;

        ViewBag.Staff = await _context.Staff
            .Where(s => s.ActiveStatus == "Active")
            .OrderBy(s => s.StaffFirstName)
            .ToListAsync();

        ViewBag.Patients = await _context.Patients
            .OrderBy(p => p.PatientFirstName)
            .ToListAsync();

        ViewBag.UserType = "Staff";
        ViewBag.CurrentUserId = id;

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SendMessage(
    string senderId,
    string receiverType,
    string receiverId,
    string title,
    string message)
    {
        var last = await _context.Notifications
            .OrderByDescending(n => n.NotificationId)
            .FirstOrDefaultAsync();

        int nextNumber = 1;

        if (last != null)
        {
            var numeric = int.Parse(last.NotificationId.Replace("NOT", ""));
            nextNumber = numeric + 1;
        }

        var notification = new Notification
        {
            NotificationId = $"NOT{nextNumber:D3}",
            SenderId = senderId,
            SenderType = "Staff",
            ReceiverType = receiverType,
            ReceiverId = receiverType == "AllStaff" ? null : receiverId,
            Title = title,
            Message = message,
            IsRead = false,
            CreatedAt = DateTime.Now
        };

        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();

        return RedirectToAction("SentMessages", new { id = senderId });
    }

    public async Task<IActionResult> SentMessages(string id)
    {
        var sent = await _context.Notifications
            .Where(n => n.SenderId == id && n.SenderType == "Staff")
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();

        ViewBag.UserType = "Staff";
        ViewBag.CurrentUserId = id;

        return View(sent);
    }

    public async Task<IActionResult> Edit(string id, string returnUrl = null)
    {
        var staff = await _context.Staff.FindAsync(id);
        if (staff == null) return NotFound();

        ViewBag.ReturnUrl = returnUrl;
        return View(staff);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, Staff staff, string returnUrl = null)
    {
        if (id != staff.StaffId)
            return NotFound();

        if (ModelState.IsValid)
        {
            _context.Staff.Update(staff);
            await _context.SaveChangesAsync();

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("StaffLookup");
        }

        ViewBag.ReturnUrl = returnUrl;
        return View(staff);
    }



    public async Task<IActionResult> PastTests()
    {
        var pastTests = await _context.LabTestResults
            .Where(t => t.Status != "Pending")
            .ToListAsync();

        return View(pastTests);
    }

    public async Task<IActionResult> PatientLookup()
    {
        var patients = await _context.Patients
            .OrderBy(p => p.PatientLastName)
            .ThenBy(p => p.PatientFirstName)
            .ToListAsync();

        var accounts = await _context.UserAccounts
            .Where(a => a.PatientId != null)
            .ToListAsync();

        ViewBag.Accounts = accounts;

        return View(patients);
    }
    public async Task<IActionResult> StaffLookup()
    {
        var staff = await _context.Staff
            .OrderBy(s => s.StaffRole)
            .ThenBy(s => s.StaffLastName)
            .ToListAsync();

        return View(staff);
    }

    [HttpPost]
    public async Task<IActionResult> Deactivate(string id)
    {
        var staff = await _context.Staff
            .Include(s => s.Patients)
            .FirstOrDefaultAsync(s => s.StaffId == id);

        if (staff == null)
            return NotFound();

        staff.ActiveStatus = "Inactive";

        var account = await _context.UserAccounts
            .FirstOrDefaultAsync(a => a.StaffId == id);

                if (account != null)
                {
                    account.AccountStatus = "Deactivated";
                }

        // If Doctor, remove from patients
        if (staff.StaffRole == "Doctor")
        {
            var patients = await _context.Patients
                .Where(p => p.AssignedDoctorId == id)
                .ToListAsync();

            foreach (var patient in patients)
            {
                patient.AssignedDoctorId = null;
            }

        }

        await _context.SaveChangesAsync();

        return RedirectToAction("StaffLookup");
    }

    [HttpPost]
    public async Task<IActionResult> Activate(string id)
    {
        var staff = await _context.Staff
            .FirstOrDefaultAsync(s => s.StaffId == id);

        if (staff == null)
            return NotFound();

        staff.ActiveStatus = "Active";

        var account = await _context.UserAccounts
            .FirstOrDefaultAsync(a => a.StaffId == id);

        if (account != null)
        {
            account.AccountStatus = "Active";
        }

        await _context.SaveChangesAsync();

        return RedirectToAction("StaffLookup");
    }

    public async Task<IActionResult> Profile(string id)
    {
        if (id == null)
            return NotFound();

        var staff = await _context.Staff
            .FirstOrDefaultAsync(s => s.StaffId == id);

        if (staff == null)
            return NotFound();

        return View(staff);
    }

    public async Task<IActionResult> ReceptionistDashboard(string id)
    {
        if (id == null)
            return NotFound();

        var staff = await _context.Staff
            .FirstOrDefaultAsync(s => s.StaffId == id);

        if (staff == null)
            return NotFound();
        ViewBag.UserType = "Staff";
        ViewBag.CurrentUserId = id;

        var today = DateTime.Today;

        var appointments = await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.AppointmentDate.Date >= today)
            .OrderBy(a => a.AppointmentDate)
            .ThenBy(a => a.AppointmentTime)
            .ToListAsync();

        var viewModel = new ReceptionistDashboardViewModel
        {
            Staff = staff,
            Appointments = appointments
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmArrival(string id, string staffId)
    {
        var appointment = await _context.Appointments
            .FirstOrDefaultAsync(a => a.AppointmentId == id);

        if (appointment == null)
            return NotFound();

        appointment.ArrivalConfirmation = true;
        appointment.AppointmentStatus = "Arrived";

        await _context.SaveChangesAsync();

        return RedirectToAction("ReceptionistDashboard",
            new { id = staffId });
    }

    public async Task<IActionResult> CreateAppointment(string id)
    {
        var patients = await _context.Patients
        .Include(p => p.AssignedDoctor)
        .ToListAsync();

        var viewModel = new CreateAppointmentViewModel
        {
            Patients = patients
        };

        ViewBag.StaffId = id;

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAppointment(CreateAppointmentViewModel model, string staffId)
    {
        if (!ModelState.IsValid)
        {
            model.Patients = await _context.Patients
                .Include(p => p.AssignedDoctor)
                .ToListAsync();

            ViewBag.StaffId = staffId;

            return View(model);
        }

        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.PatientId == model.PatientId);

        if (patient == null)
            return NotFound();

        if (string.IsNullOrEmpty(patient.AssignedDoctorId))
        {
            ModelState.AddModelError("", "This patient does not have an assigned doctor.");

            model.Patients = await _context.Patients
                .Include(p => p.AssignedDoctor)
                .ToListAsync();

            ViewBag.StaffId = staffId;

            return View(model);
        }

        var last = await _context.Appointments
            .OrderByDescending(a => a.AppointmentId)
            .FirstOrDefaultAsync();

        int nextNumber = 1;

        if (last != null)
        {
            var numeric = int.Parse(last.AppointmentId.Replace("APT", ""));
            nextNumber = numeric + 1;
        }

        var appointment = new Appointment
        {
            AppointmentId = $"APT{nextNumber:D3}",
            PatientId = model.PatientId,
            DoctorId = patient.AssignedDoctorId,
            AppointmentDate = model.AppointmentDate,
            AppointmentTime = model.AppointmentTime,
            Notes = model.Notes ?? "",
            CreationDate = DateTime.Now,
            AppointmentStatus = "Scheduled",
            ArrivalConfirmation = false
        };

        var appointmentExists = await _context.Appointments.AnyAsync(a =>
            a.DoctorId == patient.AssignedDoctorId &&
            a.AppointmentDate.Date == model.AppointmentDate.Date &&
            a.AppointmentTime == model.AppointmentTime
        );

                if (appointmentExists)
                {
                    ModelState.AddModelError("", "This doctor already has an appointment at this time.");

                    model.Patients = await _context.Patients
                        .Include(p => p.AssignedDoctor)
                        .ToListAsync();

                    ViewBag.StaffId = staffId;

                    return View(model);
                }

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return RedirectToAction("ReceptionistDashboard", new { id = staffId });
    }

    public async Task<IActionResult> CreateEmergencyAppointment(string id)
    {
        var patients = await _context.Patients.ToListAsync();

        var emergencyDoctors = await _context.Staff
            .Where(s => s.StaffRole == "Emergency Doctor" && s.ActiveStatus == "Active")
            .ToListAsync();

        var viewModel = new CreateAppointmentViewModel
        {
            Patients = patients
        };

        ViewBag.StaffId = id;
        ViewBag.EmergencyDoctors = emergencyDoctors;

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateEmergencyAppointment(
    CreateAppointmentViewModel model,
    string staffId,
    string emergencyDoctorId)
    {
        if (!ModelState.IsValid)
        {
            model.Patients = await _context.Patients.ToListAsync();

            ViewBag.StaffId = staffId;
            ViewBag.EmergencyDoctors = await _context.Staff
                .Where(s => s.StaffRole == "Emergency Doctor" && s.ActiveStatus == "Active")
                .ToListAsync();

            return View(model);
        }

        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.PatientId == model.PatientId);

        if (patient == null)
            return NotFound();

        if (string.IsNullOrEmpty(emergencyDoctorId))
        {
            ModelState.AddModelError("", "Please select an emergency doctor.");

            model.Patients = await _context.Patients.ToListAsync();

            ViewBag.StaffId = staffId;
            ViewBag.EmergencyDoctors = await _context.Staff
                .Where(s => s.StaffRole == "Emergency Doctor" && s.ActiveStatus == "Active")
                .ToListAsync();

            return View(model);
        }

        var last = await _context.Appointments
            .OrderByDescending(a => a.AppointmentId)
            .FirstOrDefaultAsync();

        int nextNumber = 1;

        if (last != null)
        {
            var numeric = int.Parse(last.AppointmentId.Replace("APT", ""));
            nextNumber = numeric + 1;
        }

        var appointment = new Appointment
        {
            AppointmentId = $"APT{nextNumber:D3}",
            PatientId = model.PatientId,
            DoctorId = emergencyDoctorId,
            AppointmentDate = model.AppointmentDate,
            AppointmentTime = model.AppointmentTime,
            Notes = "EMERGENCY: " + (model.Notes ?? ""),
            CreationDate = DateTime.Now,
            AppointmentStatus = "Emergency Scheduled",
            ArrivalConfirmation = false
        };

        var appointmentExists = await _context.Appointments.AnyAsync(a =>
            a.DoctorId == emergencyDoctorId &&
            a.AppointmentDate.Date == model.AppointmentDate.Date &&
            a.AppointmentTime == model.AppointmentTime
        );

                if (appointmentExists)
                {
                    ModelState.AddModelError("", "This doctor already has an appointment at this time.");

                    model.Patients = await _context.Patients
                        .Include(p => p.AssignedDoctor)
                        .ToListAsync();

                    ViewBag.StaffId = staffId;

                    return View(model);
                }

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return RedirectToAction("ReceptionistDashboard", new { id = staffId });
    }

    public async Task<IActionResult> PharmacistDashboard(string id)
    {
        ViewBag.UserType = "Staff";
        ViewBag.CurrentUserId = id;
        return await LoadDashboard(id, "PharmacistDashboard");
    }

    public async Task<IActionResult> AssignedPrescriptions(string id)
    {
        var prescriptions = await _context.Prescriptions
            .Include(p => p.Visit)
            .ThenInclude(v => v.Patient)
            .Where(p => p.PharmacistId == id)
            .OrderByDescending(p => p.Visit.VisitDateTime)
            .ToListAsync();

        ViewBag.UserType = "Staff";
        ViewBag.CurrentUserId = id;

        return View(prescriptions);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdatePrescriptionStatus(string prescriptionId, string status, string staffId)
    {
        var prescription = await _context.Prescriptions
            .FirstOrDefaultAsync(p => p.PrescriptionId == prescriptionId);

        if (prescription == null)
            return NotFound();

        prescription.Status = status;

        await _context.SaveChangesAsync();

        return RedirectToAction("AssignedPrescriptions", new { id = staffId });
    }

    public async Task<IActionResult> LabTechDashboard(string id)
    {
        if (id == null)
            return NotFound();

        var staff = await _context.Staff
            .FirstOrDefaultAsync(s => s.StaffId == id);

        if (staff == null)
            return NotFound();
        ViewBag.UserType = "Staff";
        ViewBag.CurrentUserId = id;

        var pendingTests = await _context.LabTestResults
            .Include(t => t.Visit)
            .Where(t => t.Status == "Pending")
            .ToListAsync();

        var viewModel = new LabTechDashboardViewModel
        {
            Staff = staff,
            PendingTests = pendingTests
        };

        return View(viewModel);
    }

    public async Task<IActionResult> UploadLabResult(string id)
    {
        ViewBag.LabTechId = id;

        ViewBag.Visits = await _context.Visits
            .Include(v => v.Patient)
            .ToListAsync();

        return View(new LabTestResult());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UploadLabResult(LabTestResult test, string actionType, string labTechId, string returnUrl = null)
    {
        ModelState.Remove("TestId");
        ModelState.Remove("Visit");
        ModelState.Remove("LabTech");
        ModelState.Remove("LabTechId");
        ModelState.Remove("Status");

        if (!ModelState.IsValid)
        {
            ViewBag.LabTechId = labTechId;
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Visits = await _context.Visits
                .Include(v => v.Patient)
                .ToListAsync();

            return View(test);
        }

        if (string.IsNullOrEmpty(test.TestId))
        {
            var last = await _context.LabTestResults
                .OrderByDescending(t => t.TestId)
                .FirstOrDefaultAsync();

            int nextNumber = 1;

            if (last != null)
            {
                var numeric = int.Parse(last.TestId.Replace("LAB", ""));
                nextNumber = numeric + 1;
            }

            test.TestId = $"LAB{nextNumber:D3}";
            test.LabTechId = labTechId;
            test.UploadDate = DateTime.Now;
            test.ResultDate = DateTime.Now;
            test.Status = actionType == "Pending" ? "Pending" : "Completed";
            test.ResultDetails ??= "";

            _context.LabTestResults.Add(test);
        }
        else
        {
            var existing = await _context.LabTestResults
                .FirstOrDefaultAsync(t => t.TestId == test.TestId);

            if (existing == null)
                return NotFound();

            existing.TestType = test.TestType;
            existing.VisitId = test.VisitId;
            existing.ResultDetails = test.ResultDetails ?? "";
            existing.LabTechId = labTechId;
            existing.ResultDate = DateTime.Now;
            existing.UploadDate = DateTime.Now;
            existing.Status = actionType == "Pending" ? "Pending" : "Completed";
        }

        await _context.SaveChangesAsync();

        if (actionType == "Pending")
            return RedirectToAction("PendingLabResults", new { id = labTechId });

        return RedirectToAction("LabTechDashboard", new { id = labTechId });
    }

    public async Task<IActionResult> PendingLabResults(string id)
    {
        var drafts = await _context.LabTestResults
            .Include(t => t.Visit)
            .ThenInclude(v => v.Patient)
            .Where(t => t.LabTechId == id && t.Status == "Pending")
            .OrderByDescending(t => t.UploadDate)
            .ToListAsync();

        ViewBag.LabTechId = id;

        return View(drafts);
    }

    public async Task<IActionResult> CompletedResults()
    {
        var results = await _context.LabTestResults
            .Include(t => t.Visit)
            .ThenInclude(v => v.Patient)
            .Include(t => t.LabTech)
            .Where(t => t.Status == "Completed")
            .OrderByDescending(t => t.UploadDate)
            .ToListAsync();

        return View(results);
    }

    public async Task<IActionResult> EditLabResult(string id, string returnUrl = null)
    {
        var test = await _context.LabTestResults
            .Include(t => t.Visit)
            .ThenInclude(v => v.Patient)
            .FirstOrDefaultAsync(t => t.TestId == id);

        if (test == null)
            return NotFound();

        ViewBag.ReturnUrl = returnUrl;

        ViewBag.Visits = await _context.Visits
            .Include(v => v.Patient)
            .ToListAsync();

        ViewBag.LabTechId = test.LabTechId;

        return View("UploadLabResult", test);
    }

    public async Task<IActionResult> NurseDashboard(string id)
    {
        ViewBag.UserType = "Staff";
        ViewBag.CurrentUserId = id;
        return await LoadDashboard(id, "NurseDashboard");
    }

    public async Task<IActionResult> VitalsMenu(string id)
    {
        var nurse = await _context.Staff.FirstOrDefaultAsync(s => s.StaffId == id);

        if (nurse == null)
            return NotFound();

        ViewBag.UserType = "Staff";
        ViewBag.CurrentUserId = id;

        return View(nurse);
    }

    public async Task<IActionResult> CreateVitals(string id)
    {
        ViewBag.NurseId = id;

        ViewBag.Visits = await _context.Visits
            .Include(v => v.Patient)
            .ToListAsync();

        return View(new Vitals());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateVitals(Vitals vitals, string actionType, string nurseId, string returnUrl = null)
    {
        ModelState.Remove("VitalsId");
        ModelState.Remove("Visit");
        ModelState.Remove("Nurse");
        ModelState.Remove("NurseId");

        if (!ModelState.IsValid)
        {
            ViewBag.NurseId = nurseId;
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Visits = await _context.Visits
                .Include(v => v.Patient)
                .ToListAsync();

            return View(vitals);
        }

        if (string.IsNullOrEmpty(vitals.VitalsId))
        {
            var lastVitals = await _context.Vitals
                .OrderByDescending(v => v.VitalsId)
                .FirstOrDefaultAsync();

            int nextNumber = 1;

            if (lastVitals != null)
            {
                var numeric = int.Parse(lastVitals.VitalsId.Replace("VIT", ""));
                nextNumber = numeric + 1;
            }

            vitals.VitalsId = $"VIT{nextNumber:D3}";
            vitals.RecordedAt = DateTime.Now;
            vitals.NurseId = nurseId;
            vitals.IsDraft = actionType == "Draft";
            vitals.BloodPressure ??= "";
            vitals.HeartRate ??= "";
            vitals.RespiratoryRate ??= "";
            vitals.Notes ??= "";

            _context.Vitals.Add(vitals);
        }
        else
        {
            var existingVital = await _context.Vitals
                .FirstOrDefaultAsync(v => v.VitalsId == vitals.VitalsId);

            if (existingVital == null)
                return NotFound();

            existingVital.VisitId = vitals.VisitId;
            existingVital.Temperature = vitals.Temperature;
            existingVital.BloodPressure = vitals.BloodPressure ?? "";
            existingVital.HeartRate = vitals.HeartRate ?? "";
            existingVital.RespiratoryRate = vitals.RespiratoryRate ?? "";
            existingVital.Notes = vitals.Notes ?? "";
            existingVital.NurseId = nurseId;
            existingVital.IsDraft = actionType == "Draft";
            existingVital.RecordedAt = DateTime.Now;
        }

        await _context.SaveChangesAsync();

        if (actionType == "Draft")
            return RedirectToAction("DraftVitals", new { id = nurseId });

        return RedirectToAction("VitalsMenu", new { id = nurseId });
    }

    public async Task<IActionResult> DraftVitals(string id)
    {
        var drafts = await _context.Vitals
            .Include(v => v.Visit)
            .ThenInclude(v => v.Patient)
            .Where(v => v.NurseId == id && v.IsDraft)
            .OrderByDescending(v => v.RecordedAt)
            .ToListAsync();

        ViewBag.NurseId = id;
        return View(drafts);
    }

    public async Task<IActionResult> PastVitals(string id)
    {
        var savedVitals = await _context.Vitals
            .Include(v => v.Visit)
            .ThenInclude(v => v.Patient)
            .Include(v => v.Nurse)
            .Where(v => !v.IsDraft)
            .OrderByDescending(v => v.RecordedAt)
            .ToListAsync();

        ViewBag.NurseId = id;
        return View(savedVitals);
    }

    public async Task<IActionResult> EditVitals(string id, string returnUrl = null)
    {
        var vital = await _context.Vitals
            .Include(v => v.Visit)
            .ThenInclude(v => v.Patient)
            .FirstOrDefaultAsync(v => v.VitalsId == id);

        if (vital == null)
            return NotFound();

        ViewBag.ReturnUrl = returnUrl;

        ViewBag.Visits = await _context.Visits
            .Include(v => v.Patient)
            .ToListAsync();

        ViewBag.NurseId = vital.NurseId;

        return View("CreateVitals", vital);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteVitals(string id)
    {
        var vital = await _context.Vitals.FindAsync(id);

        if (vital == null)
            return NotFound();

        _context.Vitals.Remove(vital);
        await _context.SaveChangesAsync();

        return RedirectToAction("DraftVitals", new { id = vital.NurseId });
    }

    public async Task<IActionResult> DoctorDashboard(string id)
    {
        if (id == null)
            return NotFound();

        var staff = await _context.Staff
            .FirstOrDefaultAsync(s => s.StaffId == id);

        if (staff == null)
            return NotFound();

        ViewBag.UserType = "Staff";
        ViewBag.CurrentUserId = id;

        var today = DateTime.Today;

        var appointments = await _context.Appointments
            .Include(a => a.Patient)
            .Where(a => a.DoctorId == id && a.AppointmentDate.Date == today)
            .OrderBy(a => a.AppointmentTime)
            .ToListAsync();

        var viewModel = new DoctorDashboardViewModel
        {
            Staff = staff,
            Appointments = appointments ?? new List<Appointment>()
        };

        return View(viewModel);
    }

    public async Task<IActionResult> PatientRecordsMenu(string id, string search)
    {
        ViewBag.DoctorId = id;

        var patientsQuery = _context.Patients.AsQueryable();

        // Restrict normal doctors
        var staff = await _context.Staff.FirstOrDefaultAsync(s => s.StaffId == id);

        if (staff.StaffRole != "Emergency Doctor")
        {
            patientsQuery = patientsQuery.Where(p => p.AssignedDoctorId == id);
        }

        if (!string.IsNullOrEmpty(search))
        {
            patientsQuery = patientsQuery.Where(p =>
                p.PatientFirstName.Contains(search) ||
                p.PatientLastName.Contains(search) ||
                p.PatientId.Contains(search)
            );
        }

        var patients = await patientsQuery.ToListAsync();

        return View(patients);
    }

    public async Task<IActionResult> CreateVisit(string id)
    {
        ViewBag.DoctorId = id;

        ViewBag.Appointments = await _context.Appointments
            .Include(a => a.Patient)
            .Where(a => a.DoctorId == id && a.ArrivalConfirmation == true)
            .ToListAsync();

        return View(new Visit());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateVisit(Visit visit, string doctorId)
    {
        var appointment = await _context.Appointments
            .FirstOrDefaultAsync(a => a.AppointmentId == visit.AppointmentId);

        if (appointment == null)
            return NotFound();

        var last = await _context.Visits
            .OrderByDescending(v => v.VisitId)
            .FirstOrDefaultAsync();

        int nextNumber = 1;

        if (last != null)
        {
            var numeric = int.Parse(last.VisitId.Replace("VIS", ""));
            nextNumber = numeric + 1;
        }

        visit.VisitId = $"VIS{nextNumber:D3}";
        visit.VisitDateTime = DateTime.Now;
        visit.PatientId = appointment.PatientId;
        visit.DoctorId = doctorId;

        _context.Visits.Add(visit);
        await _context.SaveChangesAsync();

        return RedirectToAction("DoctorDashboard", new { id = doctorId });
    }

    public async Task<IActionResult> CreatePrescription(string id)
    {
        ViewBag.DoctorId = id;

        ViewBag.Visits = await _context.Visits
            .Include(v => v.Patient)
            .Where(v => v.DoctorId == id)
            .OrderByDescending(v => v.VisitDateTime)
            .ToListAsync();

        ViewBag.Pharmacists = await _context.Staff
            .Where(s => s.StaffRole == "Pharmacist" && s.ActiveStatus == "Active")
            .ToListAsync();

        return View(new Prescription());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePrescription(Prescription prescription, string doctorId)
    {
        var last = await _context.Prescriptions
            .OrderByDescending(p => p.PrescriptionId)
            .FirstOrDefaultAsync();

        int nextNumber = 1;

        if (last != null)
        {
            var numeric = int.Parse(last.PrescriptionId.Replace("PRE", ""));
            nextNumber = numeric + 1;
        }

        prescription.PrescriptionId = $"PRE{nextNumber:D3}";
        prescription.Notes ??= "";

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        return RedirectToAction("DoctorDashboard", new { id = doctorId });
    }

    public async Task<IActionResult> PatientInformation(string id)
    {
        var patient = await _context.Patients
            .Include(p => p.AssignedDoctor)
            .FirstOrDefaultAsync(p => p.PatientId == id);

        if (patient == null) return NotFound();

        return View(patient);
    }

    public async Task<IActionResult> PatientVitals(string id)
    {
        var vitals = await _context.Vitals
            .Include(v => v.Visit)
            .ThenInclude(v => v.Patient)
            .Where(v => v.Visit.PatientId == id && !v.IsDraft)
            .OrderByDescending(v => v.RecordedAt)
            .ToListAsync();

        return View(vitals);
    }
    public async Task<IActionResult> PatientLabResults(string id)
    {
        var results = await _context.LabTestResults
            .Include(t => t.Visit)
            .ThenInclude(v => v.Patient)
            .Where(t => t.Visit.PatientId == id && t.Status == "Completed")
            .OrderByDescending(t => t.ResultDate)
            .ToListAsync();

        return View(results);
    }
    public async Task<IActionResult> VisitHistory(string id)
    {
        var visits = await _context.Visits
            .Where(v => v.PatientId == id)
            .Select(v => new PatientTimelineViewModel
            {
                Date = v.VisitDateTime,
                Type = "Visit",
                RecordId = v.VisitId,
                DoctorId = v.DoctorId,
                Description = v.Notes
            })
            .ToListAsync();

        var diagnoses = await _context.Diagnoses
            .Include(d => d.Visit)
            .Where(d => d.Visit.PatientId == id)
            .Select(d => new PatientTimelineViewModel
            {
                Date = d.DiagnosisDate,
                Type = "Diagnosis",
                RecordId = d.DiagnosisId,
                DoctorId = d.Visit.DoctorId,
                Description = d.DiagnosisName + " - " + d.Description
            })
            .ToListAsync();

        var prescriptions = await _context.Prescriptions
            .Include(p => p.Visit)
            .Where(p => p.Visit.PatientId == id)
            .Select(p => new PatientTimelineViewModel
            {
                Date = p.Visit.VisitDateTime,
                Type = "Prescription",
                RecordId = p.PrescriptionId,
                DoctorId = p.Visit.DoctorId,
                Description = p.PrescriptionText
            })
            .ToListAsync();

        var timeline = visits
            .Concat(diagnoses)
            .Concat(prescriptions)
            .OrderByDescending(x => x.Date)
            .ToList();

        return View(timeline);
    }

    public async Task<IActionResult> ITPersonnelDashboard(string id)
    {
        ViewBag.UserType = "Staff";
        ViewBag.CurrentUserId = id;
        return await LoadDashboard(id, "ITPersonnelDashboard");
    }

    public async Task<IActionResult> TicketManager()
    {
        var tickets = await _context.Tickets
            .Include(t => t.AssignedTo)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();

        return View(tickets);
    }

    public async Task<IActionResult> CreateTicket(string id)
    {
        var staff = await _context.Staff.FirstOrDefaultAsync(s => s.StaffId == id);

        if (staff == null)
            return NotFound();

        ViewBag.StaffId = id;
        ViewBag.StaffRole = staff.StaffRole;

        return View(new Ticket());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateTicket(Ticket ticket, string staffId)
    {
        var staff = await _context.Staff.FirstOrDefaultAsync(s => s.StaffId == staffId);

        if (staff == null)
            return NotFound();

        var last = await _context.Tickets
            .OrderByDescending(t => t.TicketId)
            .FirstOrDefaultAsync();

        int nextNumber = 1;

        if (last != null)
        {
            var numeric = int.Parse(last.TicketId.Replace("TCK", ""));
            nextNumber = numeric + 1;
        }

        ticket.TicketId = $"TCK{nextNumber:D3}";
        ticket.SenderId = staffId;
        ticket.SenderRole = staff.StaffRole;
        ticket.Status = "Open";
        ticket.CreatedAt = DateTime.Now;

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        return RedirectToAction("MyTickets", new { id = staffId });
    }

    public async Task<IActionResult> MyTickets(string id)
    {
        var tickets = await _context.Tickets
            .Where(t => t.SenderId == id)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();

        ViewBag.StaffId = id;

        return View(tickets);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateTicketStatus(string ticketId, string status)
    {
        var ticket = await _context.Tickets.FindAsync(ticketId);

        if (ticket == null)
            return NotFound();

        ticket.Status = status;

        await _context.SaveChangesAsync();

        return RedirectToAction("TicketManager");
    }

    public async Task<IActionResult> PasswordReset()
    {
        var accounts = await _context.UserAccounts
            .Include(a => a.Staff)
            .Include(a => a.Patient)
            .OrderBy(a => a.PasswordLastChanged)
            .ToListAsync();

        return View(accounts);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(int accountId, string newPassword)
    {
        var account = await _context.UserAccounts.FindAsync(accountId);

        if (account == null)
            return NotFound();

        account.Password = _passwordHasher.HashPassword(account, newPassword.Trim());
        account.PasswordLastChanged = DateTime.Now;

        await _context.SaveChangesAsync();

        return RedirectToAction("PasswordReset");
    }

    public async Task<IActionResult> DeviceManagement()
    {
        var devices = await _context.Devices
            .Include(d => d.AssignedStaff)
            .OrderBy(d => d.DeviceName)
            .ToListAsync();

        return View(devices);
    }

    public async Task<IActionResult> CreateDevice()
    {
        ViewBag.Staff = await _context.Staff.ToListAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateDevice(Device device)
    {
        var last = await _context.Devices
            .OrderByDescending(d => d.DeviceId)
            .FirstOrDefaultAsync();

        int next = 1;

        if (last != null)
        {
            var num = int.Parse(last.DeviceId.Replace("DEV", ""));
            next = num + 1;
        }

        device.DeviceId = $"DEV{next:D3}";
        device.LastUpdated = DateTime.Now;

        _context.Devices.Add(device);
        await _context.SaveChangesAsync();

        return RedirectToAction("DeviceManagement");
    }

    public async Task<IActionResult> EditDevice(string id, string returnUrl = null)
    {
        var device = await _context.Devices.FindAsync(id);
        if (device == null) return NotFound();

        ViewBag.ReturnUrl = returnUrl;
        ViewBag.Staff = await _context.Staff.ToListAsync();

        return View(device);
    }

    [HttpPost]
    public async Task<IActionResult> EditDevice(Device device, string returnUrl = null)
    {
        var existing = await _context.Devices.FindAsync(device.DeviceId);

        if (existing == null) return NotFound();

        existing.DeviceName = device.DeviceName;
        existing.DeviceType = device.DeviceType;
        existing.Status = device.Status;
        existing.AssignedStaffId = device.AssignedStaffId;
        existing.Room = device.Room;
        existing.LastUpdated = DateTime.Now;

        await _context.SaveChangesAsync();

        if (!string.IsNullOrEmpty(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("DeviceManagement");
    }

    public async Task<IActionResult> SystemAdminDashboard(string id)
    {
        ViewBag.UserType = "Staff";
        ViewBag.CurrentUserId = id;
        return await LoadDashboard(id, "SystemAdminDashboard");
    }


    public async Task<IActionResult> SystemStats()
    {
        var weekStart = DateTime.Today.AddDays(-7);
        var yearStart = new DateTime(DateTime.Today.Year, 1, 1);

        ViewBag.WeeklyAppointments = await _context.Appointments
            .CountAsync(a => a.AppointmentDate >= weekStart);

        ViewBag.WeeklyVisits = await _context.Visits
            .CountAsync(v => v.VisitDateTime >= weekStart);

        ViewBag.WeeklyLabResults = await _context.LabTestResults
            .CountAsync(l => l.UploadDate >= weekStart);

        ViewBag.WeeklyVitals = await _context.Vitals
            .CountAsync(v => v.RecordedAt >= weekStart);

        ViewBag.YearAppointments = await _context.Appointments
            .CountAsync(a => a.AppointmentDate >= yearStart);

        ViewBag.YearPatients = await _context.Patients
            .CountAsync(p => p.CreationDate >= yearStart);

        ViewBag.YearVisits = await _context.Visits
            .CountAsync(v => v.VisitDateTime >= yearStart);

        ViewBag.ActiveStaff = await _context.Staff
            .CountAsync(s => s.ActiveStatus == "Active");

        ViewBag.InactiveStaff = await _context.Staff
            .CountAsync(s => s.ActiveStatus == "Inactive");
        ViewBag.WeeklyLostAppointments = await _context.Appointments
            .CountAsync(a =>
                a.AppointmentDate >= weekStart &&
                a.AppointmentDate < DateTime.Today &&
                a.ArrivalConfirmation == false
            );
        ViewBag.YearLostAppointments = await _context.Appointments
            .CountAsync(a =>
                a.AppointmentDate >= yearStart &&
                a.AppointmentDate < DateTime.Today &&
                a.ArrivalConfirmation == false
            );

        var totalAppointments = await _context.Appointments
            .CountAsync(a => a.AppointmentDate >= yearStart);

        ViewBag.MissedRate = totalAppointments == 0
                    ? 0
                    : (double)ViewBag.YearLostAppointments / totalAppointments * 100;
        var topDoctor = await _context.Visits
            .Include(v => v.Doctor)
            .Where(v => v.VisitDateTime >= yearStart)
            .GroupBy(v => new
            {
                v.DoctorId,
                v.Doctor.StaffFirstName,
                v.Doctor.StaffLastName
            })
            .Select(g => new
            {
                DoctorName = g.Key.StaffFirstName + " " + g.Key.StaffLastName,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .FirstOrDefaultAsync();

        ViewBag.TopDoctor = topDoctor == null
            ? "No visits yet"
            : $"{topDoctor.DoctorName} ({topDoctor.Count} visits)";

        return View();
    }

    public async Task<IActionResult> DeactivateAccounts()
    {
        var accounts = await _context.UserAccounts
            .Include(a => a.Staff)
            .Include(a => a.Patient)
            .Where(a => a.AccountStatus == "Inactive" || a.AccountStatus == "Deactivated")
            .OrderBy(a => a.Role)
            .ThenBy(a => a.Email)
            .ToListAsync();

        return View(accounts);
    }

    public IActionResult PatientRecords()
    {
        return View();
    }

    public IActionResult UpdateVitals()
    {
        return View();
    }

    public IActionResult DatabaseOptions()
    {
        return View();
    }

    public async Task<IActionResult> Calendar(string id, string userType)
    {
        var calendarEvents = await _context.CalendarEvents
            .Where(e => e.UserId == id && e.UserType == userType)
            .OrderBy(e => e.EventDate)
            .ThenBy(e => e.EventTime)
            .ToListAsync();

        var appointmentEvents = new List<CalendarEvent>();

        if (userType == "Staff")
        {
            var staff = await _context.Staff.FirstOrDefaultAsync(s => s.StaffId == id);

            if (staff != null && (staff.StaffRole == "Doctor" || staff.StaffRole == "Emergency Doctor"))
            {
                var appointments = await _context.Appointments
                    .Include(a => a.Patient)
                    .Where(a => a.DoctorId == id)
                    .ToListAsync();

                appointmentEvents = appointments.Select(a => new CalendarEvent
                {
                    CalendarEventId = a.AppointmentId,
                    UserId = id,
                    UserType = "Staff",
                    Title = "Appointment with " + a.Patient.PatientFirstName + " " + a.Patient.PatientLastName,
                    Description = a.Notes,
                    EventDate = a.AppointmentDate,
                    EventTime = a.AppointmentTime,
                    CreatedAt = a.CreationDate
                }).ToList();
            }
        }

        if (userType == "Patient")
        {
            var appointments = await _context.Appointments
                .Include(a => a.Doctor)
                .Where(a => a.PatientId == id)
                .ToListAsync();

            appointmentEvents = appointments.Select(a => new CalendarEvent
            {
                CalendarEventId = a.AppointmentId,
                UserId = id,
                UserType = "Patient",
                Title = "Appointment with Dr. " + a.Doctor.StaffFirstName + " " + a.Doctor.StaffLastName,
                Description = a.Notes,
                EventDate = a.AppointmentDate,
                EventTime = a.AppointmentTime,
                CreatedAt = a.CreationDate
            }).ToList();
        }

        var allEvents = calendarEvents
            .Concat(appointmentEvents)
            .OrderBy(e => e.EventDate)
            .ThenBy(e => e.EventTime)
            .ToList();

        ViewBag.UserId = id;
        ViewBag.UserType = userType;

        return View(allEvents);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddCalendarEvent(
    string userId,
    string userType,
    string title,
    string description,
    DateTime eventDate,
    string eventTime)
    {
        var last = await _context.CalendarEvents
            .OrderByDescending(e => e.CalendarEventId)
            .FirstOrDefaultAsync();

        int nextNumber = 1;

        if (last != null)
        {
            var numeric = int.Parse(last.CalendarEventId.Replace("CAL", ""));
            nextNumber = numeric + 1;
        }

        var newEvent = new CalendarEvent
        {
            CalendarEventId = $"CAL{nextNumber:D3}",
            UserId = userId,
            UserType = userType,
            Title = title,
            Description = description ?? "",
            EventDate = eventDate,
            EventTime = eventTime,
            CreatedAt = DateTime.Now
        };

        _context.CalendarEvents.Add(newEvent);
        await _context.SaveChangesAsync();

        return RedirectToAction("Calendar", new { id = userId, userType = userType });
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
        Response.Headers["Pragma"] = "no-cache";
        Response.Headers["Expires"] = "0";

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> AllTickets()
    {
        var tickets = await _context.Tickets
            .Include(t => t.AssignedTo)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();

        return View(tickets);
    }

    public async Task<IActionResult> AllNotifications()
    {
        var notifications = await _context.Notifications
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();

        return View(notifications);
    }

    public async Task<IActionResult> AllVitals()
    {
        var vitals = await _context.Vitals
            .Include(v => v.Visit)
            .ThenInclude(v => v.Patient)
            .Include(v => v.Nurse)
            .OrderByDescending(v => v.RecordedAt)
            .ToListAsync();

        return View(vitals);
    }

    public async Task<IActionResult> AllLabResults()
    {
        var results = await _context.LabTestResults
            .Include(l => l.Visit)
            .ThenInclude(v => v.Patient)
            .Include(l => l.LabTech)
            .OrderByDescending(l => l.UploadDate)
            .ToListAsync();

        return View(results);
    }

    public async Task<IActionResult> AllPrescriptions()
    {
        var prescriptions = await _context.Prescriptions
            .Include(p => p.Visit)
            .ThenInclude(v => v.Patient)
            .Include(p => p.Pharmacist)
            .OrderByDescending(p => p.Visit.VisitDateTime)
            .ToListAsync();

        return View(prescriptions);
    }

    public async Task<IActionResult> AllAppointments()
    {
        var appointments = await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .OrderByDescending(a => a.AppointmentDate)
            .ThenBy(a => a.AppointmentTime)
            .ToListAsync();

        foreach (var appointment in appointments)
        {
            if (appointment.AppointmentDate.Date < DateTime.Today &&
                appointment.ArrivalConfirmation == false &&
                appointment.AppointmentStatus != "No Show")
            {
                appointment.AppointmentStatus = "No Show";
            }
        }

        await _context.SaveChangesAsync();

        return View(appointments);
    }

    public async Task<IActionResult> EditAppointment(string id, string returnUrl = null)
    {
        var appointment = await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .FirstOrDefaultAsync(a => a.AppointmentId == id);

        if (appointment == null)
            return NotFound();

        ViewBag.ReturnUrl = returnUrl;
        ViewBag.Patients = await _context.Patients.ToListAsync();

        ViewBag.Doctors = await _context.Staff
            .Where(s =>
                (s.StaffRole == "Doctor" || s.StaffRole == "Emergency Doctor") &&
                s.ActiveStatus == "Active")
            .ToListAsync();

        return View(appointment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAppointment(Appointment appointment, string returnUrl = null)
    {
        var existing = await _context.Appointments
            .FirstOrDefaultAsync(a => a.AppointmentId == appointment.AppointmentId);

        if (existing == null)
            return NotFound();

        var clash = await _context.Appointments.AnyAsync(a =>
            a.AppointmentId != appointment.AppointmentId &&
            a.DoctorId == appointment.DoctorId &&
            a.AppointmentDate.Date == appointment.AppointmentDate.Date &&
            a.AppointmentTime == appointment.AppointmentTime);

        if (clash)
        {
            ModelState.AddModelError("", "This doctor already has an appointment at that time.");

            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Patients = await _context.Patients.ToListAsync();
            ViewBag.Doctors = await _context.Staff
                .Where(s =>
                    (s.StaffRole == "Doctor" || s.StaffRole == "Emergency Doctor") &&
                    s.ActiveStatus == "Active")
                .ToListAsync();

            return View(appointment);
        }

        existing.PatientId = appointment.PatientId;
        existing.DoctorId = appointment.DoctorId;
        existing.AppointmentDate = appointment.AppointmentDate;
        existing.AppointmentTime = appointment.AppointmentTime;
        existing.AppointmentStatus = appointment.AppointmentStatus;
        existing.ArrivalConfirmation = appointment.ArrivalConfirmation;
        existing.Notes = appointment.Notes ?? "";

        await _context.SaveChangesAsync();

        if (!string.IsNullOrEmpty(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("AllAppointments");
    }

    public async Task<IActionResult> AllVisits()
    {
        var visits = await _context.Visits
            .Include(v => v.Patient)
            .Include(v => v.Doctor)
            .Include(v => v.Appointment)
            .OrderByDescending(v => v.VisitDateTime)
            .ToListAsync();

        return View(visits);
    }

    [HttpPost]
    public async Task<IActionResult> DeactivatePatient(string id)
    {
        var account = await _context.UserAccounts
            .FirstOrDefaultAsync(a => a.PatientId == id);

        if (account == null)
            return NotFound();

        account.AccountStatus = "Deactivated";

        await _context.SaveChangesAsync();

        return RedirectToAction("PatientLookup");
    }

    [HttpPost]
    public async Task<IActionResult> ActivatePatient(string id)
    {
        var account = await _context.UserAccounts
            .FirstOrDefaultAsync(a => a.PatientId == id);

        if (account == null)
            return NotFound();

        account.AccountStatus = "Active";

        await _context.SaveChangesAsync();

        return RedirectToAction("PatientLookup");
    }

    public async Task<IActionResult> MarkNotificationRead(string id)
    {
        var notification = await _context.Notifications
            .FirstOrDefaultAsync(n => n.NotificationId == id);

        if (notification == null)
            return NotFound();

        notification.IsRead = true;
        await _context.SaveChangesAsync();

        return Ok();
    }

    public async Task<IActionResult> CreateDiagnosis(string id)
    {
        ViewBag.DoctorId = id;

        ViewBag.Visits = await _context.Visits
            .Include(v => v.Patient)
            .Where(v => v.DoctorId == id)
            .OrderByDescending(v => v.VisitDateTime)
            .ToListAsync();

        return View(new Diagnosis());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateDiagnosis(Diagnosis diagnosis, string doctorId)
    {
        ModelState.Remove("DiagnosisId");
        ModelState.Remove("Visit");

        if (!ModelState.IsValid)
        {
            ViewBag.DoctorId = doctorId;
            ViewBag.Visits = await _context.Visits
                .Include(v => v.Patient)
                .Where(v => v.DoctorId == doctorId)
                .OrderByDescending(v => v.VisitDateTime)
                .ToListAsync();

            return View(diagnosis);
        }

        var last = await _context.Diagnoses
            .OrderByDescending(d => d.DiagnosisId)
            .FirstOrDefaultAsync();

        int nextNumber = 1;

        if (last != null)
        {
            var numeric = int.Parse(last.DiagnosisId.Replace("DIA", ""));
            nextNumber = numeric + 1;
        }

        diagnosis.DiagnosisId = $"DIA{nextNumber:D3}";
        diagnosis.DiagnosisDate = DateTime.Now;

        _context.Diagnoses.Add(diagnosis);
        await _context.SaveChangesAsync();

        return RedirectToAction("DoctorDashboard", new { id = doctorId });
    }


    //Functions
    private async Task<Staff?> GetStaffById(string id)
    {
        return await _context.Staff
            .FirstOrDefaultAsync(s => s.StaffId == id);
    }

    private async Task<IActionResult> LoadDashboard(string id, string viewName)
    {
        if (id == null)
            return NotFound();

        var staff = await GetStaffById(id);

        if (staff == null)
            return NotFound();

        return View(viewName, staff);
    }
    private async Task DeactivateInactiveAccounts()
    {
        var cutoffDate = DateTime.Now.AddYears(-5);

        var inactiveAccounts = await _context.UserAccounts
            .Where(a => a.LastLogin <= cutoffDate && a.AccountStatus == "Active")
            .ToListAsync();

        foreach (var account in inactiveAccounts)
        {
            account.AccountStatus = "Deactivated";

            if (!string.IsNullOrEmpty(account.StaffId))
            {
                var staff = await _context.Staff
                    .FirstOrDefaultAsync(s => s.StaffId == account.StaffId);

                if (staff != null)
                    staff.ActiveStatus = "Inactive";
            }
        }

        await _context.SaveChangesAsync();
    }


}



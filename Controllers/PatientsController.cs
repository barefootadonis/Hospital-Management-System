using Hospital_Management_System.Data;
using Hospital_Management_System.Models;
using Hospital_Management_System.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Hospital_Management_System.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;
        private readonly PasswordHasher<UserAccount> _passwordHasher = new();
        public PatientsController(ApplicationDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        // GET: Patients
        public async Task<IActionResult> Index()
        {
            return View(await _context.Patients.ToListAsync());
        }
        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Patients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Patient patient, string Password)
        {
            if (!ModelState.IsValid)
                return View("Signup", patient);

            // Check if email already exists
            var existingAccount = await _context.UserAccounts
                .FirstOrDefaultAsync(a => a.Email == patient.PatientEmail);

            if (existingAccount != null)
            {
                ModelState.AddModelError("", "An account with this email already exists.");
                return View("Signup", patient);
            }

            // Generate Patient ID
            var lastPatient = await _context.Patients
                .OrderByDescending(p => p.PatientId)
                .FirstOrDefaultAsync();

            int nextNumber = 1;

            if (lastPatient != null)
            {
                var numericPart = int.Parse(lastPatient.PatientId.Replace("PAT", ""));
                nextNumber = numericPart + 1;
            }

            patient.PatientId = $"PAT{nextNumber:D3}";
            patient.CreationDate = DateTime.Now;
            var assignedDoctor = await _context.Staff
            .Where(s => s.StaffRole == "Doctor" && s.ActiveStatus == "Active")
            .Select(s => new
            {
                DoctorId = s.StaffId,
                PatientCount = _context.Patients.Count(p => p.AssignedDoctorId == s.StaffId)
            })
            .OrderBy(x => x.PatientCount)
            .ThenBy(x => x.DoctorId)
            .FirstOrDefaultAsync();

                    if (assignedDoctor == null)
                    {
                        ModelState.AddModelError("", "No active doctors are available to assign.");
                        return View("Signup", patient);
                    }

                    patient.AssignedDoctorId = assignedDoctor.DoctorId;
            patient.PatientOtherNames ??= "";

            _context.Patients.Add(patient);

            var account = new UserAccount
            {
                Email = patient.PatientEmail,
                Role = "Patient",
                AccountStatus = "Active",
                LastLogin = DateTime.Now,
                PatientId = patient.PatientId,
                PasswordLastChanged = DateTime.Now
            };

            account.Password = _passwordHasher.HashPassword(account, Password);

            _context.UserAccounts.Add(account);

            await _context.SaveChangesAsync();

            var doctor = await _context.Staff
     .FirstOrDefaultAsync(s => s.StaffId == patient.AssignedDoctorId);

            var doctorName = doctor != null
                ? $"Dr. {doctor.StaffFirstName} {doctor.StaffLastName}"
                : "your assigned doctor";

            await _emailService.SendAsync(
                patient.PatientEmail,
                "Welcome to Hospital Management System",
                $"Welcome {patient.PatientFirstName},\n\n" +
                $"Your Patient ID is: {patient.PatientId}\n" +
                $"Your Assigned Doctor is: {doctorName}\n\n" +
                "Please keep this information safe."
            );

            return RedirectToAction("Dashboard", new { id = patient.PatientId });
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(string id, string returnUrl = null)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return NotFound();

            ViewBag.ReturnUrl = returnUrl;

            return View(patient);
        }
        // POST: Patients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Patient patient, string returnUrl = null)
        {
            if (id != patient.PatientId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Patients.Update(patient);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("PatientLookup", "Staff");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(patient);
        }
        // GET: Patients/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.PatientId == id);
            if (patient == null) return NotFound();
            return View(patient);
        }
        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.PatientId == id);
            if (patient == null) return NotFound();
            return View(patient);
        }
        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjaxDelete(string id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
                return NotFound(new { success = false, message = "Patient not found." });
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return Json(new { success = true, id });
        }
        public async Task<IActionResult> Dashboard(string id)
        {

            if (HttpContext.Session.GetString("UserId") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null) return NotFound();

            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.PatientId == id);

            if (patient == null) return NotFound();
            ViewBag.UserType = "Patient";
            ViewBag.CurrentUserId = id;

            return View(patient);
        }

        public IActionResult Signup()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            await DeactivateInactivePatientAccounts();
            if (!ModelState.IsValid)
                return View(model);

            var account = await _context.UserAccounts
                .FirstOrDefaultAsync(a =>
                    a.Email == model.EmailOrId ||
                    a.PatientId == model.EmailOrId
                );

            if (account == null)
            {
                ModelState.AddModelError("", "Invalid login credentials.");
                return View(model);
            }

            if (account.AccountStatus == "Deactivated")
            {
                ModelState.AddModelError("", "This account has been deactivated.");
                return View(model);
            }

            bool passwordValid = false;

            try
            {
                var result = _passwordHasher.VerifyHashedPassword(account, account.Password, model.Password);
                passwordValid = result != PasswordVerificationResult.Failed;
            }
            catch (FormatException)
            {
                passwordValid = account.Password == model.Password;

                if (passwordValid)
                {
                    account.Password = _passwordHasher.HashPassword(account, model.Password);
                    account.PasswordLastChanged = DateTime.Now;
                    await _context.SaveChangesAsync();
                }
            }

            if (!passwordValid)
            {
                ModelState.AddModelError("", "Invalid login credentials.");
                return View(model);
            }

            HttpContext.Session.SetString("UserId", account.PatientId);
            HttpContext.Session.SetString("UserType", "Patient");

            account.LastLogin = DateTime.Now;
            await _context.SaveChangesAsync();

            return RedirectToAction("Dashboard", new { id = account.PatientId });
        }

        public async Task<IActionResult> Profile(string id)
        {
            if (id == null)
                return NotFound();

            var patient = await _context.Patients
                .Include(p => p.AssignedDoctor)
                .FirstOrDefaultAsync(p => p.PatientId == id);

            if (patient == null)
                return NotFound();

            return View(patient);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Deactivate(string id)
        {
            var account = await _context.UserAccounts
                .FirstOrDefaultAsync(a => a.PatientId == id);

            if (account == null)
                return NotFound();

            account.AccountStatus = "Deactivated";

            await _context.SaveChangesAsync();

            return RedirectToAction("PatientLookup", "Staff");
        }

        [HttpPost]
        public async Task<IActionResult> Activate(string id)
        {
            var account = await _context.UserAccounts
                .FirstOrDefaultAsync(a => a.PatientId == id);

            if (account == null)
                return NotFound();

            account.AccountStatus = "Active";

            await _context.SaveChangesAsync();

            return RedirectToAction("PatientLookup", "Staff");
        }

        public async Task<IActionResult> BookAppointment(string id)
        {
            var patient = await _context.Patients
                .Include(p => p.AssignedDoctor)
                .FirstOrDefaultAsync(p => p.PatientId == id);

            if (patient == null)
                return NotFound();

            ViewBag.PatientId = id;
            ViewBag.DoctorId = patient.AssignedDoctorId;
            ViewBag.DoctorName = patient.AssignedDoctor != null
                ? patient.AssignedDoctor.StaffFirstName + " " + patient.AssignedDoctor.StaffLastName
                : "Assigned Doctor";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookAppointment(string patientId, DateTime appointmentDate, string appointmentTime, string notes)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.PatientId == patientId);

            if (patient == null)
                return NotFound();

            if (string.IsNullOrEmpty(patient.AssignedDoctorId))
            {
                ModelState.AddModelError("", "You do not currently have an assigned doctor.");
                ViewBag.PatientId = patientId;
                return View();
            }

            var appointmentExists = await _context.Appointments.AnyAsync(a =>
                a.DoctorId == patient.AssignedDoctorId &&
                a.AppointmentDate.Date == appointmentDate.Date &&
                a.AppointmentTime == appointmentTime
            );

            if (appointmentExists)
            {
                ModelState.AddModelError("", "This appointment time is already booked. Please choose another time.");
                ViewBag.PatientId = patientId;
                ViewBag.DoctorId = patient.AssignedDoctorId;
                return View();
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
                PatientId = patient.PatientId,
                DoctorId = patient.AssignedDoctorId,
                AppointmentDate = appointmentDate,
                AppointmentTime = appointmentTime,
                Notes = notes ?? "",
                CreationDate = DateTime.Now,
                AppointmentStatus = "Scheduled",
                ArrivalConfirmation = false
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Dashboard", new { id = patientId });
        }

        private async Task DeactivateInactivePatientAccounts()
        {
            var cutoffDate = DateTime.Now.AddYears(-5);

            var inactiveAccounts = await _context.UserAccounts
                .Where(a => a.LastLogin <= cutoffDate &&
                            a.AccountStatus == "Active" &&
                            a.PatientId != null)
                .ToListAsync();

            foreach (var account in inactiveAccounts)
            {
                account.AccountStatus = "Deactivated";
            }

            await _context.SaveChangesAsync();
        }


    }
}

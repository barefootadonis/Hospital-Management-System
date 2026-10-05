# Hospital Management System
A database-driven hospital management system developed using **C# and ASP.NET MVC**. The application simulates the management of patients, staff, appointments, visits, prescriptions, laboratory results, diagnoses, tickets, notifications and other hospital-related processes.
The system uses **role-based functionality**, with different dashboards and features available depending on whether the user is a patient or a member of hospital staff.
This project was developed as a portfolio project to demonstrate practical experience in backend development, database integration, MVC architecture, CRUD operations and building a multi-role web application.

## Features

### Authentication & Role-Based Access
- Login using an email address or user ID
- Password-based authentication
- Role-based dashboards
- Different functionality depending on the user's role
- Staff and patient accounts
- Login validation and error messages

### Patient Management
- View patient information
- Manage patient records
- View appointments
- View visits and associated information
- View prescriptions and relevant medical information
- Access patient-specific functionality through the patient dashboard

### Appointment & Arrival Management
- Create and manage appointments
- Assign appointments to doctors and patients
- Record appointment status
- Receptionists can confirm when a patient has arrived
- Doctor dashboards update to show patients who have arrived
- Visual indication of patient arrival status
- Notification sound when an arriving patient is detected on the doctor's dashboard

### Doctor Functionality
Doctors have access to functionality including:
- Doctor dashboard
- Work calendar
- Patient records
- Creating visits
- Creating prescriptions
- Creating diagnoses
- Viewing patient appointments
- Viewing patient arrival status
- Ticket management
- Viewing relevant laboratory results

### Nurse Functionality
Nurses can access functionality related to:
- Nurse dashboard
- Patient information
- Recording patient vitals
- Managing vital records
- Saving vital records as drafts
- Continuing previously saved draft records

### Laboratory Technician Functionality
Laboratory technicians can:
- Access laboratory functionality
- Create laboratory test results
- Record test information
- Upload and manage test results
- Save work and continue editing pending results

### Pharmacist Functionality
Pharmacists can:
- Access the pharmacist dashboard
- View prescriptions assigned to them
- Manage prescription-related tasks
- Update prescription status

### Receptionist Functionality
Receptionists can:
- Manage appointments
- Confirm patient arrivals
- Access relevant patient information
- Use the receptionist dashboard

### IT Personnel Functionality
IT personnel can:
- Access IT-related functionality
- Manage support tickets
- View and manage technical issues
- Work with hospital device information

### System Administrator Functionality
System administrators have access to administrative functionality, including:
- Managing staff
- Managing patient information
- Managing user accounts
- Managing hospital devices
- Managing tickets
- Managing notifications
- Accessing administrative dashboards

### Ticket System

The application includes a ticket management system allowing staff to report and manage issues.
Tickets include:
- Ticket ID
- Subject
- Description
- Priority
- Status
- Sender
- Assigned staff member
- Creation date

Ticket statuses include:
- Open
- In Progress
- Resolved

### Notification System
The system includes an internal notification system that can be used to send messages between hospital users.
Notifications support:
- Sender identification
- Receiver identification
- Sender and receiver types
- Notification titles
- Notification messages
- Read/unread status
- Creation dates

### Calendar
Staff and patients can access calendar functionality for relevant appointments and events.
Calendar events contain:
- Event title
- Description
- Date
- Time
- User association
- User type

### Email Functionality
The application includes email functionality using **Gmail SMTP**.
Email functionality can be used for application-related communication and testing.
Real email credentials are not included in the repository.

# User Roles
The system supports the following user roles:

| Role | Example Functionality |
|---|---|
| Receptionist | Appointments, patient arrivals |
| Doctor | Visits, diagnoses, prescriptions, patient records |
| Nurse | Patient vitals and patient information |
| Laboratory Technician | Laboratory test results |
| Pharmacist | Prescription management |
| IT Personnel | Tickets and device management |
| System Administrator | Administrative management |
| Patient | Patient-specific records and appointments |

When a user logs in, the application identifies their associated role and directs them to the appropriate dashboard and functionality.

# Patient Arrival System
One of the workflows implemented in the system is the patient arrival process.
When a patient arrives at the hospital, a receptionist can confirm their arrival through the appointment system.
The appointment's arrival status is then reflected on the relevant doctor's dashboard.
Doctors can therefore see which patients have arrived and are ready to be seen.
The doctor dashboard also periodically refreshes to check for changes, and an audio notification can be triggered when an arriving patient is detected.

# Medical Records
The system contains several connected medical entities.
These include:
- Patients
- Staff
- Appointments
- Visits
- Diagnoses
- Prescriptions
- Laboratory test results
- Patient vitals

These entities are connected through foreign-key relationships using Entity Framework Core.
For example:
```text
Patient
   |
   +-- Appointment
   |
   +-- Visit
          |
          +-- Diagnosis
          |
          +-- Prescription
          |
          +-- Lab Test Result
          |
          +-- Vitals

# Test Accounts
The application includes pre-created test accounts for the different user roles.
The usernames/IDs and passwords can be found in the **database setup** and within the **`ApplicationDbContext`** file.
All accounts and credentials are fictional test data created specifically for this project and do not contain real patient or staff information.
These accounts can be used to test the different dashboards and role-specific functionality.

# Email Configuration
The application includes email functionality using **Gmail SMTP**.
Real email credentials are not included in the repository. To test the email functionality locally, configure the `EmailSettings` section in `appsettings.json` using your own Gmail SMTP credentials.
Use the following format:

```json
"EmailSettings": {
  "Host": "smtp.gmail.com",
  "Port": 587,
  "EnableSsl": true,
  "Username": "YOUR_EMAIL_ADDRESS",
  "Password": "YOUR_EMAIL_APP_PASSWORD"
}
```

For Gmail, an App Password should be used instead of your normal account password.
Do not commit real passwords or other sensitive credentials to GitHub.


# Technologies Used
* C#
* ASP.NET MVC
* .NET
* Entity Framework Core
* MySQL
* HTML
* CSS
* JavaScript
* Visual Studio
* Git & GitHub

# Skills Demonstrated
This project demonstrates practical experience with:
* C# and object-oriented programming
* ASP.NET MVC architecture
* Entity Framework Core
* MySQL database integration
* CRUD operations
* Database design and relationships
* Role-based access and functionality
* Authentication and session management
* Backend development
* Web application development
* Form handling and validation
* Managing connected data and workflows
* Git and GitHub

# Running the Project
1. Clone or download the repository.
2. Open the solution in Visual Studio.
3. Configure a local MySQL database.
4. Update the database connection string in `appsettings.json`.
5. Apply the included Entity Framework Core migrations.
6. Configure the email settings if email functionality is being tested.
7. Build and run the application.
8. Use one of the test accounts provided in the database setup or `ApplicationDbContext`.

# Project Purpose
This project was developed as a portfolio project to demonstrate the design and development of a database-driven, multi-role web application.
It combines backend development, database management, MVC architecture and role-specific functionality to simulate processes within a hospital environment.


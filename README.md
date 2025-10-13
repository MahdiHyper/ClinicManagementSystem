🏥 Clinic Management System

A desktop-based medical management system built with C# (Windows Forms) and SQL Server.
The project helps clinics efficiently manage patients, appointments, medical records, prescriptions, and billing through a clean, layered architecture.

📋 Overview

This system is designed to simplify daily clinic operations by providing a centralized interface for doctors and staff.
It allows creating and managing patient profiles, scheduling appointments, recording diagnoses, issuing prescriptions, and generating billing records — all in one place.

⚙️ Features
👩‍⚕️ Patient Management

Add, edit, delete, and search for patients.

Store complete personal and medical information.

📅 Appointment Management

Schedule and manage appointments with date conflict validation.

Start and end appointments dynamically.

Link appointments to patient and doctor records.

🧾 Medical Records

Record diagnoses, symptoms, and doctor notes.

Automatically connect with the related appointment.

View full medical history for each patient.

💊 Prescriptions

Add prescriptions during an active session.

Attach medicines and instructions to each record.

View previous prescriptions through the medical record.

💰 Billing & Payments

Create invoices linked to appointments.

Record payment methods and amounts.

Generate summaries for reporting.

📈 Reports Dashboard (in progress)

Overview of daily and monthly statistics.

Quick insights like number of patients, today’s appointments, etc.

🏗️ System Architecture

The project follows a Layered Architecture (3-Tier Design):

UI Layer (Windows Forms) – Handles user interaction and presentation.

Business Logic Layer (BLL) – Contains core logic, validation, and rules.

Data Access Layer (DAL) – Manages all communication with the SQL Server database.

🧠 Technologies Used

C# (.NET Framework / WinForms)

Microsoft SQL Server

ADO.NET

Layered Architecture Design Pattern

🗄️ Database Design (Summary)

Main Tables:

Patients

Doctors

Appointments

MedicalRecords

Prescriptions

Payments

Users

Each module is connected logically (e.g., Appointment → MedicalRecord → Prescription → Payment).

🚀 How to Run the Project

Clone the repository:

git clone https://github.com/yourusername/ClinicManagementSystem.git


Open the solution in Visual Studio.

Set up the database in SQL Server using the provided .sql script (if available).

Update the connection string in App.config or DataAccessLayer as needed.

Build and run the project.

📸 Screenshots (Coming Soon)

Will include UI previews for Patients, Appointments, Records, and Billing.

🧾 Future Improvements

Add role-based user authentication.

Integrate advanced reporting system.

Export invoices and records to PDF.

Add cloud synchronization for multi-device access.

👨‍💻 Author

Mahdi Al-Suleiman

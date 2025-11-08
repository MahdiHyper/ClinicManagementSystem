Clinic Management System

Overview  
Clinic Management System is a desktop application built to streamline and automate clinic operations.  
It manages patients, doctors, appointments, medical records, prescriptions, and payments within a unified system.  
The system is designed for small and medium-sized clinics to enhance efficiency, data accuracy, and patient care.

------------------------------------------------------------

Features  
- Manage patients and doctors with complete personal and medical details.  
- Schedule and track appointments efficiently.  
- Maintain medical records with diagnosis history.  
- Create and manage prescriptions with dosage details.  
- Export invoices and prescriptions as PDF files.  
- Secure user login and role-based access.  
- Dashboard with key statistics and upcoming appointments.

------------------------------------------------------------

Architecture Overview  
The project follows a three-layer architecture for better organization and scalability:

1. UI Layer (Windows Forms):  
   Responsible for the graphical interface and user interactions.

2. Logic Layer:  
   Contains the business logic and application rules.

3. Data Access Layer:  
   Handles database communication using SQL Server and ADO.NET.

------------------------------------------------------------

Technologies Used  
- Programming Language: C# (.NET Framework)  
- UI Framework: Windows Forms  
- Database: Microsoft SQL Server  
- Data Access: ADO.NET  
- PDF Export: iTextSharp Library  
- IDE: Visual Studio  
- Version Control: Git and GitHub

------------------------------------------------------------

Setup Instructions  
1. Clone the repository from GitHub.  
2. Open the solution file (ClinicManagementSystem.sln) in Visual Studio.  
3. Restore NuGet packages if required.  
4. Set up the SQL Server database using the provided script in the Database folder.  
5. Update the connection string in the configuration class (clsDataSettings.cs) with your local database configuration.  
6. Build and run the project.

------------------------------------------------------------

Screenshots  
(Add images or screenshots showing the main interface of the system)
Suggested screenshots include:
- Login screen  
- Dashboard  
- Appointments management  
- Patient profile  
- Prescription management  
- Payment invoice screen  

------------------------------------------------------------

Future Improvements  
- Implement role-based permissions for admin and staff.  
- Add online appointment booking via web API.  
- Enable cloud synchronization for remote access.  
- Add reporting and analytics dashboards.  
- Improve UI design with modern .NET styling (WPF or .NET MAUI in future versions).

------------------------------------------------------------

Author  
Developed by: Mahdi Al-Suleiman  
GitHub Profile: https://github.com/MahdiHyper  
LinkedIn Profile: https://www.linkedin.com/in/mahdi-alsuleiman-702560277/

------------------------------------------------------------

This project represents a complete and production-ready clinic management solution demonstrating practical experience in software architecture, C#, Windows Forms, SQL Server, and system design principles.

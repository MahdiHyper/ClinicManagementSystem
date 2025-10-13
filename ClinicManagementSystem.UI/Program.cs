using ClinicManagementSystem.UI.AppointmentsForms;
using ClinicManagementSystem.UI.DoctorsForms;
using ClinicManagementSystem.UI.MedicationsForms;
using ClinicManagementSystem.UI.PatientsForms;
using ClinicManagementSystem.UI.UserForms;
using ClinicManagmentSystem.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicManagementSystem.UI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }
    }
}

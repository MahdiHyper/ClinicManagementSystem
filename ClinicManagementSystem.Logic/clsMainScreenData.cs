using ClinicManagementSystem.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystem.Logic
{
    public class clsMainScreenData
    {
        public static string GetTotalUsers ()
        {
            DataTable _dt = clsUser.GetAllUsers();

            if (_dt != null && _dt.Rows.Count > 0)
            {
                return Convert.ToString(_dt.Rows.Count);
            }

            return "";
        }

        public static string GetTotalPatient()
        {
            DataTable _dt = clsPatient.GetAllPatients();

            if (_dt != null && _dt.Rows.Count > 0)
            {
                return Convert.ToString(_dt.Rows.Count);

            }
            return "";
        }

        public static string GetTotalDoctors()
        {
            DataTable _dt = clsDoctor.GetAllDoctors();

            if (_dt != null && (_dt.Rows.Count > 0))
            {
                return Convert.ToString(_dt.Rows.Count);
            }

            return "";
        }

        public static string GetTotalTodayAppointments()
        {
            DateTime d1 = DateTime.Today;
            DateTime d2 = d1.AddHours(23).AddMinutes(59);
            DataTable _dt = clsAppointment.GetAppointmentsInRange(d1, d2);

            if (_dt != null && _dt.Rows.Count > 0)
            {
                return Convert.ToString(_dt.Rows.Count);
            }

            return "0";
        }

        public static string GetLastMonthIncome()
        {
            return clsPayment.GetTotalAmountReceivedFormatted();
        }

        public static clsAppointment GetNextScheduleAppointment()
        {
            int AppID = clsAppointment.GetNextScheduleAppointment();

            if (AppID > 0)
            {
                clsAppointment App = clsAppointment.GetAppointmentByID(AppID);
                if (App != null)
                {
                    return App;
                }
            }

            return null;
        }

    }
}

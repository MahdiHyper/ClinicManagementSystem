using ClinicManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystem.Logic
{
    public class clsAppointment
    {
        //(Scheduled =1  , Completed = 2 , Cancelled = 3)
        enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;
        public enum enAppointmentStatus { Scheduled = 1, Completed = 2, Cancelled = 3 }
        public enAppointmentStatus AppointmentStatusName;

        public int AppointmentID { get; private set; }
        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public int Status { get; set; }
        public int PaymentID { get; set; }
        public string Notes { get; set; }
        public string StatusName
        {
            get
            {
                switch (Status)
                {
                    case (int)enAppointmentStatus.Scheduled:
                        return "Scheduled";
                    case (int)enAppointmentStatus.Completed:
                        return "Completed";
                    case (int)enAppointmentStatus.Cancelled:
                        return "Cancelled";
                    default:
                        return "Unknown";
                }
            }
        }

        public clsAppointment()
        {
            this.AppointmentID = -1;
            this.PatientID = -1;
            this.DoctorID = -1;
            this.AppointmentDateTime = DateTime.MinValue;
            this.Status = -1;
            this.PaymentID = -1;
            this.Notes = null;
            _Mode = enMode.AddNew;
        }

        public clsAppointment(int AppointmentID , int PatientID , int DoctorID , DateTime AppointmentDateTime,
           int Status, int PaymentID, string Notes)
        {
            this.AppointmentID = AppointmentID;
            this.PatientID = PatientID;
            this.DoctorID = DoctorID;
            this.AppointmentDateTime = AppointmentDateTime;
            this.Status = Status;
            this.PaymentID = PaymentID;
            this.Notes = Notes;
            _Mode = enMode.Update;
        }

        private bool _AddNew ()
        {
            if (!clsAppointmentsData.IsDoctorFree(this.DoctorID , this.AppointmentDateTime)) return false;
            if (!clsAppointmentsData.IsPatientFree(this.PatientID, this.AppointmentDateTime)) return false;

            int newId = -1;

            newId = clsAppointmentsData.AddNewAppointment(this.DoctorID, this.PatientID,
                this.AppointmentDateTime, this.Status, this.PaymentID, this.Notes);

            if (newId != -1) 
            {
                this.AppointmentID = newId;
                return true;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("ERROR Logic - Appointment (AddNew)");
                return false;
            }
        }
        private bool _Update()
        {
            bool Update = clsAppointmentsData.UpdateAppointment(this.AppointmentID, this.DoctorID,
                this.PatientID, this.AppointmentDateTime,this.Status, this.PaymentID, this.Notes);

            if (Update)
            {
                return true;
            }else
            {
                System.Diagnostics.Debug.WriteLine("ERROR Logic - Appointment (Update)");
                return false;
            }
        }
        public bool Save()
        {
            if (_Mode == enMode.AddNew)
            {
                if (_AddNew())
                {
                    _Mode = enMode.Update;
                    return true;
                }

            } else
            {
                return _Update();
            }

            return false;
        }

        public static bool DeleteAppointmentByID(int AppointmentID)
        {
            return clsAppointmentsData.DeleteAppointment(AppointmentID);
        }
        public bool Delete()
        {
            return DeleteAppointmentByID(this.AppointmentID);
        }

        public static DataTable GetAllAppointments ()
        {
            return clsAppointmentsData.GetAllAppointments();
        }
        public static DataTable GetAppointmentsInRange (DateTime from , DateTime TO)
        {
            return clsAppointmentsData.GetAllAppointmentsInRange(from, TO);
        }
        public static clsAppointment GetAppointmentByID (int AppointmentID)
        {
            int DoctorID = -1;
            int PatientID = -1;
            DateTime AppointmentDateTime = DateTime.Now;
            int Status = -1;
            int PaymentID = -1;
            string Notes = null;

            if (clsAppointmentsData.GetAppointmentByID(AppointmentID,ref DoctorID,
                ref PatientID, ref AppointmentDateTime, ref Status, ref PaymentID, ref Notes))
            {
                return new clsAppointment(AppointmentID,PatientID,DoctorID,AppointmentDateTime,
                    Status, PaymentID, Notes);
            }else
            {
                return null;
            }
        }

        public static bool IsDoctorFree (int DoctorID, DateTime AppointmentDateTime)
        {
            return clsAppointmentsData.IsDoctorFree(DoctorID, AppointmentDateTime);
        }
        public static bool IsPatientFree(int PatientID, DateTime AppointmentDateTime)
        {
            return clsAppointmentsData.IsPatientFree(PatientID, AppointmentDateTime);
        }

        public static int GetNextScheduleAppointment ()
        {
            return clsAppointmentsData.GetNextScheduleAppointment();
        }
    }
}

using System;
using System.Data;
using ClinicManagementSystem.Data;

namespace ClinicManagementSystem.Logic
{
    public class clsMedicalRecord
    {
        public int MedicalRecordID { get; private set; }
        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public string Diagnosis { get; set; }
        public string Notes { get; set; }
        public DateTime DiagnosisDate { get; set; }

        public bool IsNew => MedicalRecordID <= 0;

        public clsMedicalRecord()
        {
            MedicalRecordID = -1;
            DiagnosisDate = DateTime.Now;
        }

        public bool Save()
        {
            if (IsNew)
            {
                MedicalRecordID = clsMedicalRecordData.AddNewMedicalRecord(AppointmentID, Diagnosis, DiagnosisDate, Notes, PatientID);
                return MedicalRecordID != -1;
            }
            else
            {
                return clsMedicalRecordData.UpdateMedicalRecord(MedicalRecordID, AppointmentID, Diagnosis, DiagnosisDate, Notes, PatientID);
            }
        }

        public static clsMedicalRecord FindByAppID(int appID)
        {
            int id = 0, patientID = 0;
            string diagnosis = "", notes = "";
            DateTime date = DateTime.Now;

            if (clsMedicalRecordData.GetMedicalRecordByAppID(ref id, appID, ref diagnosis, ref date, ref notes, ref patientID))
            {
                return new clsMedicalRecord
                {
                    MedicalRecordID = id,
                    AppointmentID = appID,
                    PatientID = patientID,
                    Diagnosis = diagnosis,
                    Notes = notes,
                    DiagnosisDate = date
                };
            }

            return null;
        }

        public static clsMedicalRecord FindByID(int MedicalRecordID)
        {
            int AppID = 0, patientID = 0;
            string diagnosis = "", notes = "";
            DateTime date = DateTime.Now;

            if (clsMedicalRecordData.GetMedicalRecordByID(MedicalRecordID, ref AppID, ref diagnosis, ref date, ref notes, ref patientID))
            {
                return new clsMedicalRecord
                {
                    MedicalRecordID = MedicalRecordID,
                    AppointmentID = AppID,
                    PatientID = patientID,
                    Diagnosis = diagnosis,
                    Notes = notes,
                    DiagnosisDate = date
                };
            }

            return null;
        }

        public static bool IsApplinkedWithMedicalRecored(int appointmentID)
        {
            return clsMedicalRecordData.ExistsForAppointment(appointmentID);
        }

        public static DataTable GetAllMedicalRecordsForPatient(int PatinetID)
        {
            return clsMedicalRecordData.GetAllMedicalRecordsForPatient(PatinetID);
        }
    }
}

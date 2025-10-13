using ClinicManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystem.Logic
{
    public class clsPrescription
    {
        public bool IsNew => PrescriptionID < 1;
        public int PrescriptionID { get; private set; }
        public int MedicalRecordID { get; set; }
        public DateTime IssueDate { get; set; }
        public string Notes { get; set; }
        public int PatientID { get; set; }

        public clsPrescription()
        {
            PrescriptionID = -1;
            MedicalRecordID = -1;
            IssueDate = DateTime.Now;
            Notes = string.Empty;
            PatientID = -1;
        }

        public clsPrescription(int PrescriptionID , int MedicalRecordID, DateTime IssueDate,
            string Notes , int PatientID)
        {
            this.PrescriptionID = PrescriptionID;
            this.MedicalRecordID = MedicalRecordID;
            this.IssueDate = IssueDate;
            this.Notes = Notes;
            this.PatientID = PatientID;
        }

        public bool Save()
        {
            try
            {
                if (IsNew)
                {
                    this.PrescriptionID = clsPrescriptionData.AddNewPrescription(this.MedicalRecordID, this.IssueDate, this.PatientID, this.Notes);
                    return this.PrescriptionID > -1;
                }
                else
                {
                    return clsPrescriptionData.UpdatePrescription(this.PrescriptionID, this.MedicalRecordID, this.IssueDate, this.PatientID, this.Notes);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Logic Error While Saving: {ex}");
                return false;
            }
        }

        public static bool DeletePrescription(int PrescriptionID)
        {
            return clsPrescriptionData.DeletePrescription(PrescriptionID);
        }

        public static clsPrescription FindByID(int PrescriptionID)
        {
            int MedicalRecordID = -1;
            DateTime IssueDate = DateTime.Now;
            string Notes = "";
            int PatientID = -1;

            if (clsPrescriptionData.FindByID(PrescriptionID,ref MedicalRecordID, ref IssueDate,ref PatientID,ref Notes))
            {
                return new clsPrescription
                {
                    MedicalRecordID = MedicalRecordID,
                    IssueDate = IssueDate,
                    Notes = Notes,
                    PrescriptionID = PrescriptionID,
                    PatientID = PatientID,
                };
            }
            return null;

        }

        public static clsPrescription FindByMedicalRecordID (int MedicalRecordID)
        {
            int PrescriptionID = -1;
            DateTime IssueDate = DateTime.Now;
            string Notes = "";
            int PatientID = -1;

            if (clsPrescriptionData.FindByMedicalRecordID(MedicalRecordID , ref PrescriptionID,
                ref IssueDate , ref PatientID, ref Notes))
            {
                return new clsPrescription()
                {
                    PrescriptionID = PrescriptionID,
                    MedicalRecordID = MedicalRecordID,
                    IssueDate = IssueDate,
                    Notes = Notes,
                    PatientID = PatientID
                };
            }

            return null;
        }

        public static bool IsLinkedWithMedicalRecord (int MedicalRecordId)
        {
            return clsPrescriptionData.IsLinkedWithMedicalRecord(MedicalRecordId);
        }

        public static DataTable GetMedicationsByPrescriptionID(int PrescriptionID)
        {
            return clsPrescriptionData.GetMedicationsByPrescriptionID(PrescriptionID);
        }

        public static DataTable GetPrescriptionsByPatientID(int patientID)
        {
            return clsPrescriptionData.GetPrescriptionsByPatientID(patientID);
        }

        public static int GetNumberOfPrescriptionItems (int PrescriptionID)
        {
            DataTable dt = clsPrescriptionData.GetMedicationsByPrescriptionID (PrescriptionID);
            return dt.Rows.Count;
        }

        public int GetNumberOfPrescriptionItems()
        {
            return GetNumberOfPrescriptionItems(this.PrescriptionID);
        }
    }
}

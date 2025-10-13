using System;
using System.Data;
using ClinicManagementSystem.Data;
using ClinicManagementSystem.Logic;

namespace ClinicManagementSystem.Business
{
    public class clsPatient
    {
        public enum enMode { AddNew = 0, Update = 1 }

        public int PatientID { get; set; }
        public int PersonID { get; set; }
        public int BloodTypeID { get; set; }
        public string Notes { get; set; }
        public enMode Mode { get; set; }
        public clsPerson PersonInfo { get; set; }

        public clsPatient()
        {
            this.PatientID = -1;
            this.PersonID = -1;
            this.BloodTypeID = -1;
            this.Notes = "";
            this.Mode = enMode.AddNew;
            this.PersonInfo = null;
        }
        private clsPatient(int PatientID, int PersonID, int BloodTypeID, string Notes)
        {
            this.PatientID = PatientID;
            this.PersonID = PersonID;
            this.BloodTypeID = BloodTypeID;
            this.Notes = Notes;
            this.Mode = enMode.Update;

            this.PersonInfo = clsPerson.GetPersonInfo(PersonID);
        }

        public static clsPatient GetPatientByID(int PatientID)
        {
            int PersonID = -1;
            int BloodTypeID = -1;
            string Notes = "";

            bool IsFound = clsPatientData.GetPatientByID(PatientID, ref PersonID, ref BloodTypeID, ref Notes);

            if (IsFound)
            {
                return new clsPatient(PatientID, PersonID, BloodTypeID, Notes);
            }
            else
            {
                return null;
            }
        }
        public static DataTable GetAllPatients()
        {
            return clsPatientData.GetAllPatients();
        }
        public static bool DeletePatient(int PatientID)
        {
            clsPatient patient = clsPatient.GetPatientByID(PatientID);

            if (patient != null)
            {
                int personID = patient.PersonID;
                bool patientDeleted = clsPatientData.DeletePatient(PatientID);

                if (patientDeleted)
                {
                    return clsPerson.Delete(personID);
                }
            }

            return false;
        }
        public bool Delete()
        {
            return (DeletePatient(this.PatientID));
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    return _AddNewPatient();

                case enMode.Update:
                    return _UpdatePatient();

                default:
                    return false;
            }
        }
        private bool _AddNewPatient()
        {
            if (PersonInfo != null)
            {
                int personID = clsPersonData.AddNewPerson(
                    this.PersonInfo.FirstName,
                    this.PersonInfo.SecondName,
                    this.PersonInfo.LastName,
                    this.PersonInfo.DateOfBirth,
                    this.PersonInfo.PhoneNumber,
                    this.PersonInfo.Email,
                    this.PersonInfo.Gender);

                if (personID == -1)
                {
                    System.Diagnostics.Debug.WriteLine("Logic - Patient: Failed to add new Person!");
                    return false;
                }
                this.PersonID = personID;
                this.PersonInfo.PersonID = personID;
            }
            this.PatientID = clsPatientData.AddNewPatient(this.PersonID, this.BloodTypeID, this.Notes);

            if (this.PatientID != -1)
            {
                this.Mode = enMode.Update;
                return true;
            }

            System.Diagnostics.Debug.WriteLine("Logic - Patient: Failed to add new Patient!");
            return false;
        }
        private bool _UpdatePatient()
        {
            bool isUpdatedPerson = clsPersonData.UpdatePerson(
                this.PersonID,
                this.PersonInfo.FirstName,
                this.PersonInfo.SecondName,
                this.PersonInfo.LastName,
                this.PersonInfo.DateOfBirth,
                this.PersonInfo.PhoneNumber,
                this.PersonInfo.Email,
                this.PersonInfo.Gender);

            bool isUpdatedPatient = clsPatientData.UpdatePatient(this.PatientID, this.BloodTypeID, this.Notes);

            if (!isUpdatedPatient)
            {
                System.Diagnostics.Debug.WriteLine("Logic - Patient (Update Patient) : Error while updating Patient!");
            }
            if (!isUpdatedPerson)
            {
                System.Diagnostics.Debug.WriteLine("Logic - Patient (Update Person) : Error while updating Patient!");
            }

            return (isUpdatedPatient && isUpdatedPerson);
        }
        
        public static DataTable GetAllBloodTypes ()
        {
            return clsPatientData.GetAllBloodTypes();
        }
    }
}
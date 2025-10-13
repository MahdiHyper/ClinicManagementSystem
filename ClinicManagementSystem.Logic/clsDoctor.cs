using ClinicManagementSystem.Data;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystem.Logic
{
    public class clsDoctor
    {
        enum enMode { Addnew = 0, Update = 1 }
        enMode _Mode = enMode.Addnew;

        public int DoctorID { get; set; }
        public int PersonID { get; set; }
        public int SpecializationID { get; set; }
        public double ConsultationFee { get; set; }
        public clsPerson PersonInfo { get; set; }

        public clsDoctor()
        {
            _Mode = enMode.Addnew;

            DoctorID = -1;
            PersonID = -1;
            SpecializationID = -1;
            ConsultationFee = -1;
            PersonInfo = new clsPerson();
        }

        public clsDoctor(int DoctorID, int PersonID, int Specialization, double ConsultationFee)
        {
            _Mode = enMode.Update;

            this.DoctorID = DoctorID;
            this.PersonID = PersonID;
            this.SpecializationID = Specialization;
            this.ConsultationFee = ConsultationFee;

            PersonInfo = clsPerson.GetPersonInfo(PersonID);
        }

        private bool _AddNew()
        {
            if (this.PersonInfo != null)
            {
                int PersonID = clsPersonData.AddNewPerson(PersonInfo.FirstName,
                    PersonInfo.SecondName, PersonInfo.LastName, PersonInfo.DateOfBirth,
                    PersonInfo.PhoneNumber, PersonInfo.Email, PersonInfo.Gender);

                if (PersonID == -1)
                {
                    System.Diagnostics.Debug.WriteLine("Logic - Doctors: Failed to add new Person in Doctor!");
                    return false;
                }

                this.PersonID = PersonID;
                this.PersonInfo.PersonID = PersonID;

                this.DoctorID = clsDoctorData.AddNewDoctor(this.PersonID, this.SpecializationID
                    , this.ConsultationFee);
                if (this.DoctorID != -1)
                {
                    this._Mode = enMode.Update;
                    return true;
                }
            }

            System.Diagnostics.Debug.WriteLine("Logic - Doctors: Failed to add new Doctor!");
            return false;
        }
        private bool _Update()
        {
            bool IsPersonUpdated = clsPersonData.UpdatePerson(
                this.PersonID,
                this.PersonInfo.FirstName,
                this.PersonInfo.SecondName,
                this.PersonInfo.LastName,
                this.PersonInfo.DateOfBirth,
                this.PersonInfo.PhoneNumber,
                this.PersonInfo.Email,
                this.PersonInfo.Gender);

            bool IsDoctorUpdated = clsDoctorData.UpdateDoctor(
                this.DoctorID,
                this.SpecializationID,
                this.ConsultationFee);


            return (IsPersonUpdated && IsDoctorUpdated);
        }
        public bool Save()
        {
            switch (this._Mode)
            {
                case enMode.Update:
                        return _Update();

                case enMode.Addnew:
                        return _AddNew();


                default:
                    return false;
            }
        }
        public bool Delete()
        {
            var doctor = clsDoctor.GetDoctorByID(this.DoctorID);
            if (doctor == null) return false;

            if (!clsDoctorData.DeleteDoctor(this.DoctorID))
                return false;

            return clsPerson.Delete(doctor.PersonID);
        }

        public static bool DeleteByID(int doctorID)
        {
            var doctor = clsDoctor.GetDoctorByID(doctorID);
            if (doctor == null) return false;

            return doctor.Delete();
        }

        public static DataTable GetAllDoctors ()
        {
          return clsDoctorData.GetAllDoctors();
        }
        public static clsDoctor GetDoctorByID(int DoctorID)
        {
            int PersonID = -1;
            int Specialization = -1;
            double ConsultationFee = -1;

            bool isFound = clsDoctorData.GetDoctorByID(DoctorID, ref PersonID , 
                ref  Specialization , ref  ConsultationFee);

            if (isFound)
            {
                return new clsDoctor(DoctorID, PersonID, Specialization, ConsultationFee);
            }else
            {
                return null;
            }
        }
        public static int Count()
        {
            return clsDoctorData.GetAllDoctors().Rows.Count;
        }
        public static System.Collections.Generic.Dictionary<int, string> GetAllSpecializations()
             => clsDoctorData.GetAllSpecializations();

    }
}

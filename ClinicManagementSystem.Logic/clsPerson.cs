using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ClinicManagementSystem.Data;

namespace ClinicManagementSystem.Logic
{
    public class clsPerson
    {
        public enum enMode { Addnew = 0, Update = 1 }
        public enMode _Mode;

        public int PersonID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string SecondName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public int Gender { get; set; }

        public string GenderName
        {
            get
            {
               return Gender == 1 ? "Male" : "Female";
            }
        }

        public string FullName
        {
            get
            {
                return $"{FirstName} {SecondName} {LastName}".Trim();
            }
        }

        public int Age
        {
            get
            {
                if (this != null)
                {
                    int age = DateTime.Now.Year - this.DateOfBirth.Year;
                    if (DateTime.Now.DayOfYear < this.DateOfBirth.DayOfYear)
                        age--;
                    return age;
                }
                return 0;
            }
        }

        public clsPerson()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.MinValue;
            this.Email = "";
            this.Gender = -1;
            this.PhoneNumber = "";

            _Mode = enMode.Addnew;
        }

        public clsPerson(int PersonID, string FirstName, string SecondName, string LastName
            , string Email, string PhoneNumber, DateTime DateOfBirth, int Gender)
        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.SecondName = SecondName;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.Gender = Gender;

            _Mode = enMode.Update;
        }

        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }

        private bool _AddNew()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.FirstName, this.SecondName, this.LastName
                , this.DateOfBirth, this.PhoneNumber, this.Email, this.Gender);

            return this.PersonID != -1;
        }

        private bool _Update()
        {
            return clsPersonData.UpdatePerson(this.PersonID, this.FirstName, this.SecondName
                , this.LastName, this.DateOfBirth, this.PhoneNumber, this.Email, this.Gender);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.Addnew:
                    if (_AddNew())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    {
                        return _Update();
                    }


            }
            return false;

        }

        public static bool Delete(int PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }

        public static clsPerson GetPersonInfo(int PersonID)
        {
            string FirstName = "";
            string SecondName = "";
            string LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            string Email = "";
            string PhoneNumber = "";
            short Gender = -1;

            bool IsFound = clsPersonData.GetPersonInfoByID(PersonID, ref FirstName, ref SecondName, ref LastName
                , ref DateOfBirth, ref PhoneNumber, ref Email, ref Gender);

            return IsFound ? new clsPerson(PersonID, FirstName, SecondName, LastName, Email
                    , PhoneNumber, DateOfBirth, Gender) : null;

        }

        public static bool IsEmailTaken(string email)
        {
            return clsPersonData.IsEmailExists(email);
        }

    }
}

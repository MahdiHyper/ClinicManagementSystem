using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ClinicManagementSystem.Data;

namespace ClinicManagementSystem.Logic
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode _Mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public clsPerson PersonInfo { get; set; }

        public clsUser() 
        {
            this.UserID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = false;
            this.PersonID = -1;
            this.PersonInfo = new clsPerson();

            _Mode = enMode.AddNew;
        }
        public clsUser (int UserID , int PersonID, string UserName, bool IsActive)
        {
            this.UserID = UserID;
            this.UserName = UserName;
            this.PersonID = PersonID;
            this.IsActive = IsActive;
            this.PersonInfo = clsPerson.GetPersonInfo(PersonID);

            _Mode = enMode.Update;
        }

        private bool _AddNew()
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
                System.Diagnostics.Debug.WriteLine("Logic - Users : Failed to add new User!");
                return false;
            }

            this.PersonID = personID;

            string hashedPassword = clsHelper.ComputeHash(this.Password);

            int userID = clsUserData.AddNewUser(
                this.PersonID,
                this.UserName,
                hashedPassword,
                this.IsActive);


            if (userID == -1)
            {
                System.Diagnostics.Debug.WriteLine("Failed to add new user.");
                return false;
            }

            this.UserID = userID;

            System.Diagnostics.Debug.WriteLine("_AddNew completed successfully.");
            return true;
        }
        private bool _Update()
        {
            bool isUpdatedPerson = clsPersonData.UpdatePerson(this.PersonID, this.PersonInfo.FirstName, this.PersonInfo.SecondName, this.PersonInfo.LastName,
                this.PersonInfo.DateOfBirth, this.PersonInfo.PhoneNumber, this.PersonInfo.Email, this.PersonInfo.Gender);

            bool isUpdatedUser =  clsUserData.UpdateUser(this.UserID, this.UserName, this.IsActive);

            if (!isUpdatedUser)
            {
                System.Diagnostics.Debug.WriteLine("Logic - Users : Error while updating User!");
            }

            return (isUpdatedUser && isUpdatedPerson);
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNew())
                        {
                            _Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case enMode.Update:
                    {
                        return _Update();
                    }
            }
            return false;
        }
        public static bool DeleteUser(int UserID)
        {
            clsUser _User = clsUser.FindUserByID(UserID);
           
            if (clsUserData.DeleteUser(UserID)) {

                return clsPerson.Delete(_User.PersonID) ;
           
            }

            return false;
        }
        public static clsUser FindUserByID(int UserID)
        {
            bool isFound = false;

            int PersonID = -1;
            string UserName = "";
            bool IsActive = false;

            isFound = clsUserData.FindUserByUserID(UserID, ref PersonID, ref UserName, ref IsActive);

            if (isFound)
            {
                return new clsUser(UserID, PersonID, UserName, IsActive);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Faild to Find User with UserID = " + UserID);
                return null; 
            }  
        }
        public static clsUser FindUserByUsername (string Username)
        {
            bool isFound = false;

            int UserID = -1;
            int PersonID = -1;
            bool IsActive = false;

            isFound = clsUserData.FindUserByUserName(Username, ref UserID, ref PersonID, ref IsActive);

                if (isFound)
                {
                    return new clsUser(UserID, PersonID, Username, IsActive);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Logic - Users (FindUserByUsername)" );            
                    return null;
                }
        }
        public static DataTable GetAllUsers ()
        {
            return clsUserData.GetAllUsers();
        }
        public static bool Login(string UserName, string Password)
        {            
           
            return clsUserData.LoginByUserNameAndPassword(UserName, Password);

        }
        public static bool IsUserNameTaken(string UserName)
        {
            return clsUserData.IsUserNameTaken(UserName);
        }
        public static bool IsUserActive(int UserID)
        {
            return clsUserData.IsUserActive(UserID);
        }

        public bool UpdatePassword(string NewPassword)
        {
            string HashedPassword = clsHelper.ComputeHash(NewPassword);

            return clsUserData.UpdatePassword(this.UserID, HashedPassword);
        }

    }
}

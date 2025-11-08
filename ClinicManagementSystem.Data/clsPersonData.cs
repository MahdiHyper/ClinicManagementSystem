using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Data.SqlClient;

namespace ClinicManagementSystem.Data
{
    public class clsPersonData
    {
        public static string Connection = clsDataSettings.ConnectionString;

        public static DataTable GetAllPeople()
        {
            DataTable dtAllPerson = new DataTable();

            string query = "SELECT * FROM People;";

            using (SqlConnection connection = new SqlConnection(Connection))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                            dtAllPerson.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR Database - Person (GetAllPeople) " + ex.Message);
                }
            }

            return dtAllPerson;
        }

        public static int AddNewPerson(string FirstName, string SecondName,
    string LastName, DateTime DateOfBirth, string PhoneNumber, string Email, int Gender)
        {
            int PersonID = -1;

            string query = @"INSERT INTO People (FirstName, SecondName, LastName, DateOfBirth, PhoneNumber, Email, Gender)
VALUES (@FirstName, @SecondName, @LastName, @DateOfBirth, @PhoneNumber, @Email, @Gender);
                     SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(Connection))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FirstName", FirstName);

                if (!string.IsNullOrWhiteSpace(SecondName))
                    command.Parameters.AddWithValue("@SecondName", SecondName);
                else
                    command.Parameters.AddWithValue("@SecondName", DBNull.Value);

                command.Parameters.AddWithValue("@LastName", LastName);
                command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@Gender", Gender);


                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        PersonID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Database - Person : " + ex.Message);
                }
            }

            return PersonID;
        }

        public static bool UpdatePerson(int PersonID, string FirstName, string SecondName,
    string LastName, DateTime DateOfBirth, string PhoneNumber, string Email, int Gender)
        {
            int rowsAffected = 0;

            string Query = @"UPDATE People 
                     SET 
                        FirstName = @FirstName, 
                        SecondName = @SecondName,
                        LastName = @LastName, 
                        DateOfBirth = @DateOfBirth,
                        PhoneNumber = @PhoneNumber, 
                        Email = @Email,
                        Gender = @Gender 
                     WHERE PersonID = @PersonID;";

            using (SqlConnection connection = new SqlConnection(Connection))
            using (SqlCommand command = new SqlCommand(Query, connection))
            {
                command.Parameters.AddWithValue("@PersonID", PersonID);
                command.Parameters.AddWithValue("@FirstName", FirstName);

                if (!string.IsNullOrWhiteSpace(SecondName))
                    command.Parameters.AddWithValue("@SecondName", SecondName);
                else
                    command.Parameters.AddWithValue("@SecondName", DBNull.Value);

                command.Parameters.AddWithValue("@LastName", LastName);
                command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@Gender", Gender);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR Database - Person (Update) " + ex.Message);

                    return false;
                }

                return rowsAffected > 0;
            }
        }

        public static bool DeletePerson (int PersonID)
        {
            int rowsAffected = 0;

            string query = @"DELETE FROM People
                          WHERE PersonID = @PersonID";

            using (SqlConnection connection = new SqlConnection(Connection))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@PersonID", PersonID);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR Database - Person (Delete) " + ex.Message);

                    return false;
                }

            }
            return rowsAffected > 0;
        }

        public static bool GetPersonInfoByID(int PersonID, ref string FirstName,
    ref string SecondName, ref string LastName, ref DateTime DateOfBirth,
    ref string PhoneNumber, ref string Email, ref short Gender)
        {
            bool IsFound = false;

            string Query = @"SELECT * FROM People WHERE PersonID = @PersonID;";

            using (SqlConnection connect = new SqlConnection(Connection))
            using (SqlCommand command = new SqlCommand(Query, connect))
            {
                command.Parameters.AddWithValue("@PersonID", PersonID);

                try
                {
                    connect.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            IsFound = true;

                            FirstName = Convert.ToString(reader["FirstName"]);
                            SecondName = reader["SecondName"] != DBNull.Value ? Convert.ToString(reader["SecondName"]) : "";
                            LastName = Convert.ToString(reader["LastName"]);
                            DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                            PhoneNumber = Convert.ToString(reader["PhoneNumber"]);
                            Email = Convert.ToString(reader["Email"]);
                            Gender = Convert.ToInt16(reader["Gender"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR Database - Person (GetPersonInfoByID) " + ex.Message);

                    return false;
                }
            }

            return IsFound;
        }


        public static bool IsEmailExists(string email)
        {
            using (var con = new SqlConnection(Connection))
            using (var cmd = new SqlCommand(
                "SELECT 1 FROM dbo.People WHERE Email = @Email", con))
            {
                try
                {
                    con.Open();
                    var obj = cmd.ExecuteScalar();
                    return obj != null;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR Database - Person (IsEmailExists) " + ex.Message);
                    return false;
                }

            }
        }

    }
}

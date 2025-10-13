using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ClinicManagementSystem.Data
{
    public static class clsPatientData
    {
        private static string ConnectionString = clsDataSettings.ConnectionString;

        public static int AddNewPatient(int PersonID, int BloodTypeID, string Notes)
        {
            int PatientID = -1;

            string Query = @"INSERT INTO Patients (PersonID, BloodTypeID, Notes)
                            VALUES (@PersonID, @BloodTypeID, @Notes);
                            SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, connection))
            {
                cmd.Parameters.AddWithValue("@PersonID", PersonID);
                cmd.Parameters.AddWithValue("@BloodTypeID", BloodTypeID);

                if (!string.IsNullOrWhiteSpace(Notes))
                {
                    cmd.Parameters.AddWithValue("@Notes", Notes);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
                }

                try
                {
                    connection.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        PatientID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Database Error - Patient (AddNew): {ex.Message}");
                }
            }

            return PatientID;
        }

        public static bool UpdatePatient(int PatientID, int BloodTypeID, string Notes)
        {
            int rowsAffected = 0;

            string Query = @"UPDATE Patients
                            SET BloodTypeID = @BloodTypeID,
                                Notes = @Notes
                            WHERE PatientID = @PatientID;";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, connection))
            {
                cmd.Parameters.AddWithValue("@BloodTypeID", BloodTypeID);

                if (!string.IsNullOrWhiteSpace(Notes))
                {
                    cmd.Parameters.AddWithValue("@Notes", Notes);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
                }

                cmd.Parameters.AddWithValue("@PatientID", PatientID);

                try
                {
                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Database Error - Patient (Update): {ex.Message}");
                    return false;
                }
            }
            return (rowsAffected > 0);
        }

        public static bool DeletePatient(int PatientID)
        {
            int rowsAffected = 0;

            string Query = @"DELETE FROM Patients WHERE PatientID = @PatientID;";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, connection))
            {
                cmd.Parameters.AddWithValue("@PatientID", PatientID);

                try
                {
                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Database Error - Patient (Delete): {ex.Message}");
                    return false;
                }
            }
            return rowsAffected > 0;
        }

        public static DataTable GetAllPatients()
        {
            DataTable dtAllPatients = new DataTable();

            string Query = @"SELECT PatientID, FirstName, LastName,
                            BloodTypeName, DateOfBirth, PhoneNumber,
                            Email, 
                            CASE 
                                WHEN Gender = 1 THEN 'Male' 
                                ELSE 'Female' 
                            END AS Gender,
                            Notes
                            FROM Patients
                            INNER JOIN People ON Patients.PersonID = People.PersonID
                            INNER JOIN BloodTypes ON Patients.BloodTypeID = BloodTypes.BloodTypeID;";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, connection))
            {
                try
                {
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dtAllPatients.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Database Error - Patient (GetAll): {ex.Message}");
                }
            }
            return dtAllPatients;
        }

        public static bool GetPatientByID(int PatientID, ref int PersonID, ref int BloodTypeID, ref string Notes)
        {
            bool found = false;
          
            string Query = @"SELECT PersonID, BloodTypeID, Notes 
                            FROM Patients
                            WHERE PatientID = @PatientID;";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, connection))
            {
                cmd.Parameters.AddWithValue("@PatientID", PatientID);

                try
                {
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            found = true;
                            PersonID = (int)reader["PersonID"];
                            BloodTypeID = (int)reader["BloodTypeID"];

                            Notes = reader["Notes"] == DBNull.Value ? "" : reader["Notes"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Database Error - Patient (GetByID): {ex.Message}");
                    return false;
                }
            }

            return found;
        }

        public static DataTable GetAllBloodTypes()
        {
            DataTable dt = new DataTable();

            string Query = @"SELECT BloodTypeID, BloodTypeName 
                                FROM BloodTypes 
                                ORDER BY BloodTypeID;";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, connection))
            {
                connection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                     dt.Load(reader);
                }
            }
            return dt;
        }


    }
}
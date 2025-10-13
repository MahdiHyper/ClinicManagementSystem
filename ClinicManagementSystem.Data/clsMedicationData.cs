using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ClinicManagementSystem.Data
{
    public class clsMedicationData
    {
        private static string ConnectionString = clsDataSettings.ConnectionString;

        public static int AddNewMedication (string MedicationName, string MedicationSerialNumber ,string Description)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                string query = @"
                INSERT INTO Medications (MedicationName, MedicationSerialNumber, Description)
                VALUES (@MedicationName, @MedicationSerialNumber, @MedicationDescription);
                SELECT CAST(SCOPE_IDENTITY() AS INT);"; 
                
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@MedicationName", MedicationName);
                    
                    if (!string.IsNullOrWhiteSpace(MedicationSerialNumber))
                    {
                        cmd.Parameters.AddWithValue("@MedicationSerialNumber", MedicationSerialNumber);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@MedicationSerialNumber", DBNull.Value);
                    }

                    if (!string.IsNullOrWhiteSpace(Description))
                    {
                        cmd.Parameters.AddWithValue("@MedicationDescription", Description);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@MedicationDescription", DBNull.Value);
                    }

                    try 
                    {
                        con.Open();
                        int newId = (int)cmd.ExecuteScalar();
                        return newId;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Database Error - Medication (AddNew): {ex.Message}");
                        return -1;
                    }
                   
                }
            }
            
        }

        public static bool DeleteMedication(int MedicationID)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                string query = "DELETE FROM Medications WHERE MedicationID = @MedicationID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@MedicationID", MedicationID);
                    try
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Database Error - Medication (Delete): {ex.Message}");
                    }
                }
            }
            return false;
        }

        public static DataTable GetAllMedications()
        {
            DataTable dtMedications = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                string query = "SELECT MedicationID, MedicationName, MedicationSerialNumber, Description FROM Medications ORDER BY MedicationID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dtMedications);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Database Error - Medication (GetAll): {ex.Message}");
                    }
                }
            }
            return dtMedications;
        }

        public static bool GetMedicationByID (int MedicationID, ref string MedicationName ,
            ref string MedicationSerialNumber, ref string MedicationDescription)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                string query = "SELECT MedicationName, MedicationSerialNumber, Description FROM Medications WHERE MedicationID = @MedicationID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@MedicationID", MedicationID);
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                MedicationName = reader["MedicationName"]?.ToString();
                                MedicationSerialNumber = reader["MedicationSerialNumber"] != DBNull.Value ? reader["MedicationSerialNumber"].ToString() : string.Empty;
                                MedicationDescription = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : string.Empty;
                                return true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Database Error - Medication (GetByID): {ex.Message}");
                    }
                }
            }
            return false;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystem.Data
{
    
    public class clsPrescriptionData
    {
        public static int AddNewPrescription(int MedicalRecordID, DateTime IssueDate,
           int PatientID, string Notes)
        {
            int PrescriptionID = -1;

            string Qyery = @"INSERT INTO Prescriptions (MedicalRecordID , IssueDate,
                            Notes , PatientID)
                            VALUES 
                            (@MedicalRecordID , @IssueDate,
                            @Notes , @PatientID)
                            SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = new SqlConnection(clsDataSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Qyery, conn))
            {
                
                cmd.Parameters.AddWithValue("@MedicalRecordID", MedicalRecordID);
                cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
                cmd.Parameters.AddWithValue("@PatientID", PatientID);
                if (!string.IsNullOrEmpty(Notes))
                {
                    cmd.Parameters.AddWithValue("@Notes", Notes);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Notes", DBNull.Value);

                }

                try
                {
                    conn.Open();

                    var result = cmd.ExecuteScalar();
                    if (result != null)
                        PrescriptionID = Convert.ToInt32(result);

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error: {ex.ToString()}");
                }
            }
            return PrescriptionID;
        }

        public static bool UpdatePrescription(int Id, int MedicalRecordID, DateTime IssueDate,
   int PatientID, string Notes)
        {
            int RowEffected = 0;

            string Query = @"UPDATE Prescriptions
                        SET
                        MedicalRecordID = @MedicalRecordID,
                        IssueDate = @IssueDate,
                        Notes = @Notes,
                        PatientID = @PatientID
                        WHERE PrescriptionID = @PrescriptionID;";

            using (SqlConnection conn = new SqlConnection(clsDataSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                cmd.Parameters.AddWithValue("@MedicalRecordID", MedicalRecordID);
                cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
                cmd.Parameters.AddWithValue("@PatientID", PatientID);
                cmd.Parameters.AddWithValue("@PrescriptionID", Id);

                if (!string.IsNullOrEmpty(Notes))
                    cmd.Parameters.AddWithValue("@Notes", Notes);
                else
                    cmd.Parameters.AddWithValue("@Notes", DBNull.Value);

                try
                {
                    conn.Open();
                    RowEffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error: {ex}");
                    return false;
                }
            }

            return RowEffected > 0;
        }


        public static bool DeletePrescription(int Id)
        {
            int RowEffected = 0;

            string Query = @"DELETE FROM Prescriptions WHERE PrescriptionID = @PrescriptionID";

            using (SqlConnection conn = new SqlConnection(clsDataSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                cmd.Parameters.AddWithValue("@PrescriptionID", Id);

                try
                {
                    conn.Open();
                    RowEffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error: {ex.ToString()}");
                    return false;
                }
            }
            return RowEffected > 0;
        }

        public static bool FindByID(int Id, ref int MedicalRecordID, ref DateTime IssueDate,
          ref int PatientID, ref string Notes)
        {
            string Query = @"SELECT MedicalRecordID,
                        IssueDate , Notes , PatientID
                        FROM Prescriptions WHERE PrescriptionID = @PrescriptionID;";

            using (SqlConnection conn = new SqlConnection(clsDataSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                cmd.Parameters.AddWithValue("@PrescriptionID", Id);


                try
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            MedicalRecordID = reader.GetInt32(reader.GetOrdinal("MedicalRecordID"));
                            IssueDate = reader.GetDateTime(reader.GetOrdinal("IssueDate"));
                            PatientID = reader.GetInt32(reader.GetOrdinal("PatientID"));
                            Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : "";
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error: {ex.ToString()}");
                    return false;
                }
            }
            return false;
        }

        public static bool FindByMedicalRecordID(int MedicalRecordID,ref int PrescriptionID, ref DateTime IssueDate,
          ref int PatientID, ref string Notes)
        {
            string Query = @"SELECT PrescriptionID,
                        IssueDate , Notes , PatientID
                        FROM Prescriptions WHERE MedicalRecordID = @MedicalRecordID;";

            using (SqlConnection conn = new SqlConnection(clsDataSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                cmd.Parameters.AddWithValue("@MedicalRecordID", MedicalRecordID);


                try
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PrescriptionID = reader.GetInt32(reader.GetOrdinal("PrescriptionID"));
                            IssueDate = reader.GetDateTime(reader.GetOrdinal("IssueDate"));
                            PatientID = reader.GetInt32(reader.GetOrdinal("PatientID"));
                            Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : "";
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error: {ex.ToString()}");
                    return false;
                }
            }
            return false;
        }

        public static bool IsLinkedWithMedicalRecord(int MedicalRecordID)
        {
            string Query = @"SELECT 1 FROM Prescriptions WHERE MedicalRecordID = @MedicalRecordID;";

            using (SqlConnection conn = new SqlConnection(clsDataSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                cmd.Parameters.AddWithValue("@MedicalRecordID", MedicalRecordID);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error: {ex.ToString()}");
                }
            }
            return false;
        }

        public static DataTable GetMedicationsByPrescriptionID(int prescriptionID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetPrescriptionMedications", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PrescriptionID", prescriptionID);

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        public static DataTable GetPrescriptionsByPatientID(int patientID)
        {
            DataTable dt = new DataTable();

            string Query = @"SELECT 
                        PrescriptionID,
                        MedicalRecordID,
                        IssueDate,
                        Notes,
                        PatientID
                     FROM Prescriptions
                     WHERE PatientID = @PatientID
                     ORDER BY IssueDate DESC;";

            using (SqlConnection conn = new SqlConnection(clsDataSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                cmd.Parameters.AddWithValue("@PatientID", patientID);

                try
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error: {ex}");
                }
            }

            return dt;
        }

        


    }
}

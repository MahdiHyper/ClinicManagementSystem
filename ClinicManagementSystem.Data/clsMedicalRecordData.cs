using System;
using System.Data;
using System.Data.SqlClient;

namespace ClinicManagementSystem.Data
{
    public class clsMedicalRecordData
    {
        public static int AddNewMedicalRecord(int AppID, string Diagnosis, DateTime Date, string Notes, int PatientID)
        {
            int Id = -1;
            string Query = @"INSERT INTO MedicalRecords
                            (AppointmentID, Diagnosis, DiagnosisDate, Notes, PatientID)
                            VALUES (@AppID, @Diagnosis, @Date, @Notes, @PatientID);
                            SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = new SqlConnection(clsDataSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                cmd.Parameters.AddWithValue("@AppID", AppID);
                cmd.Parameters.AddWithValue("@Diagnosis", Diagnosis ?? "");
                cmd.Parameters.AddWithValue("@Date", Date);

                if (string.IsNullOrWhiteSpace(Notes))
                    cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Notes", Notes);

                cmd.Parameters.AddWithValue("@PatientID", PatientID);

                conn.Open();
                var value = cmd.ExecuteScalar();

                if (int.TryParse(Convert.ToString(value), out Id))
                    return Id;

                System.Diagnostics.Debug.WriteLine("ERROR: Failed to convert new MedicalRecord ID.");
                return -1;
            }
        }

        public static bool UpdateMedicalRecord(int MedicalRecordID, int AppID, string Diagnosis, DateTime Date, string Notes, int PatientID)
        {
            string Query = @"UPDATE MedicalRecords
                             SET AppointmentID = @AppID,
                                 Diagnosis = @Diagnosis,
                                 DiagnosisDate = @Date,
                                 Notes = @Notes,
                                 PatientID = @PatientID
                             WHERE MedicalRecordID = @MedicalRecordID";

            using (SqlConnection conn = new SqlConnection(clsDataSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                cmd.Parameters.AddWithValue("@AppID", AppID);
                cmd.Parameters.AddWithValue("@Diagnosis", Diagnosis ?? "");
                cmd.Parameters.AddWithValue("@Date", Date);

                if (string.IsNullOrWhiteSpace(Notes))
                    cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Notes", Notes);

                cmd.Parameters.AddWithValue("@PatientID", PatientID);
                cmd.Parameters.AddWithValue("@MedicalRecordID", MedicalRecordID);

                try
                {
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR updating MedicalRecord: " + ex.Message);
                    return false;
                }
            }
        }

        public static bool GetMedicalRecordByID(int MedicalRecordID, ref int AppID, ref string Diagnosis, ref DateTime DiagnosisDate, ref string Notes, ref int PatientID)
        {
            string Query = @"SELECT * FROM MedicalRecords WHERE MedicalRecordID = @MedicalRecordID;";

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
                            AppID = Convert.ToInt32(reader["AppointmentID"]);
                            Diagnosis = reader["Diagnosis"]?.ToString() ?? "";
                            DiagnosisDate = Convert.ToDateTime(reader["DiagnosisDate"]);
                            Notes = reader["Notes"] == DBNull.Value ? "" : reader["Notes"].ToString();
                            PatientID = Convert.ToInt32(reader["PatientID"]);
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR GetMedicalRecordByID: " + ex.Message);
                }

                return false;
            }
        }

        public static bool GetMedicalRecordByAppID(ref int MedicalRecordID, int AppointmentID, ref string Diagnosis, ref DateTime DiagnosisDate, ref string Notes, ref int PatientID)
        {
            string Query = @"SELECT * FROM MedicalRecords WHERE AppointmentID = @AppointmentID;";

            using (SqlConnection conn = new SqlConnection(clsDataSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                cmd.Parameters.AddWithValue("@AppointmentID", AppointmentID);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            MedicalRecordID = Convert.ToInt32(reader["MedicalRecordID"]);
                            Diagnosis = reader["Diagnosis"]?.ToString() ?? "";
                            DiagnosisDate = Convert.ToDateTime(reader["DiagnosisDate"]);
                            Notes = reader["Notes"] == DBNull.Value ? "" : reader["Notes"].ToString();
                            PatientID = Convert.ToInt32(reader["PatientID"]);
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR GetMedicalRecordByAppID: " + ex.Message);
                }

                return false;
            }
        }

        public static bool ExistsForAppointment(int appointmentID)
        {
            string query = "SELECT COUNT(*) FROM MedicalRecords WHERE AppointmentID = @AppID;";
            using (SqlConnection conn = new SqlConnection(clsDataSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@AppID", appointmentID);
                conn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public static DataTable GetAllMedicalRecordsForPatient(int PatientID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(clsDataSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetAllMedicalRecordsForPatient", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PatientID", PatientID);

                    try
                    {
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("ERROR Data - MedicalRecords GetAllPatient: " + ex.Message);
                    }
                }
            }
            return dt;
        }
    }
}

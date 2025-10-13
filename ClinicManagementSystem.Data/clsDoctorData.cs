using System;
using System.Data;
using System.Data.SqlClient;

namespace ClinicManagementSystem.Data
{
    public class clsDoctorData
    {
        private static readonly string connectionString = clsDataSettings.ConnectionString;

        public static int AddNewDoctor(int personID, int specializationID, double consultationFee)
        {
            const string query = @"
                                INSERT INTO Doctors (PersonID, SpecializationID, ConsultationFee)
                                VALUES (@PersonID, @SpecializationID, @ConsultationFee);
                                SELECT CAST(SCOPE_IDENTITY() AS int);";

            using (var connect = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, connect))
            {
                cmd.Parameters.AddWithValue("@PersonID", personID);
                cmd.Parameters.AddWithValue("@SpecializationID", specializationID);
                cmd.Parameters.AddWithValue("@ConsultationFee", consultationFee);

                connect.Open();
                var result = cmd.ExecuteScalar();
                return (result != null) ? Convert.ToInt32(result) : -1;
            }
        }

        public static bool UpdateDoctor(int doctorID, int specializationId, double consultationFee)
        {
            const string query = @"
                                    UPDATE Doctors
                                    SET SpecializationID = @SpecializationID,
                                    ConsultationFee  = @ConsultationFee
                                    WHERE DoctorID = @DoctorID;";

            using (var connection = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@SpecializationID", specializationId);
                cmd.Parameters.AddWithValue("@ConsultationFee", consultationFee);
                cmd.Parameters.AddWithValue("@DoctorID", doctorID);

                connection.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool DeleteDoctor(int doctorID)
        {
            int rowsAffected = 0;

            const string query = "DELETE FROM Doctors WHERE DoctorID = @DoctorID;";

            using (SqlConnection connect = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, connect))
            {
                cmd.Parameters.AddWithValue("@DoctorID", doctorID);

                try
                {
                    connect.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR Database - Doctors (Delete): " + ex.Message);
                }
            }

            return rowsAffected > 0;
        }

        public static bool GetDoctorByID(int doctorID, ref int personID, ref int specializationId, ref double consultationFee)
        {
            const string query = @"
                                 SELECT PersonID, SpecializationID, ConsultationFee
                                 FROM Doctors
                                 WHERE DoctorID = @DoctorID;";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@DoctorID", doctorID);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read()) return false;

                    personID = Convert.ToInt32(reader["PersonID"]);
                    specializationId = (reader["SpecializationID"] == DBNull.Value) ? 0 : Convert.ToInt32(reader["SpecializationID"]);
                    consultationFee = Convert.ToDouble(reader["ConsultationFee"]);
                    return true;
                }
            }
        }

        public static DataTable GetAllDoctors()
        {
            var dt = new DataTable();

            const string query = @"
                              SELECT  d.DoctorID,
                                      p.FirstName,
                                      p.LastName,
                                      s.Name AS Specialization,
                                      d.ConsultationFee,
                                      p.DateOfBirth,
                                      CASE WHEN p.Gender = 1 THEN 'Male' ELSE 'Female' END AS Gender,
                                      p.Email
                              FROM Doctors d
                              INNER JOIN People p          ON d.PersonID = p.PersonID
                              LEFT  JOIN Specializations s  ON s.ID = d.SpecializationID;";
        
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (var r = cmd.ExecuteReader())
                    dt.Load(r);
            }

            return dt;
        }

        public static System.Collections.Generic.Dictionary<int, string> GetAllSpecializations()
        {
            var dict = new System.Collections.Generic.Dictionary<int, string>();

            const string query = @"SELECT ID, Name FROM Specializations;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                            dict[Convert.ToInt32(r["ID"])] = Convert.ToString(r["Name"]);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR Database - Specializations (Lookup): " + ex.Message);
                }
            }

            return dict;
        }
    }
}

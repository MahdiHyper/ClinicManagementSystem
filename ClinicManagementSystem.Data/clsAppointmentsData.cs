using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ClinicManagementSystem.Data
{
    public class clsAppointmentsData
    {
        private static string Connect = clsDataSettings.ConnectionString;

        public static int AddNewAppointment(int DoctorID, int PatientID,
            DateTime AppointmentDateTime, int Status, int PaymentID, string Notes)
        {
            using(SqlConnection conn = new SqlConnection(Connect))
            {
                conn.Open();

                string Query = @"INSERT INTO Appointments
                               (DoctorID,PatientID,AppointmentDateTime,Status,PaymentID,Notes)
                                VALUES
                               (@DoctorID,@PatientID,@AppointmentDateTime,@Status,@PaymentID,@Notes)
                               SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@DoctorID", DoctorID);
                    cmd.Parameters.AddWithValue("@PatientID", PatientID);
                    cmd.Parameters.AddWithValue("@AppointmentDateTime", AppointmentDateTime);
                    cmd.Parameters.AddWithValue("@Status", Status);
                    cmd.Parameters.AddWithValue("@PaymentID", PaymentID);
                    
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
                        var newId = cmd.ExecuteScalar();
                        return Convert.ToInt32(newId);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"ERROR Database - Appointments (AddNew)" + ex.ToString());
                        return -1;
                    }
                }

            }
        }

        public static bool UpdateAppointment (int AppointmentID, int DoctorID, int PatientID,
            DateTime AppointmentDateTime, int Status, int PaymentID, string Notes)
        {
            using (SqlConnection conn = new SqlConnection(Connect))
            {
                conn.Open();

                string Query = @"Update Appointments 
                                Set 
                                DoctorID = @DoctorID,
                                PatientID = @PatientID,
                                AppointmentDateTime = @AppointmentDateTime,
                                Status = @Status,
                                PaymentID = @PaymentID,
                                Notes = @Notes
                                WHERE AppointmentID = @AppointmentID;";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {

                    cmd.Parameters.AddWithValue("@DoctorID", DoctorID);
                    cmd.Parameters.AddWithValue("@PatientID", PatientID);
                    cmd.Parameters.AddWithValue("@AppointmentDateTime", AppointmentDateTime);
                    cmd.Parameters.AddWithValue("@Status", Status);
                    cmd.Parameters.AddWithValue("@PaymentID", PaymentID);
                    if (!string.IsNullOrEmpty(Notes))
                    {
                        cmd.Parameters.AddWithValue(@"Notes", Notes);
                    }else
                    {
                        cmd.Parameters.AddWithValue(@"Notes", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue(@"AppointmentID", AppointmentID);

                    try
                    {
                        int RowEffected = cmd.ExecuteNonQuery();
                        return RowEffected > 0;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"ERROR Database - Appointments (Update)" + ex.ToString());
                        return false;
                    }

                }
            }
        }

        public static bool DeleteAppointment (int AppointmentID)
        {
            using ( SqlConnection conn = new SqlConnection(Connect))
            {
                conn.Open();

                string Query = @"DELETE FROM Appointments 
                                WHERE AppointmentID = @AppointmentID;";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@AppointmentID", AppointmentID);

                    try
                    {
                        int RowEffected = (int)cmd.ExecuteNonQuery();
                        return RowEffected > 0;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine ("ERROR Databse - Appointments (Delete) " + ex.ToString());
                        return false;
                    }
                }
            }
        }

        public static DataTable GetAllAppointments ()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Connect))
            {
                conn.Open();

                string Query = @"SELECT AppointmentID
                                  ,StartAt
                                  ,DoctorName
                                  ,PatientName
                                  ,StatusText
                                  ,Notes
                                  FROM View_AllAppointments
                                    ORDER BY StartAt DESC";
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"ERROR Database - Appointments (All Appointments) {ex.Message}");
                    }
                }
            }
            return dt;
        }

        public static DataTable GetAllAppointmentsInRange(DateTime from, DateTime toExclusive)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(Connect))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                                    SELECT AppointmentID
                                  ,StartAt
                                  ,DoctorName
                                 ,PatientName
                                  ,StatusText
                                  ,Notes
                                    FROM dbo.View_AllAppointments
                                    WHERE StartAt >= @From AND StartAt < @To
                                    ORDER BY StartAt;";
                cmd.Parameters.Add("@From", SqlDbType.DateTime2).Value = from;
                cmd.Parameters.Add("@To", SqlDbType.DateTime2).Value = toExclusive;

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                        dt.Load(reader);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"DB ERROR GetAllAppointmentsInRange: {ex}");
                }
            }
            return dt;
        }

        public static bool GetAppointmentByID(int appointmentID,
                     ref int doctorID, ref int patientID, ref DateTime appointmentDate,
                     ref int status, ref int paymentID, ref string notes)
        {
            using (var conn = new SqlConnection(Connect))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                                SELECT DoctorID, PatientID, AppointmentDateTime, Status, PaymentID, Notes
                                FROM dbo.Appointments
                                WHERE AppointmentID = @AppointmentID;";
                
                cmd.Parameters.Add("@AppointmentID", SqlDbType.Int).Value = appointmentID;

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read()) return false;

                        doctorID = reader.GetInt32(reader.GetOrdinal("DoctorID"));
                        patientID = reader.GetInt32(reader.GetOrdinal("PatientID"));
                        appointmentDate = reader.GetDateTime(reader.GetOrdinal("AppointmentDateTime"));
                        status = reader.GetInt32(reader.GetOrdinal("Status"));
                        paymentID = reader.GetInt32(reader.GetOrdinal("PaymentID"));

                        int ordNotes = reader.GetOrdinal("Notes");
                        notes = reader.IsDBNull(ordNotes) ? null : reader.GetString(ordNotes);
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"DB ERROR GetAppointmentByID: {ex}");
                    return false;
                }
            }
        }

        public static bool IsDoctorFree(int doctorID, DateTime slotStart)
        {
            using (var conn = new SqlConnection(Connect))
            {
                string Query = @"
                             SELECT Found=1
                             FROM dbo.Appointments
                             WHERE DoctorID = @DoctorID
                               AND AppointmentDateTime = @Slot
                               AND Status = 1;";

                using (SqlCommand cmd = new SqlCommand(Query,conn))
                {
                    cmd.Parameters.Add("@DoctorID", SqlDbType.Int).Value = doctorID;
                    cmd.Parameters.Add("@Slot", SqlDbType.DateTime2).Value = slotStart;
                    try
                    {
                        conn.Open();
                        return cmd.ExecuteScalar() == null;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"ERROR Database - Appointments (isDoctorFree) {ex.Message}");
                        return false;
                    }
                }
            }
        }
        public static bool IsPatientFree(int PatientID, DateTime slotStart)
        {
            using (var conn = new SqlConnection(Connect))
            {
                string Query = @"
                             SELECT Found=1
                             FROM dbo.Appointments
                             WHERE PatientID = @PatientID
                               AND AppointmentDateTime = @Slot
                               AND Status = 1;";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.Add("@PatientID", SqlDbType.Int).Value = PatientID;
                    cmd.Parameters.Add("@Slot", SqlDbType.DateTime2).Value = slotStart;
                    try
                    {
                        conn.Open();
                        return cmd.ExecuteScalar() == null;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"ERROR Database - Appointments (IsPatientFree) {ex.Message}");
                        return false;
                    }
                }
            }
        }

        public static int GetNextScheduleAppointment()
        {
            int ID = -1;

            string Query = @"SELECT Top 1 a.AppointmentID
                        FROM Appointments a
                        WHERE a.AppointmentDateTime > GETDATE() AND a.Status = 1
                        ORDER BY a.AppointmentDateTime ASC;";

            using (var conn = new SqlConnection(Connect))
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                try
                {
                    conn.Open();

                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out ID))
                    {
                        return ID;
                    }

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"ERROR Data - GetNextScheduleAppointment: {ex.Message}");
                }

            }

            return ID;
        }


    }
}

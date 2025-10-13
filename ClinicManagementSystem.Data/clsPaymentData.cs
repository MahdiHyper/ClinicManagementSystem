using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystem.Data
{
    public class clsPaymentData
    {
        private static string ConnectString = clsDataSettings.ConnectionString;

        public static int AddNewPayment(double PaymentAmount, double PaymentReceived, DateTime PaymentDate)
        {
            string Query = @"INSERT INTO dbo.Payments
                        (PaymentAmount, PaymentReceived, PaymentDate)
                     VALUES
                        (@PaymentAmount, @PaymentReceived, @PaymentDate);
                     SELECT SCOPE_IDENTITY();";

            using (var connect = new SqlConnection(ConnectString))
            using (var cmd = new SqlCommand(Query, connect))
            {
                cmd.Parameters.AddWithValue("@PaymentAmount", PaymentAmount);
                cmd.Parameters.AddWithValue("@PaymentReceived", PaymentReceived);
                cmd.Parameters.AddWithValue("@PaymentDate", PaymentDate);

                try
                {
                    connect.Open();
                    var result = cmd.ExecuteScalar();
                    return (result != null) ? Convert.ToInt32(result) : -1;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR - Data Payments (AddNew) " + ex.Message);
                }
            }
            return -1;
        }


        public static bool UpdatePayment(int PaymentID, double PaymentAmount, double PaymentReceived,
            DateTime PaymentDate)
        {
            string Query = @"UPDATE dbo.Payments
                     SET  PaymentAmount  = @PaymentAmount,
                          PaymentReceived= @PaymentReceived,
                          PaymentDate    = @PaymentDate
                     WHERE PaymentID     = @PaymentID;";

            using (var connect = new SqlConnection(ConnectString))
            using (var cmd = new SqlCommand(Query, connect))
            {
                cmd.Parameters.AddWithValue("@PaymentAmount", PaymentAmount);
                cmd.Parameters.AddWithValue("@PaymentReceived", PaymentReceived);
                cmd.Parameters.AddWithValue("@PaymentDate", PaymentDate);
                cmd.Parameters.AddWithValue("@PaymentID", PaymentID);

                try
                {
                    connect.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR - Data Payments (Update) " + ex.Message);
                    return false;
                }
            }
        }


        public static bool GetPaymentByID(int PaymentID, ref double PaymentAmount, ref double PaymentReceived,
        ref DateTime PaymentDate)
        {
            string Query = @"SELECT PaymentAmount , PaymentReceived , PaymentDate
                     FROM dbo.Payments
                     WHERE PaymentID = @PaymentID;";

            using (var conn = new SqlConnection(ConnectString))
            using (var cmd = new SqlCommand(Query, conn))
            {
                cmd.Parameters.AddWithValue("@PaymentID", PaymentID);

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PaymentAmount = Convert.ToDouble(reader["PaymentAmount"]);   // ✅
                            PaymentReceived = Convert.ToDouble(reader["PaymentReceived"]); // ✅
                            PaymentDate = Convert.ToDateTime(reader["PaymentDate"]);
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR - Data Payments (GetByID) " + ex.Message);
                    return false;
                }
            }
            return false;
        }

        public static DataTable GetAllPayments()
        {
            DataTable dt = new DataTable();

            string Query = @"SELECT * FROM View_GetAllPaymentsInfo;";

            using (var conn = new SqlConnection(clsDataSettings.ConnectionString))
            using (var cmd = new SqlCommand(Query, conn))
            {
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                       
                     dt.Load(reader);
                        
                    }

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR Data - Payments GetALL" + ex.Message);
                }
            }
            return dt;
        }

        public static int GetPatientIdByPaymentID(int PaymentID)
        {
            int PaitentID = -1;

            string Query = @"SELECT PatientID FROM 
                            Payments INNER JOIN 
                            Appointments ON Appointments.PaymentID = Payments.PaymentID
                            WHERE Payments.PaymentID = @PaymentID
                            ";

            using (SqlConnection conn = new SqlConnection(clsDataSettings.ConnectionString))
            using (var cmd = new SqlCommand( Query, conn))
            {
                cmd.Parameters.AddWithValue("@PaymentID", PaymentID);

                try
                {
                    conn.Open();

                    var dID = cmd.ExecuteScalar();
                    if (int.TryParse(Convert.ToString(dID), out PaitentID))
                    {
                        return PaitentID;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"ERROR Data - Payment GetPatientID {ex.Message}");
                }
            }
            return PaitentID;
        }

        public static int GetDoctorIdByPaymentID(int PaymentID)
        {
            int DoctorID = -1;

            string Query = @"SELECT DoctorID FROM 
                            Payments INNER JOIN 
                            Appointments ON Appointments.PaymentID = Payments.PaymentID
                            WHERE Payments.PaymentID = @PaymentID
                                         ";

            using (SqlConnection conn = new SqlConnection(clsDataSettings.ConnectionString))
            using (var cmd = new SqlCommand(Query, conn))
            {
                cmd.Parameters.AddWithValue("@PaymentID", PaymentID);

                try
                {
                    conn.Open();

                    var dID = cmd.ExecuteScalar();
                    if (int.TryParse(Convert.ToString(dID), out DoctorID))
                    {
                        return DoctorID;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"ERROR Data - Payment GetPatientID {ex.Message}");
                }
            }
            return DoctorID;
        }

        public static decimal GetTotalAmountReceived()
        {
            decimal total = 0;
            string query = "SELECT SUM(PaymentReceived) FROM Payments;";

            using (SqlConnection conn = new SqlConnection(clsDataSettings.ConnectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                        total = Convert.ToDecimal(result);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR DataLayer - Payments GetTotalReceived: " + ex.Message);
                }
            }

            return total;
        }


    }
}

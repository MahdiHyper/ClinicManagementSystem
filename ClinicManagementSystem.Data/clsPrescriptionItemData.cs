using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystem.Data
{
    public class clsPrescriptionItemData
    {
        public static int AddNewPrescriptionItem(int PrescriptionID, int MedicationID,
            double Dosage, string DosageType, string DosageNote)
        {
            string Query = @"INSERT INTO PrescriptionItems (PrescriptionID, MedicationID, 
                                Dosage, DosageType, DosageNote)
                                VALUES
                                (@PrescriptionID, @MedicationID, 
                                @Dosage, @DosageType, @DosageNote);
                                SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = new SqlConnection(clsDataSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                cmd.Parameters.AddWithValue("@PrescriptionID", PrescriptionID);
                cmd.Parameters.AddWithValue("@MedicationID", MedicationID);
                cmd.Parameters.AddWithValue("@Dosage", Dosage);
                cmd.Parameters.AddWithValue("@DosageType", DosageType);
                if (!string.IsNullOrEmpty(DosageNote))
                {
                    cmd.Parameters.AddWithValue("@DosageNote", DosageNote);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DosageNote", DBNull.Value);
                }

                conn.Open();
                object result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        public static bool DeletePrescriptionItem(int PrescriptionItemID)
        {
            string Query = @"DELETE FROM PrescriptionItems WHERE PrescriptionItemID = @PrescriptionItemID";
            using (SqlConnection conn = new SqlConnection(clsDataSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                cmd.Parameters.AddWithValue("@PrescriptionItemID", PrescriptionItemID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public static DataTable GetAllByPrescriptionID(int PrescriptionID)
        {
            string Query = @"SELECT * FROM PrescriptionItems WHERE PrescriptionID = @PrescriptionID";
            var table = new DataTable();

            using (SqlConnection conn = new SqlConnection(clsDataSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                cmd.Parameters.AddWithValue("@PrescriptionID", PrescriptionID);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    table.Load(reader);
                }
            }

            return table;
        }
    }
}

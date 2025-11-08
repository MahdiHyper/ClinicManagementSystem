using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System;

namespace ClinicManagementSystem.Data
{
    public class clsUserData
    {
        private static string ConnectionString = clsDataSettings.ConnectionString;

        public static int AddNewUser(int PersonID, string UserName, string PasswordHash, bool IsActive)
        {
            int UserID = -1;

            string Query = @"
                INSERT INTO Users (PersonID, UserName, PasswordHash, IsActive)
                VALUES (@PersonID, @UserName, @PasswordHash, @IsActive);
                SELECT SCOPE_IDENTITY();";

            using (SqlConnection Connect = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, Connect))
            {
                cmd.Parameters.AddWithValue("@PersonID", PersonID);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@PasswordHash", PasswordHash);
                cmd.Parameters.AddWithValue("@IsActive", IsActive);

                try
                {
                    Connect.Open();
                    object ID = cmd.ExecuteScalar();
                    if (ID != null && int.TryParse(ID.ToString(), out int NewID))
                        UserID = NewID;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR Database - Users (AddNew): " + ex.Message);
                }
            }
            return UserID;
        }

        public static bool UpdateUser(int UserID, string UserName, bool IsActive)
        {
            int RowsAffected = 0;
            string Query = @"UPDATE Users SET UserName = @UserName, IsActive = @IsActive WHERE UserID = @UserID;";

            using (SqlConnection Connect = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, Connect))
            {
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@IsActive", IsActive);

                try
                {
                    Connect.Open();
                    RowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR Database - Users (Update): " + ex.Message);
                }
            }
            return RowsAffected > 0;
        }

        public static bool DeleteUser(int UserID)
        {
            int RowsAffected = 0;
            string Query = @"DELETE FROM Users WHERE UserID = @UserID;";

            using (SqlConnection Connect = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, Connect))
            {
                cmd.Parameters.AddWithValue("@UserID", UserID);

                try
                {
                    Connect.Open();
                    RowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR Database - Users (Delete): " + ex.Message);
                }
            }
            return RowsAffected > 0;
        }

        public static bool FindUserByUserID(int UserID, ref int PersonID, ref string UserName, ref bool IsActive)
        {
            bool IsFound = false;
            string Query = @"SELECT * FROM Users WHERE UserID = @UserID;";

            using (SqlConnection Connect = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, Connect))
            {
                cmd.Parameters.AddWithValue("@UserID", UserID);

                try
                {
                    Connect.Open();
                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            IsFound = true;
                            PersonID = Convert.ToInt32(Reader["PersonID"]);
                            UserName = Convert.ToString(Reader["UserName"]);
                            IsActive = Convert.ToBoolean(Reader["IsActive"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR Database - Users (FindByUserID): " + ex.Message);
                }
            }
            return IsFound;
        }

        public static bool FindUserByUserName(string UserName, ref int UserID, ref int PersonID, ref bool IsActive)
        {
            bool IsFound = false;
            string Query = "SELECT TOP 1 UserID, PersonID, IsActive FROM Users WHERE UserName = @UserName;";

            using (SqlConnection Connect = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, Connect))
            {
                cmd.Parameters.AddWithValue("@UserName", UserName);

                try
                {
                    Connect.Open();
                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            UserID = (int)Reader["UserID"];
                            PersonID = (int)Reader["PersonID"];
                            IsActive = (bool)Reader["IsActive"];
                            IsFound = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR Database - Users (FindByUserName): " + ex.Message);
                }
            }
            return IsFound;
        }

        public static bool LoginByUserNameAndPassword(string UserName, string PasswordHash)
        {
            bool IsFound = false;
            string Query = @"SELECT TOP 1 1 FROM Users WHERE UserName = @UserName AND PasswordHash = @PasswordHash;";

            using (SqlConnection Connect = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, Connect))
            {
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@PasswordHash", PasswordHash);

                try
                {
                    Connect.Open();
                    object result = cmd.ExecuteScalar();
                    IsFound = (result != null);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR Database - Users (Login): " + ex.Message);
                }
            }
            return IsFound;
        }

        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            string Query = "SELECT * FROM View_UsersInfo;";

            using (SqlConnection Connect = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, Connect))
            {
                try
                {
                    Connect.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                            dt.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR Database - Users (GetAll): " + ex.Message);
                }
            }
            return dt;
        }

        public static bool IsUserNameTaken(string UserName)
        {
            string Query = "SELECT 1 FROM Users WHERE UserName = @UserName;";
            object Row = null;

            using (SqlConnection Connect = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, Connect))
            {
                cmd.Parameters.AddWithValue("@UserName", UserName);

                try
                {
                    Connect.Open();
                    Row = cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR Database - Users (IsUserNameTaken): " + ex.Message);
                }
            }
            return Row != null;
        }

        public static bool UpdatePassword(int UserID, string NewPasswordHash)
        {
            int rowsAffected = 0;
            string Query = @"UPDATE Users SET PasswordHash = @PasswordHash WHERE UserID = @UserID;";

            using (SqlConnection Connect = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, Connect))
            {
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@PasswordHash", NewPasswordHash);

                try
                {
                    Connect.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR Database - Users (UpdatePassword): " + ex.Message);
                }
            }
            return rowsAffected > 0;
        }

        public static bool IsUserActive(int UserID)
        {
            string Query = "SELECT 1 FROM Users WHERE UserID = @UserID AND IsActive = 1;";

            using (SqlConnection Connect = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, Connect))
            {
                cmd.Parameters.AddWithValue("@UserID", UserID);

                try
                {
                    Connect.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR Database - Users (IsUserActive): " + ex.Message);
                    return false;
                }
            }
        }
    }
}

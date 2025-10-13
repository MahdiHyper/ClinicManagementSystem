using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Linq;

namespace ClinicManagementSystem.Data
{
    public class clsUserData
    {
        private static string ConnectionString = clsDataSettings.ConnectionString;

        public static int AddNewUser(int PersonID, string UserName, string PasswordHash
            , bool IsActive)
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
                    {
                        UserID = NewID;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("SQL ERROR in AddNewUser: " + ex.Message);
                }

            }
            return UserID;

        }

        public static bool UpdateUser(int UserID, string UserName, bool IsActive)
        {
            int RowsAffected = 0;

            string Query = @"UPDATE Users 
                           SET 
                            UserName = @UserName
                           ,IsActive = @IsActive
                           WHERE UserID = @UserID;";

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
                    System.Diagnostics.Debug.WriteLine($"Error Database - User (Update): {ex.Message}");
                }

            }
            return RowsAffected > 0;
        }

        public static bool DeleteUser(int UserID)
        {
            int RowsAffected = 0;

            string Query = @"DELETE FROM Users
                         WHERE UserID = @UserID;";

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
                    System.Diagnostics.Debug.WriteLine($"ERROR Database - User (Delete) : {ex.Message}");
                }

            }


            return RowsAffected > 0;
        }

        public static bool FindUserByUserID(int UserID, ref int PersonID
            , ref string UserName, ref bool IsActive)
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
                    System.Diagnostics.Debug.WriteLine("DataBase : Faild to Find User with UserID = " + ex.Message);

                    IsFound = false; ;
                }
            }
            return IsFound;
        }

        public static bool FindUserByUserName (string UserName, ref int UserID,
            ref int  PersonID, ref bool  IsActive)
        {
            bool IsFound = false;

            string Query = "SELECT UserID, PersonID, IsActive FROM Users WHERE UserName = @UserName";

            using (SqlConnection Connect = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand( Query, Connect))
            {
                cmd.Parameters.AddWithValue ("@UserName", UserName);

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
                    IsFound = false;
                    System.Diagnostics.Debug.WriteLine("Database - Users (FindUserByUserName) " + ex);
                }
            }

            return IsFound;
        }

        public static bool LoginByUserNameAndPassword(string UserName, string PasswordHash)
        {
            bool IsFound = false;

            string Query = @"SELECT 1 FROM Users
                          WHERE UserName = @UserName AND PasswordHash = @PasswordHash;";

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
                    System.Diagnostics.Debug.WriteLine("Database - Users Login " + ex.Message);
                    IsFound = false;
                }

            }
            return IsFound;
        }

        public static DataTable GetAllUsers()
        {
            string Query = "SELECT * FROM View_UsersInfo;";
            DataTable dt = new DataTable();

            using (var Connect = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(Query, Connect))
            {

                try
                {
                    Connect.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Database - Users : " + ex.Message);
                }

                return dt;

            }
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
                    System.Diagnostics.Debug.WriteLine("Database - Users :" + ex.Message);
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
                    System.Diagnostics.Debug.WriteLine($"Error Database  - User (UpdatePass) : {ex.Message}");
                    return false;
                }
            }

            return rowsAffected > 0;
        }

        public static bool IsUserActive(int UserID)
        {
            string Query = "SELECT COUNT(*) FROM Users WHERE UserID = @UserID AND IsActive = 1";

            using (SqlConnection Connect = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(Query, Connect))
            {
                cmd.Parameters.AddWithValue("@UserID", UserID);

                try
                {
                    Connect.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Database - Users(IsUserActive): {ex.Message}");
                    return false;
                }
            }
        }

        
    }
}

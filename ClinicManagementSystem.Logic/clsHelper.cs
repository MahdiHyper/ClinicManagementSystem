using Microsoft.Win32;
using System;
using System.Security.Cryptography;
using System.Text;

namespace ClinicManagementSystem.Logic
{
    static public class clsHelper
    {
        public static clsUser CurrentUser;

        static private string _Salt = "MahdiHyper404";
        public static string ComputeHash(string Password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(_Salt + Password));


                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public static void RememberUsernameAndPassword(string username, string password)
        {
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\CMS";

            try
            {
                
                Registry.SetValue(keyPath, "Username", username, RegistryValueKind.String);
                Registry.SetValue(keyPath, "Password", password, RegistryValueKind.String);
                Registry.SetValue(keyPath, "RememberMe", "true", RegistryValueKind.String);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error saving credentials: " + ex.Message);
            }
        }
        public static bool GetRememberedUsernameAndPassword(ref string username, ref string password)
        {
            username = string.Empty;
            password = string.Empty;

            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\CMS";

            try
            {
                string rememberMe = Registry.GetValue(keyPath, "RememberMe", null) as string;
                if (rememberMe != "true") return false;

                username = Registry.GetValue(keyPath, "Username", null) as string;
                password = Registry.GetValue(keyPath, "Password", null) as string;

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading credentials: " + ex.Message);
                return false;
            }
        }

    }

}

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CipherShield
{
   

    public static class PasswordStorage
    {
        
        // Additional entropy for the password encryption
        private static readonly byte[] AdditionalEntropy = new byte[]
        {
            91, 182, 173, 64, 155, 246, 137, 228,
            19, 200, 211, 122, 133, 144, 255, 166,
            177, 188, 199, 210, 221, 232, 243, 254,
            165, 176, 187, 198, 209, 220, 231, 242
        };

        // Save the Encryption password to a file

        public static void SavePassword(string password)
        {
            byte[] encryptedPassword = ProtectedData.Protect(
                Encoding.UTF8.GetBytes(password), AdditionalEntropy, DataProtectionScope.CurrentUser);
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cipher Shield");
            Directory.CreateDirectory(appDataPath); // Ensure the directory exists
            string filePath = Path.Combine(appDataPath, "Encryption-Password.dat");
            File.WriteAllBytes(filePath, encryptedPassword);
        }


        // Retrieve the Encryption password from the file
        public static string GetPassword()
        {
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cipher Shield");
            string filePath = Path.Combine(appDataPath, "Encryption-Password.dat");

            byte[] encryptedPassword = File.ReadAllBytes(filePath);
            byte[] decryptedPassword = ProtectedData.Unprotect(
                encryptedPassword, AdditionalEntropy, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decryptedPassword);
        }


            // Update the master password
            //public static void UpdatePassword(string newPassword)
            //{
            //    SavePassword(newPassword);
            //}

            //// change the master password
            //public static void ChangeDatabasePassword(string databaseFilePath, string oldPassword, string newPassword)
            //{
            //    using (var connection = new SqliteConnection($"Data Source={databaseFilePath};Password={oldPassword};"))
            //    {
            //        connection.Open();

            //        using (var command = new SqliteCommand($"PRAGMA rekey = '{newPassword}';", connection))
            //        {

            //            command.ExecuteNonQuery();
            //        }
            //    }
            //}

        }
    }



    

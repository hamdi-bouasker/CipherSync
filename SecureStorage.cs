using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace CipherShield
{
    public static class SecureStorage
    {
        // Additional entropy for the password encryption
        private static readonly byte[] AdditionalEntropy = new byte[]
        {
        91, 182, 173, 64, 155, 246, 137, 228,
        19, 200, 211, 122, 133, 144, 255, 166,
        177, 188, 199, 210, 221, 232, 243, 254,
        165, 176, 187, 198, 209, 220, 231, 242
        };

        // Save the master password to a file
        public static void SavePassword(string password)
        {
            byte[] encryptedPassword = ProtectedData.Protect(
                Encoding.UTF8.GetBytes(password), AdditionalEntropy, DataProtectionScope.CurrentUser);
            System.IO.File.WriteAllBytes("Master-Password.dat", encryptedPassword);
        }

        // Retrieve the master password from the file
        public static string GetPassword()
        {
            byte[] encryptedPassword = System.IO.File.ReadAllBytes("Master-Password.dat");
            byte[] decryptedPassword = ProtectedData.Unprotect(
                encryptedPassword, AdditionalEntropy, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decryptedPassword);
        }

        // Update the master password
        public static void UpdatePassword(string newPassword) 
        { 
            SavePassword(newPassword); 
        }

        // change the master password file
        public static void ChangeDatabasePassword(string databaseFilePath, string oldPassword, string newPassword)
        {
            using (var connection = new SqliteConnection($"Data Source={databaseFilePath};Password={oldPassword};"))
            {
                connection.Open();

                using (var command = new SqliteCommand($"PRAGMA rekey = '{newPassword}';", connection))
                {
                    
                    command.ExecuteNonQuery();
                }
            }
        }


    }


}

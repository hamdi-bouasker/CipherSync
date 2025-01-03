using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CipherShield
{
    public static class SecureStorage
    {
        private static readonly byte[] AdditionalEntropy = new byte[]
        {
        91, 182, 173, 64, 155, 246, 137, 228,
        19, 200, 211, 122, 133, 144, 255, 166,
        177, 188, 199, 210, 221, 232, 243, 254,
        165, 176, 187, 198, 209, 220, 231, 242
        };

        public static void SavePassword(string password)
        {
            byte[] encryptedPassword = ProtectedData.Protect(
                Encoding.UTF8.GetBytes(password), AdditionalEntropy, DataProtectionScope.CurrentUser);
            System.IO.File.WriteAllBytes("Master-Password.dat", encryptedPassword);
        }

        public static string GetPassword()
        {
            byte[] encryptedPassword = System.IO.File.ReadAllBytes("Master-Password.dat");
            byte[] decryptedPassword = ProtectedData.Unprotect(
                encryptedPassword, AdditionalEntropy, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decryptedPassword);
        }
    }


}

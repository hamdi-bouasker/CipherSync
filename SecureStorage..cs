using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CipherSync
{
    public static class SecureStorage
    {
        public static void SavePassword(string password)
        {
            byte[] encryptedPassword = ProtectedData.Protect(
                Encoding.UTF8.GetBytes(password), null, DataProtectionScope.CurrentUser);
            System.IO.File.WriteAllBytes("Master-Password.dat", encryptedPassword);
        }

        public static string GetPassword()
        {
            byte[] encryptedPassword = System.IO.File.ReadAllBytes("Master-Password.dat");
            byte[] decryptedPassword = ProtectedData.Unprotect(
                encryptedPassword, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decryptedPassword);
        }
    }

}

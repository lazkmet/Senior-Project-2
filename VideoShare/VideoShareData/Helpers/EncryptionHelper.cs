using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VideoShareData.Helpers
{
    internal class EncryptionHelper //Encryption functions designed by Ross Lewerenz, 3/30/23. I have adapted them to fit my object model and avoid obsolete code
    {
        public static byte[] CreatePasswordHash(string rawPassword) {
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            var pbkdf2 = new Rfc2898DeriveBytes(Encoding.ASCII.GetBytes(rawPassword), salt, 100000);
            byte[] hashBytes = new byte[36];
            byte[] hash = pbkdf2.GetBytes(20);
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return hashBytes;
        }
        public static bool CheckPasswordHash(string rawPassword, byte[] hashBytes) {
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(rawPassword, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;
            return true;
        }
    }
}

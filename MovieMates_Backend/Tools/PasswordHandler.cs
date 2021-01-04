using System;
using System.Security.Cryptography;

namespace MovieMates_Backend.Tools
{
    public class PasswordHandler
    {
        public string[] GenerateSaltAndHash(string password)
        {
            string[] data = new string[2];
            byte[] saltBytes = new byte[64];

            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(saltBytes);

            data[1] = Convert.ToBase64String(saltBytes); // Salt
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            data[0] = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)); // Hash

            return data;
        }

        public bool VerifyPassword(string password, string passwordHash, string passwordSalt)
        {
            var saltBytes = Convert.FromBase64String(passwordSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == passwordHash;
        }
    }
}

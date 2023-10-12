using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConexiónAmigo.Common.EncryptionDecryption
{    
    public static class EncryptionDecryption
    {
        public static void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;

                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            }
        }

        public static bool VerifyPasswordHash(string Password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));

                return ComputedHash.SequenceEqual(PasswordHash);
            }

        }

        public static string CreateRandomToken(string Password)
        {
            var token = Guid.NewGuid().ToString();
            return token;
        }
    }
}

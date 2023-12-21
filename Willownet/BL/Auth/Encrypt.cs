using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Willownet.BL.Auth
{
    public class Encrypt : IEncrypt
    {
        // 64 cause HMACSHA512 (512 / 8)
        public string HashPassword(string password, string salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                System.Text.Encoding.ASCII.GetBytes(salt),
                KeyDerivationPrf.HMACSHA512,
                5000,
                64));
        }
    }
}

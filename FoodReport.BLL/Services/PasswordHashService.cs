using System;
using FoodReport.BLL.Interfaces.PasswordHashing;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace FoodReport.BLL.Services
{
    public class PasswordHashService : IPasswordHasher
    {
        private readonly byte[] Salt = new byte[128 / 8];

        public PasswordHashService()
        {
            Salt = Convert.FromBase64String("QVm9ws5UMvwFV5No2rta/A==");
        }

        public string HashPassword(string password)
        {
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                Salt,
                KeyDerivationPrf.HMACSHA1,
                10000,
                256 / 8));
            var wtf = Convert.ToBase64String(Salt);
            var fu = Convert.FromBase64String(wtf);
            return hashed;
        }
    }
}
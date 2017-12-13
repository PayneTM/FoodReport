using FoodReport.BLL.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace FoodReport.BLL.Services
{
    public class PasswordHashService : IPasswordHasher
    {
        private byte[] Salt = new byte[128 / 8];
        public PasswordHashService()
        {
            Salt = Convert.FromBase64String("QVm9ws5UMvwFV5No2rta/A==");
        }
        public string HashPassword(string password)
        {

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            var wtf = Convert.ToBase64String(Salt);
            var fu = Convert.FromBase64String(wtf);
            return hashed;
        }

    }
}

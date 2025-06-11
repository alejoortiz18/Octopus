using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class PasswordHelper
    {
        public static (byte[] Hash, byte[] Salt) CrearHash(string password)
        {
            // Generar salt aleatorio de 16 bytes
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Usar PBKDF2 para crear el hash
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32); // 256 bits

            return (hash, salt);
        }

        public static bool VerificarPassword(string password, byte[] hashGuardado, byte[] saltGuardado)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, saltGuardado, 10000, HashAlgorithmName.SHA256);
            byte[] hashComparar = pbkdf2.GetBytes(32);

            return hashComparar.SequenceEqual(hashGuardado);
        }
    }
}

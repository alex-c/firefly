using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Cryptography;

namespace Firefly.Services
{
    /// <summary>
    /// Provides password hashing functionality.
    /// </summary>
    public class PasswordHashingService
    {
        /// <summary>
        /// A logger for local logging needs.
        /// </summary>
        private ILogger Logger { get; }

        /// <summary>
        /// Instantiaties the service.
        /// </summary>
        /// <param name="loggerFactory">A factory to create loggers from.</param>
        public PasswordHashingService(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<PasswordHashingService>();
        }

        /// <summary>
        /// Generates a salt and hashes a password.
        /// </summary>
        /// <param name="password">Password to hash.</param>
        /// <returns>Returns the hashed password and the salt used to hash.</returns>
        /// <exception cref="ArgumentException">Thrown if no valid password was provided.</exception>
        public (string, byte[]) HashAndSaltPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("No valid password provided.", nameof(password));
            }

            byte[] salt = GenerateSalt();
            return (HashAndSaltPassword(password, salt), salt);
        }

        /// <summary>
        /// Hashes a password using a given salt.
        /// Source: https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-3.1
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt to hash the password with.</param>
        /// <returns>Returns the hashed password.</returns>
        /// <exception cref="ArgumentException">Thrown if either the provided password or salt are not valid.</exception>
        public string HashAndSaltPassword(string password, byte[] salt)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("No valid password provided.", nameof(password));
            }

            if (salt == null || salt.Length != 128 / 8)
            {
                throw new ArgumentException("No valid salt provided.", nameof(salt));
            }

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        /// <summary>
        /// Generates a 128-bit salt using secure PRNG.
        /// Source: https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-3.1
        /// </summary>
        /// <returns>Reutrns a new random salt.</returns>
        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
    }
}

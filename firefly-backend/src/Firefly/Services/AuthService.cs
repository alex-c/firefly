using Firefly.Models;
using Firefly.Repositories;
using Firefly.Services.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Firefly.Services
{
    /// <summary>
    /// A service that provides authentication functionality.
    /// </summary>
    public class AuthService
    {
        /// <summary>
        /// Magic string for the administrator role in JWTs.
        /// </summary>
        public const string ROLE_ADMINISTRATOR = "Administrator";

        /// <summary>
        /// Provides password hashing functionalities.
        /// </summary>
        private PasswordHashingService PasswordHashingService { get; }

        /// <summary>
        /// Grants access to user information.
        /// </summary>
        private IReadOnlyUserRepository UserRepository { get; }
        /// <summary>
        /// Signing credentials for JWTs.
        /// </summary>
        /// 
        private SigningCredentials SigningCredentials { get; }

        /// <summary>
        /// Lifetime of issued JWTs.
        /// </summary>
        private TimeSpan JwtLifetime { get; }

        /// <summary>
        /// Issuer name of issued JWTs.
        /// </summary>
        private string JwtIssuer { get; }

        /// <summary>
        /// A logger for local logging needs.
        /// </summary>
        private ILogger Logger { get; }

        /// <summary>
        /// Sets up the service with all known authentication providers.
        /// </summary>
        /// <param name="loggerFactory">Fasctory to create loggers from.</param>
        /// <param name="passwordHashingService">Provides password hashing functionalities.</param>
        /// <param name="userRepository">Provides users.</param>
        /// <param name="configuration">App configuration for JWT signing information.</param>
        public AuthService(ILoggerFactory loggerFactory, PasswordHashingService passwordHashingService, IReadOnlyUserRepository userRepository, IConfiguration configuration)
        {
            Logger = loggerFactory.CreateLogger<AuthService>();
            PasswordHashingService = passwordHashingService;
            UserRepository = userRepository;

            // JWT-related configuration
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Secret")));
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtLifetime = TimeSpan.FromMinutes(configuration.GetValue<int>("Jwt:LifetimeInMinutes"));
            JwtIssuer = configuration.GetValue<string>("Jwt:Issuer");
        }

        /// <summary>
        /// Attempts to authenticate a user.
        /// </summary>
        /// <param name="id">The user's unique ID.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="serializedToken">The serialized token.</param>
        /// <returns>Returns whether authentication was successful.</returns>
        /// <exception cref="Firefly.Services.Exceptions.EntityNotFoundException">User</exception>
        public bool TryAuthenticate(string id, string password, out string serializedToken)
        {
            serializedToken = null;

            // Get user
            User user = UserRepository.GetUser(id);
            if (user == null)
            {
                throw new EntityNotFoundException("User", id);
            }

            // Check password
            if (user.Password != PasswordHashingService.HashAndSaltPassword(password, user.Salt))
            {
                return false;
            }

            // Set user claims
            List<Claim> claims = new List<Claim>
            {
                // Add subject, name, role
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim("name", user.Name),
            };
            if (user.IsAdmin)
            {
                claims.Add(new Claim("role", ROLE_ADMINISTRATOR));
            }

            // Generate token
            JwtSecurityToken token = new JwtSecurityToken(JwtIssuer, null, claims, expires: DateTime.Now.Add(JwtLifetime), signingCredentials: SigningCredentials);
            serializedToken = new JwtSecurityTokenHandler().WriteToken(token);

            // Done!
            return true;
        }
    }
}

using Firefly.Models;
using Firefly.Repositories;
using Firefly.Services.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Firefly.Services
{
    /// <summary>
    /// A service that provides user query and user administration functionality.
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// Provides password hashing features.
        /// </summary>
        private PasswordHashingService PasswordHashingService { get; }

        /// <summary>
        /// Provides access to user data persistence.
        /// </summary>
        private IUserRepository UserRepository { get; }

        /// <summary>
        /// A logger for local logging needs.
        /// </summary>
        private ILogger Logger { get; }

        /// <summary>
        /// Sets up the service.
        /// </summary>
        /// <param name="loggerFactory">Fasctory to create loggers from.</param>
        /// <param name="passwordHashingService">Provides hashing features.</param>
        /// <param name="userRepository">Repository for user data.</param>
        public UserService(ILoggerFactory loggerFactory, PasswordHashingService passwordHashingService, IUserRepository userRepository)
        {
            Logger = loggerFactory.CreateLogger<UserService>();
            PasswordHashingService = passwordHashingService;
            UserRepository = userRepository;
        }

        /// <summary>
        /// Gets all available users.
        /// </summary>
        /// <returns>Returns a list of users.</returns>
        public IEnumerable<User> GetAllUsers()
        {
            return UserRepository.GetAllUsers();
        }

        /// <summary>
        /// Gets a user by his unique id, which is his email.
        /// </summary>
        /// <param name="email">Email of the user to get.</param>
        /// <returns>Returns the user if found.</returns>
        /// <exception cref="EntityNotFoundException">Thrown if there is no user with the given email.</exception>
        public User GetUser(string email)
        {
            return GetUserOrThrowNotFoundException(email);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="email">An email for the user to create.</param>
        /// <param name="name">A name for the user to create.</param>
        /// <param name="password">A password for the user.</param>
        /// <returns>Returns the newly created user.</returns>
        /// <exception cref="EntityAlreadyExsistsException">Thrown if the email is already taken.</exception>
        public User CreateUser(string email, string name, string password)
        {
            if (UserRepository.GetUser(email) != null)
            {
                throw new EntityAlreadyExsistsException("User", email);
            }

            // Hash & salt password, create user!
            (string hash, byte[] salt) = PasswordHashingService.HashAndSaltPassword(password);
            return UserRepository.CreateUser(email, name, hash, salt);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="email">The email of the user to update.</param>
        /// <param name="name">The new user name to set.</param>
        /// <returns>Returns the modified user model.</returns>
        /// <exception cref="EntityNotFoundException">Thrown if there is no such user to update.</exception>
        public User UpdateUser(string email, string name)
        {
            User user = GetUserOrThrowNotFoundException(email);

            user.Name = name;
            UserRepository.UpdateUser(user);

            return user;
        }

        /// <summary>
        /// Attempts to change a user's password
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="oldPassword">The old password for verification.</param>
        /// <param name="newPassword">The new password to save.</param>
        /// <exception cref="EntityNotFoundException">Thrown if there is no such user.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown if the submitted old password is wrong!</exception>
        public void ChangePassword(string email, string oldPassword, string newPassword)
        {
            User user = GetUserOrThrowNotFoundException(email);

            // Verify old password
            if (user.Password != PasswordHashingService.HashAndSaltPassword(oldPassword, user.Salt))
            {
                throw new UnauthorizedAccessException();
            }

            // Hash and salt new password
            (string hashedPassword, byte[] salt) = PasswordHashingService.HashAndSaltPassword(newPassword);
            user.Password = hashedPassword;
            user.Salt = salt;
            UserRepository.UpdateUser(user);
        }

        #region Private Helpers

        /// <summary>
        /// Attempts to get a user from the underlying repository and throws a <see cref="EntityNotFoundException"/> if no matching user could be found.
        /// </summary>
        /// <param name="email">Email of the user to get.</param>
        /// <exception cref="EntityNotFoundException">Thrown if no matching user could be found.</exception>
        /// <returns>Returns the user, if found.</returns>
        private User GetUserOrThrowNotFoundException(string email)
        {
            User user = UserRepository.GetUser(email);

            // Check for user existence
            if (user == null)
            {
                throw new EntityNotFoundException("User", email);
            }

            return user;
        }

        #endregion
    }
}

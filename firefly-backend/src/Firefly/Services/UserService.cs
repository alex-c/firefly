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
        /// Gets users, optionally filtered using a partial name.
        /// </summary>
        /// <param name="partialName">Partial name to search for.</param>
        /// <returns>Returns a list of matching users.</returns>
        public IEnumerable<User> GetUsers(string partialName = null)
        {
            if (string.IsNullOrWhiteSpace(partialName))
            {
                return UserRepository.GetAllUsers();
            }
            else
            {
                return UserRepository.SearchUsersByName(partialName);
            }
        }

        /// <summary>
        /// Gets a user by his unique id.
        /// </summary>
        /// <param name="id">ID of the user to get.</param>
        /// <returns>Returns the user if found.</returns>
        /// <exception cref="EntityNotFoundException">Thrown if there is no user with the given id.</exception>
        public User GetUser(string id)
        {
            return GetUserOrThrowNotFoundException(id);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="id">An ID for the user to create.</param>
        /// <param name="name">A name for the user to create.</param>
        /// <param name="password">A password for the user.</param>
        /// <param name="isAdmin">Whether the user is an admin. Defaults to false.</param>
        /// <returns>Returns the newly created user.</returns>
        /// <exception cref="EntityAlreadyExsistsException">Thrown if the ID is already taken.</exception>
        public User CreateUser(string id, string name, string password, bool isAdmin = false)
        {
            if (UserRepository.GetUser(id) != null)
            {
                throw new EntityAlreadyExsistsException("User", id);
            }

            // Hash & salt password, create user!
            (string hash, byte[] salt) = PasswordHashingService.HashAndSaltPassword(password);
            return UserRepository.CreateUser(id, name, hash, salt, isAdmin);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="name">The new user name to set.</param>
        /// <param name="isAdmin">Whether the user is an admin.</param>
        /// <returns>Returns the modified user model.</returns>
        /// <exception cref="EntityNotFoundException">Thrown if there is no such user to update.</exception>
        public User UpdateUser(string id, string name, bool isAdmin)
        {
            User user = GetUserOrThrowNotFoundException(id);

            user.Name = name;
            user.IsAdmin = isAdmin;
            UserRepository.UpdateUser(user);

            return user;
        }

        /// <summary>
        /// Attempts to change a user's password
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <param name="oldPassword">The old password for verification.</param>
        /// <param name="newPassword">The new password to save.</param>
        /// <exception cref="EntityNotFoundException">Thrown if there is no such user.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown if the submitted old password is wrong!</exception>
        public void ChangePassword(string id, string oldPassword, string newPassword)
        {
            User user = GetUserOrThrowNotFoundException(id);

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
        /// <param name="id">ID of the user to get.</param>
        /// <exception cref="EntityNotFoundException">Thrown if no matching user could be found.</exception>
        /// <returns>Returns the user, if found.</returns>
        private User GetUserOrThrowNotFoundException(string id)
        {
            User user = UserRepository.GetUser(id);

            // Check for user existence
            if (user == null)
            {
                throw new EntityNotFoundException("User", id);
            }

            return user;
        }

        #endregion
    }
}

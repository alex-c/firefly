﻿using Firefly.Models;
using System.Collections.Generic;

namespace Firefly.Repositories
{
    /// <summary>
    /// A user repository allows to get/create/update/delete users.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>Returns all users.</returns>
        IEnumerable<User> GetAllUsers();

        /// <summary>
        /// Searches users by name using a partial name.
        /// </summary>
        /// <param name="partialName">Partial name to search for.</param>
        /// <returns>Returns a list of matching users.</returns>
        IEnumerable<User> SearchUsersByName(string partialName);

        /// <summary>
        /// Gets a user by his unique user ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>Returns the user, or null if no matching user was found.</returns>
        User GetUser(string id);

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="id">The ID of the user to create.</param>
        /// <param name="name">The display name of the user to create.</param>
        /// <param name="password">The password of the user to create.</param>
        /// <param name="salt">The salt used to had the user's password.</param>
        /// <param name="isAdmin">Whether the user is an admin.</param>
        /// <returns>Returns the newly created user.</returns>
        User CreateUser(string id, string name, string password, byte[] salt, bool isAdmin);

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="user">The user to update.</param>
        void UpdateUser(User user);

        /// <summary>
        /// Deletes a user, identified by his unique ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        void DeleteUser(string id);
    }
}

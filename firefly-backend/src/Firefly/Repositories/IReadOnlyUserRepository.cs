using Firefly.Models;
using System.Collections.Generic;

namespace Firefly.Repositories
{
    /// <summary>
    /// A read-only user repository allows to get users.
    /// </summary>
    public interface IReadOnlyUserRepository
    {
        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>Returns all users.</returns>
        IEnumerable<User> GetAllUsers();

        /// <summary>
        /// Gets a user by his Email, which is his unique user ID.
        /// </summary>
        /// <param name="email">The Email (ID) of the user.</param>
        /// <returns>Returns the user, or null if no matching user was found.</returns>
        User GetUser(string email);
    }
}

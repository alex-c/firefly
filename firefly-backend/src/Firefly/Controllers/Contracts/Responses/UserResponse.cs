using Firefly.Models;

namespace Firefly.Controllers.Contracts.Responses
{
    /// <summary>
    /// A generic user contract for responses containing user data.
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// Creates an instance from a user model.
        /// </summary>
        /// <param name="user">The user model for which to create an instance.</param>
        public UserResponse(User user)
        {
            Id = user.Id;
            Name = user.Name;
            IsAdmin = user.IsAdmin;
        }

        /// <summary>
        /// The user's unique ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The user's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Whether the user is an admin.
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}

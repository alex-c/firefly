namespace Firefly.Controllers.Contracts.Request
{
    /// <summary>
    /// A request to create a new user.
    /// </summary>
    public class UserCreationRequest
    {
        /// <summary>
        /// A unique user ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// A user name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A user password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Whether the user to create is an admin.
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}

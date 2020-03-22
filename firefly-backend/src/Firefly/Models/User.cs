namespace Firefly.Models
{
    /// <summary>
    /// A Firefly user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// The user's Email, which is hes unique login ID.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The user's name as it will be displayed in the UI.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The user's hashed password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The salt used to hash this user's password.
        /// </summary>
        public byte[] Salt { get; set; }

        /// <summary>
        /// Whether the user is an admin.
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}

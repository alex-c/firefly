using System;

namespace Firefly.Models
{
    /// <summary>
    /// Represents a user password reset link.
    /// </summary>
    public class PasswordResetLink
    {
        /// <summary>
        /// ID of the link.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ID of the user this link is for.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Expiration date and time of this link.
        /// </summary>
        public DateTime Expires { get; set; }
    }
}

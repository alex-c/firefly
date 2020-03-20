namespace Firefly.Controllers.Contracts.Request
{
    /// <summary>
    /// A request contract for user logins.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Email of the user to login.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password of the user.
        /// </summary>
        public string Password { get; set; }
    }
}

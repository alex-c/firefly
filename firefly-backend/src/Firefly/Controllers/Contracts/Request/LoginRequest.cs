namespace Firefly.Controllers.Contracts.Request
{
    /// <summary>
    /// A request contract for user logins.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// ID of the user to login.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Password of the user.
        /// </summary>
        public string Password { get; set; }
    }
}

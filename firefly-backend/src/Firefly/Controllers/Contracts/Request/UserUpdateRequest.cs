namespace Firefly.Controllers.Contracts.Request
{
    /// <summary>
    /// A request to update an user.
    /// </summary>
    public class UserUpdateRequest
    {
        /// <summary>
        /// A user name to set.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Whether the user is an admin.
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}

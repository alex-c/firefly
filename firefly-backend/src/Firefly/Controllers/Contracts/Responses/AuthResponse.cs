namespace Firefly.Controllers.Contracts.Responses
{
    /// <summary>
    /// A response to a successful authentication.
    /// </summary>
    public class AuthResponse
    {
        /// <summary>
        /// JWT which will allow the authenticated client to access private API routes.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Creates a response to a successful authentication request.
        /// </summary>
        /// <param name="token">The JWT to deliver.</param>
        public AuthResponse(string token)
        {
            Token = token;
        }
    }
}

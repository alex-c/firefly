namespace Firefly.Controllers.Contracts.Responses
{
    /// <summary>
    /// A generic client error response object (for use with status codes 4xx).
    /// </summary>
    public class ClientErrorResponse
    {
        /// <summary>
        /// The error message to pass along.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Creates a client error response with a message.
        /// </summary>
        /// <param name="message">The message to pass along.</param>
        public ClientErrorResponse(string message)
        {
            Message = message;
        }
    }
}

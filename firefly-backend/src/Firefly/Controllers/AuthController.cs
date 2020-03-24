using Firefly.Controllers.Contracts.Request;
using Firefly.Controllers.Contracts.Responses;
using Firefly.Services;
using Firefly.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Firefly.Controllers
{
    /// <summary>
    /// API for user authentication.
    /// </summary>
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// The service providing authentication functionality.
        /// </summary>
        private AuthService AuthService { get; }

        /// <summary>
        /// Initializes a controller instance.
        /// </summary>
        /// <param name="loggerFactory">Factory to create loggers from.</param>
        /// <param name="authService">Injected authentication service.</param>
        public AuthController(ILogger<AuthController> logger, AuthService authService)
        {
            Logger = logger;
            AuthService = authService;
        }

        /// <summary>
        /// Login: Authenticates a user.
        /// </summary>
        /// <param name="loginRequest">User login request.</param>
        /// <returns>Returns a JWT on success.</returns>
        [HttpPost]
        public IActionResult AuthenticateUser([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null || loginRequest.Id == null || loginRequest.Password == null)
            {
                return HandleBadRequest("A user ID and password need to be supplied for login requests.");
            }

            try
            {
                if (AuthService.TryAuthenticate(loginRequest.Id, loginRequest.Password, out string token))
                {
                    return Ok(new AuthResponse(token));
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (EntityNotFoundException)
            {
                return Unauthorized();
            }
            catch (Exception exception)
            {
                return HandleUnexpectedException(exception);
            }
        }
    }
}

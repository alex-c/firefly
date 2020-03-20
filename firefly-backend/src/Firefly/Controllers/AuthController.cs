using Firefly.Controllers.Contracts.Request;
using Firefly.Controllers.Contracts.Responses;
using Firefly.Models;
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
        public AuthController(ILoggerFactory loggerFactory, AuthService authService)
        {
            Logger = loggerFactory.CreateLogger<AuthController>();
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
            if (loginRequest == null || loginRequest.Email == null || loginRequest.Password == null)
            {
                return HandleBadRequest("A login name and password should be supplied for login requests.");
            }

            try
            {
                if (AuthService.TryAuthenticate(loginRequest.Email, loginRequest.Password, out string token))
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

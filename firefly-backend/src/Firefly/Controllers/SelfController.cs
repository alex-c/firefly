using Firefly.Controllers.Contracts.Request;
using Firefly.Services;
using Firefly.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Firefly.Controllers
{
    /// <summary>
    /// Functionality concerning the requesting user himself.
    /// </summary>
    [Route("api/self"), Authorize]
    public class SelfController : ControllerBase
    {
        /// <summary>
        /// The service providing user-related CRUD functionality.
        /// </summary>
        private UserService UserService { get; }

        /// <summary>
        /// Initializes the controller with all needed components.
        /// </summary>
        /// <param name="loggerFactory">Factory to create loggers from.</param>
        /// <param name="userService">Service providing user-related functionality.</param>
        public SelfController(ILoggerFactory loggerFactory, UserService userService)
        {
            Logger = loggerFactory.CreateLogger<SelfController>();
            UserService = userService;
        }

        /// <summary>
        /// Lets a user change his password.
        /// </summary>
        /// <param name="passwordChangeRequest">The request contract.</param>
        /// <returns>Returns an empty 200 Ok response on success.</returns>
        [HttpPost("password")]
        public IActionResult ChangePassword([FromBody] PasswordChangeRequest passwordChangeRequest)
        {
            if (passwordChangeRequest == null ||
                string.IsNullOrWhiteSpace(passwordChangeRequest.Old) ||
                string.IsNullOrWhiteSpace(passwordChangeRequest.New))
            {
                return HandleBadRequest("The old user password and a new password need to be supplied.");
            }

            try
            {
                string id = GetSubject();
                UserService.ChangePassword(id, passwordChangeRequest.Old, passwordChangeRequest.New);
                return NoContent();
            }
            catch (EntityNotFoundException exception)
            {
                return HandleUnexpectedException(exception);
            }
            catch (UnauthorizedAccessException)
            {
                return HandleBadRequest("The submitted password is wrong.");
            }
            catch (Exception exception)
            {
                return HandleUnexpectedException(exception);
            }
        }
    }
}

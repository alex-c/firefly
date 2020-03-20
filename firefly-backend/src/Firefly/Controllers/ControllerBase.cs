using Firefly.Controllers.Contracts.Responses;
using Firefly.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;

namespace Firefly.Controllers
{
    /// <summary>
    /// Base class for controllers, that helps to handle logging and errors in a unified way.
    /// </summary>
    public class ControllerBase : Controller
    {
        /// <summary>
        /// Logger for controller-level logging.
        /// </summary>
        protected ILogger Logger { get; set; }

        /// <summary>
        /// Get's the URI of a newly created resource, to be included in `201 Created` responses.
        /// </summary>
        /// <param name="resourceId">The ID of the newly created resource.</param>
        /// <returns>Returns the newly created resource's URI.</returns>
        protected Uri GetNewResourceUri<T>(T resourceId)
        {
            return new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{resourceId}");
        }

        /// <summary>
        /// Get's the request subject's ID as pasrsed from the JWT, which is the user ID for users.
        /// </summary>
        /// <returns>Returns the subject's ID.</returns>
        /// <exception cref="Exception">Thrown if no valid subject ID was found.</exception>
        protected string GetSubject()
        {
            Claim subject = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            if (subject == null)
            {
                throw new Exception("No subject available.");
            }
            return subject.Value;
        }

        #region Error Handling

        /// <summary>
        /// Handle bad requests.
        /// </summary>
        /// <param name="message">Message that should explain why the request is bad!</param>
        /// <returns>Returns a 400 error.</returns>
        protected IActionResult HandleBadRequest(string message)
        {
            return BadRequest(new ClientErrorResponse(message));
        }

        /// <summary>
        /// Handle "resource not found"-type of exceptions
        /// </summary>
        /// <param name="exception">The actual exception.</param>
        /// <returns>Returns a 404 error.</returns>
        protected IActionResult HandleResourceNotFoundException(EntityNotFoundException exception)
        {
            return NotFound(new ClientErrorResponse(exception.Message));
        }

        /// <summary>
        /// Handle "resource already exists"-type of exceptions.
        /// </summary>
        /// <param name="exception">The actual exception.</param>
        /// <returns>Returns a 409 error.</returns>
        protected IActionResult HandleResourceAlreadyExistsException(EntityAlreadyExsistsException exception)
        {
            return Conflict(new ClientErrorResponse(exception.Message));
        }

        /// <summary>
        /// Handle unexpected exceptions.
        /// </summary>
        /// <param name="exception">Unexpected exception that was caught.</param>
        /// <returns>Returns a 500 error.</returns>
        protected IActionResult HandleUnexpectedException(Exception exception)
        {
            Logger?.LogError(exception, "An unexpected exception was caught.");
            return new StatusCodeResult(500);
        }

        /// <summary>
        /// Handle unexpected exceptions with extra message.
        /// </summary>
        /// <param name="exception">Unexpected exception that was caught.</param>
        /// <param name="message">Extra message explaining the problem.</param>
        /// <returns>Returns a 500 error.</returns>
        protected IActionResult HandleUnexpectedException(Exception exception, string message)
        {
            Logger?.LogError(exception, message);
            return new StatusCodeResult(500);
        }

        #endregion
    }
}

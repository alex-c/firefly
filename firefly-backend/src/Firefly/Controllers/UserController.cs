using Firefly.Controllers.Contracts.Request;
using Firefly.Controllers.Contracts.Responses;
using Firefly.Models;
using Firefly.Services;
using Firefly.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Firefly.Controllers
{
    /// <summary>
    /// API route for user data.
    /// </summary>
    [Route("api/users"), Authorize]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// The service providing user-related CRUD functionality.
        /// </summary>
        private UserService UserService { get; }

        /// <summary>
        /// A service providing auth-related functionality.
        /// </summary>
        private AuthService AuthService { get; }

        /// <summary>
        /// Initializes the controller with all needed components.
        /// </summary>
        /// <param name="loggerFactory">Factory to create loggers from.</param>
        /// <param name="userService">Service providing user-related functionality.</param>
        /// <param name="authService">Service providing auth functionality.</param>
        public UserController(ILogger<UserController> logger, UserService userService, AuthService authService)
        {
            Logger = logger;
            UserService = userService;
            AuthService = authService;
        }

        #region Public getters

        /// <summary>
        /// Gets available users.
        /// </summary>
        /// <param name="page">Requested page of data.</param>
        /// <param name="elementsPerPage">Number of elements requested.</param>
        /// <param name="search">Optional partial name to filter users with.</param>
        /// <returns>Returns a paginated list of users.</returns>
        [HttpGet]
        public IActionResult GetUsers([FromQuery] int page = 1, [FromQuery] int elementsPerPage = 10, [FromQuery] string search = null)
        {
            try
            {
                IEnumerable<User> users = UserService.GetUsers(search);
                IEnumerable<User> paginatedUsers = users.Skip((page - 1) * elementsPerPage).Take(elementsPerPage);
                return Ok(new PaginatedResponse(paginatedUsers.Select(u => new UserResponse(u)), users.Count()));
            }
            catch (Exception exception)
            {
                return HandleUnexpectedException(exception);
            }
        }

        /// <summary>
        /// Gets a specific user by his unique ID.
        /// </summary>
        /// <param name="id">ID of the user to get.</param>
        /// <returns>Returns the user, if found.</returns>
        [HttpGet("{id}")]
        public IActionResult GetUser(string id)
        {
            try
            {
                User user = UserService.GetUser(id);
                return Ok(new UserResponse(user));
            }
            catch (EntityNotFoundException exception)
            {
                return HandleResourceNotFoundException(exception);
            }
            catch (Exception exception)
            {
                return HandleUnexpectedException(exception);
            }
        }

        #endregion

        #region Administrative features

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userCreationRequest">The request contract.</param>
        /// <returns>Returns the newly created user.</returns>
        [HttpPost, Authorize(Roles = AuthService.ROLE_ADMINISTRATOR)]
        public IActionResult CreateUser([FromBody] UserCreationRequest userCreationRequest)
        {
            if (userCreationRequest == null ||
                string.IsNullOrWhiteSpace(userCreationRequest.Id) ||
                string.IsNullOrWhiteSpace(userCreationRequest.Name) ||
                string.IsNullOrWhiteSpace(userCreationRequest.Password))
            {
                return HandleBadRequest("A valid user ID, name, password and role need to be provided.");
            }

            // Attempt to create the new user
            try
            {
                User user = UserService.CreateUser(userCreationRequest.Id, userCreationRequest.Name, userCreationRequest.Password, userCreationRequest.IsAdmin);
                return Created(GetNewResourceUri(user.Id), new UserResponse(user));
            }
            catch (EntityAlreadyExsistsException exception)
            {
                return HandleResourceAlreadyExistsException(exception);
            }
            catch (Exception exception)
            {
                return HandleUnexpectedException(exception);
            }
        }

        /// <summary>
        /// Updates an existing user's data.
        /// </summary>
        /// <param name="id">ID of the user to update.</param>
        /// <param name="userUpdateRequest">Request contract with data to update.</param>
        /// <returns>Returns the modified user.</returns>
        [HttpPatch("{id}"), Authorize(Roles = AuthService.ROLE_ADMINISTRATOR)]
        public IActionResult UpdateUser(string id, [FromBody] UserUpdateRequest userUpdateRequest)
        {
            if (userUpdateRequest == null || string.IsNullOrWhiteSpace(userUpdateRequest.Name))
            {
                return HandleBadRequest("A valid user name needs to be provided.");
            }

            // Attempt to update the user
            try
            {
                User user = UserService.UpdateUser(id, userUpdateRequest.Name, userUpdateRequest.IsAdmin);
                return Ok(new UserResponse(user));
            }
            catch (EntityNotFoundException exception)
            {
                return HandleResourceNotFoundException(exception);
            }
            catch (Exception exception)
            {
                return HandleUnexpectedException(exception);
            }
        }

        /// <summary>
        /// Generates a password reset link for a given user.
        /// </summary>
        /// <param name="id">ID of the user to generate a link for.</param>
        /// <returns>Returns the newly generated link.</returns>
        [HttpPost("{id}/reset"), Authorize(Roles = AuthService.ROLE_ADMINISTRATOR)]
        public IActionResult GeneratePasswordResetLink(string id)
        {
            try
            {
                User user = UserService.GetUser(id);
                PasswordResetLink link = AuthService.GeneratePasswordResetLink(user);
                return Ok(link);
            }
            catch (EntityNotFoundException exception)
            {
                return HandleResourceNotFoundException(exception);
            }
            catch (Exception exception)
            {
                return HandleUnexpectedException(exception);
            }
        }

        #endregion
    }
}

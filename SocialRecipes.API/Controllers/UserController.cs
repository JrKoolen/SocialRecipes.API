using Microsoft.AspNetCore.Mvc;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;
using SocialRecipes.Services.Services;

namespace SocialRecipes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserService _userService;

        public UserController(ILogger<UserController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("GetTotalUsers")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTotalUsers()
        {

            try
            {
                _logger.LogInformation("Attempting to retrieve all users.");
                int userCount = await _userService.GetTotalUsersAsync();
                _logger.LogInformation("Successfully retrieved all users.");
                return Ok(userCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while attempting to retrieve all users.");
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = "An unexpected error occurred while processing your request.",
                    Instance = "/users"
                });
            }
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>
        /// A success message if the user is deleted successfully, or an error message if the user does not exist.
        /// </returns>
        [HttpDelete("DeleteUser/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete user with ID {id}.");

                UserDto user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    _logger.LogWarning($"User with ID {id} not found.");
                    return NotFound(new ProblemDetails
                    {
                        Title = "User not found",
                        Status = StatusCodes.Status404NotFound,
                        Detail = "The user with the given ID does not exist.",
                        Instance = $"/users/{id}"
                    });
                }

                await _userService.DeleteUserByIdAsync(id);
                _logger.LogInformation($"Successfully deleted user with ID {id}.");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while attempting to delete user with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = "An unexpected error occurred while processing your request.",
                    Instance = $"/users/{id}"
                });
            }
        }
    }
}


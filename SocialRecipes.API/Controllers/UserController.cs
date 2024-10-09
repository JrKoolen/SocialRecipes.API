using Microsoft.AspNetCore.Mvc;
using SocialRecipes.Domain.IServices;
using SocialRecipes.DTO.General;
using SocialRecipes.DTO.IN;
using Microsoft.Extensions.Logging; // Don't forget the proper using directive

namespace SocialRecipes.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        // Injecting IUserService properly in the constructor
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("CreateUser")]
        public IActionResult CreateUser(AddUserDto user)
        {
            if (user == null)
            {
                _logger.LogError("User object sent from client is null.");
                return BadRequest("User is null.");
            }

            if (string.IsNullOrEmpty(user.Name))
            {
                _logger.LogError("User name is null or empty.");
                return BadRequest("User name is required.");
            }

            // Since _userService is now injected, no need to check for null
            _userService.CreateUser(user);
            _logger.LogInformation($"Creating a new user: {user.Name}");

            return Ok(new { message = "200", user });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            _logger.LogInformation($"User with ID {id} removed from database");

            UserDto user = _userService.GetUserById(id);
            if (user == null)
            {
                _logger.LogWarning($"User with ID {id} not found.");
                return NotFound(new { message = "User not found", id });
            }

            _userService.DeleteUserById(id);
            _logger.LogInformation($"User with ID {id} successfully deleted.");

            return Ok(new { message = "User deleted", id });
        }
    }
}

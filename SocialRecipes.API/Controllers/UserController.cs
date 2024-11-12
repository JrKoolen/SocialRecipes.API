using Microsoft.AspNetCore.Mvc;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;
using SocialRecipes.Services.Services;

namespace SocialRecipes.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserService _userService;

        public UserController(ILogger<UserController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _logger.LogInformation($"Delete user {id} ");

            UserDto user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning($"User not found: {id}");
                return NotFound(new { message = "User not found", id });
            }

            await _userService.DeleteUserByIdAsync(id);
            _logger.LogInformation($"User deleted {id} ");

            return Ok(new { message = "User deleted", id });
        }
    }
}

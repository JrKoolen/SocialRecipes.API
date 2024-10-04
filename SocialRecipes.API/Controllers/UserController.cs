using Microsoft.AspNetCore.Mvc;
using SocialRecipes.Domain.IServices;

namespace SocialRecipes.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpPost("CreateUser")]
        public IActionResult CreateUser([FromBody] SocialRecipes.DTO.IN.CreateUserDto user)
        {
            if (user == null)
            {
                _logger.LogError("User object sent from client is null.");
                return BadRequest("User is null.");
            }
            _logger.LogInformation($"Creating a new user: {user.Name}");
            return Ok(new { message = "200", user });
        }
    }
}
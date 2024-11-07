using Microsoft.AspNetCore.Mvc;
using SocialRecipes.Domain.IServices;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;

namespace SocialRecipes.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        // Moved the  logica of creating a user to authcontroller.
       //HttpPost("CreateUser")]
      //public async Task<IActionResult> CreateUser(AddUserDto user)
      //{
     //     if (user == null)
    //      {
   //           _logger.LogError("User object sent from client is null.");
   //           return BadRequest("User is null.");
   //       }
   //
   //       if (string.IsNullOrEmpty(user.Name))
   //       {
   //           _logger.LogError("User name is null or empty.");
   //           return BadRequest("User name is required.");
   //       }

   //       await _userService.CreateUserAsync(user);
   //       _logger.LogInformation($"Creating a new user: {user.Name}");

   //       return Ok(new { message = "200", user });
   //   }

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

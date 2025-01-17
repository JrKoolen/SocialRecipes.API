using SocialRecipes.Infrastructure.Settings;
using SocialRecipes.Domain.Dto.IN;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using SocialRecipes.Services.Services;
using SocialRecipes.Domain.Dto.General;
using System.Security.Authentication;
using Swashbuckle.AspNetCore.Filters;
using SocialRecipes.Domain.SwaggerExamples;


namespace SocialRecipes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly JwtSettings _jwtSettings;
        private readonly AuthService _authService;

        public AuthController(ILogger<AuthController> logger, IOptions<JwtSettings> jwtSettings, AuthService authService)
        {
            _logger = logger;
            _jwtSettings = jwtSettings.Value;
            _authService = authService;
        }

        /// <summary>
        /// Allows a user to login.
        /// </summary>
        /// <param name="login">the login requires a name and password.</param>
        /// <returns> returns a JWT token.</returns>
        [HttpPost("login")]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LoginResponseExample))]
        public async Task<IActionResult> LoginAsync([FromForm] LoginDto login)
        {
            try
            {
                _logger.LogInformation("Starting LoginAsync.");
                _logger.LogInformation("User details: {Name},", login.Name);
                if (login == null || string.IsNullOrWhiteSpace(login.Name) || string.IsNullOrWhiteSpace(login.Password))
                {
                    _logger.LogWarning("Invalid login credentials provided.");
                    return Unauthorized(new { message = "Invalid username or password." });
                }

                UserDto isValidUser = await _authService.LoginAsync(login);

                if (isValidUser is not null)
                {
                    var token = GenerateJwtToken(login.Name);
                    return Ok(new
                    {
                        message = "Login successful.",
                        token,
                        userId = isValidUser.Id
                    });
                }
                return Unauthorized(new { message = "Invalid username or password." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during login.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal server error. Please try again later." });
            }
        }

        /// <summary>
        /// Registers a new user with the provided details.
        /// </summary>
        /// <param name="addUser">The user details for registration.</param>
        /// <returns>
        /// A success message if the user is registered successfully,
        /// or an error message if the registration fails.
        /// </returns>
        [HttpPost("register")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterAsync([FromForm] AddUserDto addUser)
        {
            try
            {
                _logger.LogInformation("Starting user registration process.");
                if (addUser == null || string.IsNullOrWhiteSpace(addUser.Name) || string.IsNullOrWhiteSpace(addUser.Password) || string.IsNullOrWhiteSpace(addUser.Email))
                {
                    _logger.LogWarning("Invalid registration details provided.");
                    return BadRequest(new { message = "Invalid input data. Please provide all required fields." });
                }

                var result = await _authService.RegisterAsync(addUser);

                if (!result)
                {
                    _logger.LogWarning("User registration failed.");
                    return BadRequest(new { message = "User registration failed." });
                }
                else
                {
                    _logger.LogInformation("User registered successfully.");
                    return Ok(new { message = "User registered successfully." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during registration.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal server error. Please try again later." });
            }
        }


        private string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSecret());

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
            new Claim(ClaimTypes.Name, username)
        }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _jwtSettings.GetIssuer(),
                Audience = _jwtSettings.GetIssuer(),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
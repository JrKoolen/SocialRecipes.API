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

[ApiController]
[Route("[controller]")]
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
    /// Authenticates a user based on their username and password.
    /// </summary>
    /// <param name="login">The user's login credentials (username and password).</param>
    /// <returns>
    /// A JWT token if authentication is successful; otherwise, an unauthorized error.
    /// </returns>
    [HttpPost("login")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LoginAsync([FromForm] LoginDto login)
    {
        try
        {
            _logger.LogInformation("Starting LoginAsync.");

            if (login == null || string.IsNullOrWhiteSpace(login.Username) || string.IsNullOrWhiteSpace(login.Password))
            {
                _logger.LogWarning("Invalid login credentials provided.");
                return Unauthorized(new { message = "Invalid username or password." });
            }

            UserDto isValidUser = await _authService.LoginAsync(login);

            if (isValidUser is not null)
            {
                var token = GenerateJwtToken(login.Username);
                return Ok(new
                {
                    message = "Login successful.",
                    token,
                    userId = isValidUser.Id
                });
            }
            return Unauthorized(new { message = "Invalid username or password." });
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex, "Null argument passed during login.");
            return BadRequest(new { message = "Required data is missing. Please provide all required fields." });
        }
        catch (AuthenticationException ex)
        {
            _logger.LogError(ex, "Authentication error occurred.");
            return Unauthorized(new { message = "Authentication failed. Please check your credentials." });
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

            if (addUser == null || string.IsNullOrWhiteSpace(addUser.Name) || string.IsNullOrWhiteSpace(addUser.Password))
            {
                _logger.LogWarning("Invalid registration details provided.");
                return BadRequest(new { message = "Invalid input data. Please provide all required fields." });
            }

            string response = await _authService.RegisterAsync(addUser);

            if (response.Contains("User created succesfully"))
            {
                _logger.LogInformation($"User {addUser.Name} registered successfully.");
                return Ok(new
                {
                    message = response,
                    username = addUser.Name
                });
            }
            return BadRequest(new { message = response });
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Invalid argument provided during registration.");
            return BadRequest(new { message = "Invalid registration details. Please check your input." });
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
        var key = Encoding.UTF8.GetBytes(_jwtSettings.GetIssuer());

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

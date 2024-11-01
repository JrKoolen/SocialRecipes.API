using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SocialRecipes.DTO.IN;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SocialRecipes.Infrastructure.Settings; 

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthService _authService;
    private readonly JwtSettings _jwtSettings;

    public AuthController(ILogger<AuthController> logger, IAuthService authService, IOptions<JwtSettings> jwtSettings)
    {
        _logger = logger;
        _authService = authService;
        _jwtSettings = jwtSettings.Value; 
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto login)
    {
        var isValidUser = _authService.Login(login);
        if (isValidUser)
        {
            var token = GenerateJwtToken(login.Username);
            return Ok(new { token });
        }

        return Unauthorized("Invalid username or password.");
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] AddUserDto addUser)
    {
        var isRegistered = _authService.Register(addUser);
        if (isRegistered)
        {
            return Ok("User registered successfully.");
        }

        return BadRequest("Username already exists or registration failed.");
    }

    private string GenerateJwtToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, username)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _jwtSettings.Issuer,           
            Audience = _jwtSettings.Audience,       
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

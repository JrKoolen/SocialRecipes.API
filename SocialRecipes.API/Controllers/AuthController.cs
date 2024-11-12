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

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto login)
    {
        UserDto isValidUser = await _authService.LoginAsync(login);
        if (isValidUser is not null)
        {
            var token = GenerateJwtToken(login.Username);
            return Ok(new { token, id  = isValidUser.Id });
        }

        return Unauthorized("Invalid username or password.");
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] AddUserDto addUser)
    {
        Console.WriteLine(addUser.Name);
        var isRegistered = await _authService.RegisterAsync(addUser);
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

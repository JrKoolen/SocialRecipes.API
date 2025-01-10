using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialRecipes.DAL.Repositories;
using SocialRecipes.Services.IRepositories;
using SocialRecipes.Infrastructure.Settings;
using SocialRecipes.Services.Services;
using System.Text;
using SocialRecipes.DAL;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Swashbuckle.AspNetCore.Filters;
using SocialRecipes.Domain.Dto.ExampleResponse;
using SocialRecipes.Domain.SwaggerExamples;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
Settings settings = new Settings();
builder.Configuration.AddEnvironmentVariables();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

if (jwtSettings == null)
{
    jwtSettings = settings.GetJwtSettings();
    Console.WriteLine("JwtSettings were missing in the configuration. Using fallback values.");
}

builder.Services.AddSingleton(jwtSettings);

var connstring = configuration.GetConnectionString("DefaultConnection");
if (connstring == null)
{
    connstring = settings.GetConnectionString();
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connstring,
        new MySqlServerVersion(new Version(8, 0, 21)),
        mysqlOptions => mysqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null
        )));

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<RecipeService>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<FollowService>();
builder.Services.AddScoped<IFollowerRepository, FollowerRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<CommentService>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.GetIssuer(),
        ValidAudience = jwtSettings.GetIssuer(),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSecret()))
    };
});

builder.Services.AddCors(options =>
{
options.AddPolicy("AllowFrontend", policy =>
{
    policy.WithOrigins("http://localhost:3000", "http://localhost:3001", "http://socialrecipesadmin-container:3001", "http://localhost:8081/")
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials();
});
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.ExampleFilters();
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<LoginResponseExample>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate(); 
}



var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Application configuration started.");

app.UseCors("AllowFrontend");
app.UseSwagger();
app.UseSwaggerUI();
logger.LogInformation("Swagger UI enabled.");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
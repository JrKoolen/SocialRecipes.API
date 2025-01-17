using System;
using System.Security.Cryptography.X509Certificates;
using DotNetEnv;

namespace SocialRecipes.Infrastructure.Settings
{
    public class Settings
    {
        private readonly string _connectionString;
        private readonly JwtSettings jwtSettings;
        public Settings(string env = "development")
        {
            string workingDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine($"{workingDirectory}");

            string envFileName = Path.Combine(workingDirectory, $".env.{env}");
            Console.WriteLine($"{envFileName}");

            if (File.Exists(envFileName))
            {
                DotNetEnv.Env.Load(envFileName);
                Console.WriteLine($"{envFileName}");
            }

            _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? "Server=socialrecipedb.mysql.database.azure.com; Port=3306; Database=socialrecipesdb; Uid=JKadmin; Pwd=@R3x!9Tq#Lz@1pV!;";
            string jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? "T8x!g5#Lk92z@Q7P$G1%XcMZ5L!7DfNlR";
            string jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "http://localhost";
            string jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "http://localhost";

            jwtSettings = new JwtSettings(
                jwtSecret,
                jwtIssuer,
                jwtAudience
            );
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }

        public JwtSettings GetJwtSettings()
        {
            return jwtSettings;
        }
    }
}

    // "Server=mysql-db;Database=socialrecipesdb;User Id=root;Password=rootpassword;"
    // "Server=socialrecipedb.mysql.database.azure.com; Port=3306; Database=socialrecipesdb; Uid=JKadmin; Pwd=@R3x!9Tq#Lz@1pV!;";


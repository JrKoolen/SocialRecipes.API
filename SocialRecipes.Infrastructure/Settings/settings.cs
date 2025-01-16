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
            string envFileName = $".env.{env}";
            DotNetEnv.Env.Load(envFileName);

            _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            jwtSettings = new JwtSettings(
                 Environment.GetEnvironmentVariable("JWT_SECRET"),
                 Environment.GetEnvironmentVariable("JWT_ISSUER"),
                 Environment.GetEnvironmentVariable("JWT_AUDIENCE") 
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


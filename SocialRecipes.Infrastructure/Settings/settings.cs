using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using SocialRecipes.Infrastructure.Settings;

namespace SocialRecipes.Infrastructure.Settings
{
    public class Settings
    {
        private readonly string _connectionString = "Server=mysql-db;Database=socialrecipesdb;User Id=root;Password=rootpassword;";

        private JwtSettings jwtSettings;

        public Settings()
        {
            jwtSettings = new JwtSettings();

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

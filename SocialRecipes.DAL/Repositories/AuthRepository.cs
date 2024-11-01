using SocialRecipes.Services.IRepositories;
using SocialRecipes.DTO.IN;
using MySql.Data.MySqlClient;
using SocialRecipes.DTO.General;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace SocialRecipes.DAL.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly string _connectionString;

        public AuthRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Login(LoginDto loginDto)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand("SELECT Password FROM Users WHERE Username = @username", connection);
            command.Parameters.AddWithValue("@username", loginDto.Username);

            var dbPasswordHash = command.ExecuteScalar()?.ToString();
            if (dbPasswordHash == null)
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(loginDto.Password, dbPasswordHash);
        }

        public bool Register(AddUserDto addUserDto)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var checkUserCommand = new MySqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @username", connection);
            checkUserCommand.Parameters.AddWithValue("@username", addUserDto.Name);

            var userExists = Convert.ToInt32(checkUserCommand.ExecuteScalar()) > 0;
            if (userExists)
                return false; 

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(addUserDto.Password);

            var insertCommand = new MySqlCommand("INSERT INTO Users (Username, Password) VALUES (@username, @password)", connection);
            insertCommand.Parameters.AddWithValue("@username", addUserDto.Name);
            insertCommand.Parameters.AddWithValue("@password", hashedPassword);

            return insertCommand.ExecuteNonQuery() > 0;
        }
    }
}

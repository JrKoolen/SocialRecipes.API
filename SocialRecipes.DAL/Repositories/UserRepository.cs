using SocialRecipes.Domain.IRepositories;
using SocialRecipes.DAL.Dto.IN;
using MySql.Data.MySqlClient;

namespace SocialRecipes.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateUser(CreateUserDto user)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"INERT INTO User (Name, Email, Password, Age) VALUES (@Name, @Email, @Password, @Age)";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@User", user.Name);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Age", user.Age);
                }
            }
        }
    }
}

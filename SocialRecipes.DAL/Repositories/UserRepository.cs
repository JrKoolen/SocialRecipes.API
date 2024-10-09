using SocialRecipes.Domain.IRepositories;
using SocialRecipes.DTO.IN;
using MySql.Data.MySqlClient;
using SocialRecipes.DTO.General;
namespace SocialRecipes.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(AddUserDto user)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO User (Name, Email, Password, Age) VALUES (@Name, @Email, @Password, @Age)";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@User", user.Name);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Password", user.Password);
                }
            }
        }

        public void DeleteUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public UserDto GetUserById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

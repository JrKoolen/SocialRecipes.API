using SocialRecipes.Services.IRepositories;
using SocialRecipes.DTO.IN;
using MySql.Data.MySqlClient;
using SocialRecipes.DTO.General;

namespace SocialRecipes.DAL.Repositories
{
    public class FollowerRepository : IFollowerRepository
    {
        private readonly string _connectionString;

        public FollowerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public UserDto[] GetFollowers(int userId)
        {
            var followers = new List<UserDto>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT u.Id, u.Username, u.Email
                    FROM Follows f
                    JOIN Users u ON f.Following_User_Id = u.Id
                    WHERE f.Followed_User_Id = @FollowedUserId";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FollowedUserId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var userDto = new UserDto
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Username"),
                                Email = reader.GetString("Email")
                            };
                            followers.Add(userDto);
                        }
                    }
                }
            }

            return followers.ToArray();
        }

        public UserDto[] GetFollowing(int userId)
        {
            var following = new List<UserDto>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT u.Id, u.Username, u.Email
                    FROM Follows f
                    JOIN Users u ON f.Followed_User_Id = u.Id
                    WHERE f.Following_User_Id = @FollowingUserId";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FollowingUserId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var userDto = new UserDto
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Username"),
                                Email = reader.GetString("Email")
                            };
                            following.Add(userDto);
                        }
                    }
                }
            }

            return following.ToArray();
        }

        public void Follow(int userId, int followerId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    INSERT INTO Follows (Following_User_Id, Followed_User_Id, Created_At)
                    VALUES (@FollowerId, @UserId, NOW())";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FollowerId", followerId);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveFollow(int userId, int followerId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    DELETE FROM Follows
                    WHERE Following_User_Id = @FollowerId AND Followed_User_Id = @UserId";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FollowerId", followerId);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
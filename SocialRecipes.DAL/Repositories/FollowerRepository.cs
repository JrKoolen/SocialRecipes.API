using SocialRecipes.Domain.IRepositories;
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

        public void Follow(int userId, int followerId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO Followers (FollowerId, FollowingId, FollowingDate) VALUES (@FollowerId, @FollowingId, @FollowingDate)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FollowerId", followerId);
                    command.Parameters.AddWithValue("@FollowingId", userId);
                    command.Parameters.AddWithValue("@FollowingDate", DateTime.Now);
                    command.ExecuteNonQuery();
                }
            }
        }

        public FollowerDto GetFollowers(int userId)
        {
            var followerDto = new FollowerDto();
            var followerIds = new List<int>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
        SELECT u.Id, u.Username, u.Email
        FROM Follows f
        JOIN Users u ON f.Followed_User_Id = u.Id
        WHERE f.FollowingId = @FollowingId";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FollowingId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            followerIds.Add(reader.GetInt32("FollowerId"));
                        }
                    }
                }
            }
            followerDto.followed_user_id= followerIds.ToArray();
            return followerDto;
        }

        public FollowerDto GetFollowing(int userId)
        {
            var followerDto = new FollowerDto();
            var followingIds = new List<int>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"SELECT FollowingId FROM Follows WHERE FollowerId = @FollowerId";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FollowerId", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            followingIds.Add(reader.GetInt32("FollowingId"));
                        }
                    }
                }
            }

            followerDto.following_user_id = followingIds.ToArray();
            return followerDto;
        }

        public void RemoveFollow(int userId, int followerId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"DELETE FROM Follows WHERE FollowerId = @FollowerId AND FollowingId = @FollowingId";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FollowerId", followerId);
                    command.Parameters.AddWithValue("@FollowingId", userId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

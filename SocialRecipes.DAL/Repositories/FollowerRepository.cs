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

        public void Follow(int userid)
        {
            throw new NotImplementedException();
        }

        public FollowerDto GetFollowers(int userid)
        {
            throw new NotImplementedException();
        }

        public FollowerDto GetFollowing(int userid)
        {
            throw new NotImplementedException();
        }

        public void RemoveFollow(int userid)
        {
            throw new NotImplementedException();
        }
    }
}

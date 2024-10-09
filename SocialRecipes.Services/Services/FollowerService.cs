using SocialRecipes.Domain.IRepositories;
using SocialRecipes.Domain.IServices;
using SocialRecipes.DTO.General;
using SocialRecipes.DTO.IN;

namespace SocialRecipes.Services.Services
{
    public class FollowerService : IFollowerService
    {
        private readonly IFollowerRepository _followerRepository;

        public FollowerService(IFollowerRepository followerRepository)
        {
            _followerRepository = followerRepository;
        }

        public bool Follow(int userId, int followerId)
        {
            try
            {
                _followerRepository.Follow(userId, followerId);
                return true; 
            }
            catch
            {
                return false; 
            }
        }

        public FollowerDto GetFollowers(int userId)
        {
            return _followerRepository.GetFollowers(userId);
        }

        public FollowerDto GetFollowing(int userId)
        {
            return _followerRepository.GetFollowing(userId);
        }

        public bool RemoveFollow(int userId, int followerId)
        {
            try
            {
                _followerRepository.RemoveFollow(userId, followerId);
                return true; 
            }
            catch
            {
                return false; 
            }
        }
    }
}

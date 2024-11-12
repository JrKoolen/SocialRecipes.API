using SocialRecipes.Services.IRepositories;
using SocialRecipes.Domain.Dto.General;

namespace SocialRecipes.Services.Services
{
    public class FollowerService 
    {
        private readonly IFollowerRepository _followerRepository;

        public FollowerService(IFollowerRepository followerRepository)
        {
            _followerRepository = followerRepository;
        }

        public async Task<bool> FollowAsync(int userId, int followerId)
        {
            try
            {
                await _followerRepository.FollowAsync(userId, followerId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<UserDto[]> GetFollowersAsync(int userId)
        {
            return await _followerRepository.GetFollowersAsync(userId);
        }

        public async Task<UserDto[]> GetFollowingAsync(int userId)
        {
            return await _followerRepository.GetFollowingAsync(userId);
        }

        public async Task<bool> RemoveFollowAsync(int userId, int followerId)
        {
            try
            {
                await _followerRepository.RemoveFollowAsync(userId, followerId);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

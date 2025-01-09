using SocialRecipes.Services.IRepositories;
using SocialRecipes.Domain.Dto.General;
using Microsoft.Extensions.Logging;

namespace SocialRecipes.Services.Services
{
    public class FollowService
    {
        private readonly IFollowerRepository _followerRepository;

        public FollowService(IFollowerRepository followerRepository)
        {
            _followerRepository = followerRepository ?? throw new ArgumentNullException(nameof(followerRepository));
        }

        public async Task FollowAsync(int? userId, int? followerId)
        {
            await _followerRepository.FollowAsync(userId.Value, followerId.Value);
        }


        public async Task<UserDto[]> GetFollowersAsync(int userId)
        {
            var followers = await _followerRepository.GetFollowersAsync(userId);
            return followers ?? Array.Empty<UserDto>();
        }

        public async Task<UserDto[]> GetFollowingAsync(int userId)
        {
            var following = await _followerRepository.GetFollowingAsync(userId);
            return following ?? Array.Empty<UserDto>();
        }

        public async Task RemoveFollowAsync(int? userId, int? followerId)
        {

            await _followerRepository.RemoveFollowAsync(userId.Value, followerId.Value);
        }

    }
}

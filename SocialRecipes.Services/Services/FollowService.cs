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
            if (!userId.HasValue || !followerId.HasValue)
            {
                throw new ArgumentNullException("UserId or FollowerId cannot be null.");
            }

            if (userId <= 0 || followerId <= 0)
            {
                throw new ArgumentOutOfRangeException("UserId or FollowerId must be greater than zero.");
            }

            if (userId == followerId)
            {
                throw new InvalidOperationException("A user cannot follow themselves.");
            }

            await _followerRepository.FollowAsync(userId.Value, followerId.Value);
        }


        public async Task<UserDto[]> GetFollowersAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "UserId must be greater than zero.");
            }

            var followers = await _followerRepository.GetFollowersAsync(userId);
            return followers ?? Array.Empty<UserDto>();
        }

        public async Task<UserDto[]> GetFollowingAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "UserId must be greater than zero.");
            }

            var following = await _followerRepository.GetFollowingAsync(userId);
            return following ?? Array.Empty<UserDto>();
        }

        public async Task RemoveFollowAsync(int? userId, int? followerId)
        {
            if (!userId.HasValue || !followerId.HasValue)
            {
                throw new ArgumentNullException("UserId or FollowerId cannot be null.");
            }

            if (userId <= 0 || followerId <= 0)
            {
                throw new ArgumentOutOfRangeException("UserId and FollowerId must be greater than zero.");
            }

            if (userId == followerId)
            {
                throw new InvalidOperationException("A user cannot unfollow themselves.");
            }

            await _followerRepository.RemoveFollowAsync(userId.Value, followerId.Value);
        }

    }
}

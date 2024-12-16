using SocialRecipes.Services.IRepositories;
using SocialRecipes.Domain.Dto.General;
using Microsoft.Extensions.Logging;

namespace SocialRecipes.Services.Services
{
    public class FollowService
    {
        private readonly IFollowerRepository _followerRepository;
        private readonly ILogger<FollowService> _logger;

        public FollowService(IFollowerRepository followerRepository, ILogger<FollowService> logger)
        {
            _followerRepository = followerRepository ?? throw new ArgumentNullException(nameof(followerRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        // After coming back to the code after receiving feedback i did the following
        // Moved logging to the controller because its a form of output and should be done in the controller.
        // But if it is needed in the service layer it can be done.
        // So i removed logger from the injection.
        // I also removed the try catch block because it was to excessive and not needed
        // In some functions i always returned True and the function was async so i changed it to void.
        // Kept the Logger injection for now but should be removed.
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

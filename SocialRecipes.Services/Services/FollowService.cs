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

        public async Task<bool> FollowAsync(int? userId, int? followerId)
        {
            if (!userId.HasValue || !followerId.HasValue)
            {
                _logger.LogWarning("UserId or FollowerId is null.");
                throw new ArgumentNullException("UserId or FollowerId cannot be null.");
            }

            if (userId <= 0 || followerId <= 0)
            {
                _logger.LogWarning("UserId or FollowerId is invalid. Must be greater than zero.");
                throw new ArgumentOutOfRangeException("UserId or FollowerId must be greater than zero.");
            }

            if (userId == followerId)
            {
                _logger.LogWarning("A user cannot follow themselves. UserId: {UserId}", userId);
                throw new InvalidOperationException("A user cannot follow themselves.");
            }

            try
            {
                await _followerRepository.FollowAsync(userId.Value, followerId.Value);
                _logger.LogInformation("User {FollowerId} successfully followed User {UserId}.", followerId, userId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while attempting to follow User {UserId} by User {FollowerId}.", userId, followerId);
                throw; 
            }
        }

        public async Task<UserDto[]> GetFollowersAsync(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    _logger.LogWarning("Invalid UserId: {UserId}. Must be greater than zero.", userId);
                    throw new ArgumentOutOfRangeException(nameof(userId), "UserId must be greater than zero.");
                }

                var followers = await _followerRepository.GetFollowersAsync(userId);

                _logger.LogInformation("Successfully retrieved followers for UserId: {UserId}.", userId);
                return followers ?? Array.Empty<UserDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving followers for UserId: {UserId}.", userId);
                throw new ApplicationException("An error occurred while retrieving followers.", ex);
            }
        }

        public async Task<UserDto[]> GetFollowingAsync(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    _logger.LogWarning("Invalid UserId: {UserId}. Must be greater than zero.", userId);
                    throw new ArgumentOutOfRangeException(nameof(userId), "UserId must be greater than zero.");
                }

                var following = await _followerRepository.GetFollowingAsync(userId);

                _logger.LogInformation("Successfully retrieved following users for UserId: {UserId}.", userId);
                return following ?? Array.Empty<UserDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving following users for UserId: {UserId}.", userId);
                throw new ApplicationException("An error occurred while retrieving following users.", ex);
            }
        }

        public async Task<bool> RemoveFollowAsync(int? userId, int? followerId)
        {
            if (!userId.HasValue || !followerId.HasValue)
            {
                _logger.LogWarning("UserId or FollowerId is null.");
                throw new ArgumentNullException("UserId or FollowerId cannot be null.");
            }

            if (userId <= 0 || followerId <= 0)
            {
                _logger.LogWarning("UserId or FollowerId must be greater than zero. UserId: {UserId}, FollowerId: {FollowerId}", userId, followerId);
                throw new ArgumentOutOfRangeException("UserId and FollowerId must be greater than zero.");
            }

            try
            {
                await _followerRepository.RemoveFollowAsync(userId.Value, followerId.Value);
                _logger.LogInformation("User {FollowerId} successfully unfollowed User {UserId}.", followerId, userId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while attempting to remove follow for User {UserId} by User {FollowerId}.", userId, followerId);
                throw new ApplicationException("An error occurred while processing the unfollow operation.", ex);
            }
        }
    }
}

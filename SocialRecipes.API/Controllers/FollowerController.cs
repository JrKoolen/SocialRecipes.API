using SocialRecipes.Domain.IServices;
using Microsoft.AspNetCore.Mvc;

namespace SocialRecipes.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FollowController : Controller
    {
        private readonly ILogger<FollowController> _logger;
        private readonly IFollowerService _followerService;

        public FollowController(ILogger<FollowController> logger, IFollowerService followService)
        {
            _logger = logger;
            _followerService = followService;
        }

        [HttpPost("Follow/{userId}")]
        public async Task<IActionResult> FollowAsync(int userId, int followerId)
        {
            _logger.LogInformation($"User attempting to follow user with ID {userId}");

            var result = await _followerService.FollowAsync(userId, followerId);

            if (!result)
            {
                _logger.LogWarning($"Failed to follow user with ID {userId}");
                return BadRequest(new { message = "Failed to follow", userId });
            }

            _logger.LogInformation($"Successfully followed user with ID {userId}");
            return Ok(new { message = "Successfully followed", userId });
        }

        [HttpGet("GetFollowers/{userId}")]
        public async Task<IActionResult> GetFollowersAsync(int userId)
        {
            _logger.LogInformation($"Retrieving followers for user with ID {userId}");

            var followers = await _followerService.GetFollowersAsync(userId);
            if (followers == null || followers.Length == 0)
            {
                _logger.LogWarning($"No followers found for user with ID {userId}");
                return NotFound(new { message = "No followers found", userId });
            }

            return Ok(new { message = "Followers retrieved", followers });
        }

        [HttpGet("GetFollowing/{userId}")]
        public async Task<IActionResult> GetFollowingAsync(int userId)
        {
            _logger.LogInformation($"Retrieving following users for user with ID {userId}");

            var following = await _followerService.GetFollowingAsync(userId);
            if (following == null || following.Length == 0)
            {
                _logger.LogWarning($"User with ID {userId} is not following anyone.");
                return NotFound(new { message = "No following users found", userId });
            }

            return Ok(new { message = "Following users retrieved", following });
        }

        [HttpDelete("Unfollow/{userId}")]
        public async Task<IActionResult> RemoveFollowAsync(int userId, int followerId)
        {
            _logger.LogInformation($"User attempting to unfollow user with ID {userId}");

            var result = await _followerService.RemoveFollowAsync(userId, followerId);

            if (!result)
            {
                _logger.LogWarning($"Failed to unfollow user with ID {userId}");
                return BadRequest(new { message = "Failed to unfollow", userId });
            }

            _logger.LogInformation($"Successfully unfollowed user with ID {userId}");
            return Ok(new { message = "Successfully unfollowed", userId });
        }
    }
}

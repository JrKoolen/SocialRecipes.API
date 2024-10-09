using Microsoft.AspNetCore.Mvc;
using SocialRecipes.Domain.IServices;
using Microsoft.Extensions.Logging;

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

        [HttpGet("GetFollowers/{userId}")]
        public IActionResult GetFollowers(int userId)
        {
            _logger.LogInformation($"Retrieving followers for user with ID {userId}");

            var followers = _followerService.GetFollowers(userId);
            if (followers == null)
            {
                _logger.LogWarning($"No followers found for user with ID {userId}");
                return NotFound(new { message = "No followers found", userId });
            }

            return Ok(new { message = "Followers retrieved", followers });
        }

        [HttpGet("GetFollowing/{userId}")]
        public IActionResult GetFollowing(int userId)
        {
            _logger.LogInformation($"Retrieving following users for user with ID {userId}");

            var following = _followerService.GetFollowing(userId);
            if (following == null)
            {
                _logger.LogWarning($"User with ID {userId} is not following anyone.");
                return NotFound(new { message = "No following users found", userId });
            }

            return Ok(new { message = "Following users retrieved", following });
        }
        [HttpPost("Follow/{userId}")]
        public IActionResult Follow(int userId)
        {
            _logger.LogInformation($"User attempting to follow user with ID {userId}");

            var result = _followerService.Follow(userId); 

            if (!result)
            {
                _logger.LogWarning($"Failed to follow user with ID {userId}");
                return BadRequest(new { message = "Failed to follow", userId });
            }

            _logger.LogInformation($"Successfully followed user with ID {userId}");
            return Ok(new { message = "Successfully followed", userId });
        }

        [HttpDelete("Unfollow/{userId}")]
        public IActionResult RemoveFollow(int userId)
        {
            _logger.LogInformation($"User attempting to unfollow user with ID {userId}");

            var result = _followerService.RemoveFollow(userId);

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

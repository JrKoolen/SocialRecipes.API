using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialRecipes.Services.Services;

namespace SocialRecipes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FollowController : Controller
    {
        private readonly ILogger<FollowController> _logger;
        private readonly FollowService _followerService;

        public FollowController(ILogger<FollowController> logger, FollowService followerService)
        {
            _logger = logger;
            _followerService = followerService;
        }

        /// <summary>
        /// Follows a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to be followed.</param>
        /// <param name="followerId">The ID of the user who wants to follow.</param>
        /// <returns>
        /// A success message if the follow action is completed, or an error if the action fails.
        /// </returns>
        [HttpPost("Follow/{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> FollowAsync(int userId, int followerId)
        {
            try
            {
                _logger.LogInformation($"User {followerId} attempting to follow user with ID {userId}.");
                await _followerService.FollowAsync(userId, followerId);
                _logger.LogInformation($"User {followerId} successfully followed user with ID {userId}.");
                return Ok(new { message = "Successfully followed the user.", userId });
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning(ex, $"Invalid arguments for follow operation: {ex.Message}");
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, $"Invalid operation: {ex.Message}");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while user {followerId} was attempting to follow user {userId}.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal server error. Please try again later." });
            }
        }

        /// <summary>
        /// Retrieves all followers for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose followers are being retrieved.</param>
        /// <returns>A list of followers, or an error message if no followers are found.</returns>
        [HttpGet("GetFollowers/{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFollowersAsync(int userId)
        {
            try
            {
                _logger.LogInformation($"Retrieving followers for user with ID {userId}.");

                var followers = await _followerService.GetFollowersAsync(userId);
                if (followers == null || followers.Length == 0)
                {
                    _logger.LogWarning($"No followers found for user with ID {userId}.");
                    return NotFound(new { message = "No followers found for the specified user.", userId });
                }

                _logger.LogInformation($"Successfully retrieved followers for user with ID {userId}.");
                return Ok(new { message = "Followers retrieved successfully.", followers });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving followers for user with ID {userId}.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal server error. Please try again later." });
            }
        }

        /// <summary>
        /// Retrieves the list of users that the specified user is following.
        /// </summary>
        /// <param name="userId">The ID of the user whose following list is being retrieved.</param>
        /// <returns>A list of followed users, or an error message if the user is not following anyone.</returns>
        [HttpGet("GetFollowing/{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFollowingAsync(int userId)
        {
            try
            {
                _logger.LogInformation($"Retrieving users followed by user with ID {userId}.");

                var following = await _followerService.GetFollowingAsync(userId);
                if (following == null || following.Length == 0)
                {
                    _logger.LogWarning($"User with ID {userId} is not following anyone.");
                    return NotFound(new { message = "No following users found for the specified user.", userId });
                }

                _logger.LogInformation($"Successfully retrieved following users for user with ID {userId}.");
                return Ok(new { message = "Following users retrieved successfully.", following });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving following users for user with ID {userId}.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal server error. Please try again later." });
            }
        }

        /// <summary>
        /// Unfollows a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to be unfollowed.</param>
        /// <param name="followerId">The ID of the user who wants to unfollow.</param>
        /// <returns>
        /// A success message if the unfollow action is completed, or an error if the action fails.
        /// </returns>
        [HttpDelete("Unfollow/{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveFollowAsync(int userId, int followerId)
        {
            try
            {
                _logger.LogInformation($"User {followerId} attempting to unfollow user with ID {userId}.");
                await _followerService.RemoveFollowAsync(userId, followerId);

                _logger.LogInformation($"User {followerId} successfully unfollowed user with ID {userId}.");
                return Ok(new { message = "Successfully unfollowed the user.", userId });
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning(ex, $"Invalid arguments for unfollow operation: {ex.Message}");
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, $"Invalid operation: {ex.Message}");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while user {followerId} was attempting to unfollow user {userId}.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal server error. Please try again later." });
            }
        }

    }
}

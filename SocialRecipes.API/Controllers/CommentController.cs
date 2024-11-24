using Microsoft.AspNetCore.Mvc;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Services.Services;

namespace SocialRecipes.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly CommentService _commentService;
        private readonly ILogger<AuthController> _logger;

        public CommentController(ILogger<AuthController> logger, CommentService commentService)
        {
            _commentService = commentService;
            _logger = logger;
        }

        /// <summary>
        /// Adds a new comment to a recipe.
        /// </summary>
        /// <param name="comment">The comment details to be added.</param>
        /// <returns>A success message if the comment is added successfully.</returns>
        [HttpPost("Add")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddComment([FromForm] CommentDto comment)
        {
            try
            {
                if (comment == null)
                {
                    _logger.LogWarning("Invalid input: Comment cannot be null.");
                    return BadRequest(new { error = "InvalidInput", message = "Comment cannot be null." });
                }

                _logger.LogInformation($"Adding a new comment to recipe {comment.RecipeId} by user {comment.UserId}.");
                await _commentService.AddCommentAsync(comment);

                _logger.LogInformation("Comment added successfully.");
                return Ok(new { message = "Comment added successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding a comment.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal server error. Please try again later." });
            }
        }

        /// <summary>
        /// Retrieves all comments for a specific recipe.
        /// </summary>
        /// <param name="recipeId">The ID of the recipe to fetch comments for.</param>
        /// <returns>A list of comments associated with the recipe.</returns>
        [HttpGet("ByRecipe/{recipeId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCommentsByRecipe(int recipeId)
        {
            try
            {
                _logger.LogInformation($"Fetching comments for recipe with ID {recipeId}.");
                var comments = await _commentService.GetCommentsByRecipeIdAsync(recipeId);

                if (comments == null || !comments.Any())
                {
                    _logger.LogWarning($"No comments found for recipe ID {recipeId}.");
                    return NotFound(new { error = "NotFound", message = "No comments found for the specified recipe." });
                }

                _logger.LogInformation($"Successfully fetched comments for recipe ID {recipeId}.");
                return Ok(comments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unexpected error occurred while fetching comments for recipe ID {recipeId}.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal server error. Please try again later." });
            }
        }

        /// <summary>
        /// Retrieves all comments made by a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user to fetch comments for.</param>
        /// <returns>A list of comments made by the user.</returns>
        [HttpGet("ByUser/{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCommentsByUser(int userId)
        {
            try
            {
                _logger.LogInformation($"Fetching comments made by user with ID {userId}.");
                var comments = await _commentService.GetCommentsByUserIdAsync(userId);

                if (comments == null || !comments.Any())
                {
                    _logger.LogWarning($"No comments found for user ID {userId}.");
                    return NotFound(new { error = "NotFound", message = "No comments found for the specified user." });
                }

                _logger.LogInformation($"Successfully fetched comments for user ID {userId}.");
                return Ok(comments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unexpected error occurred while fetching comments for user ID {userId}.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal server error. Please try again later." });
            }
        }

        /// <summary>
        /// Deletes a specific comment by its ID.
        /// </summary>
        /// <param name="commentId">The ID of the comment to delete.</param>
        /// <returns>A success message if the comment is deleted successfully.</returns>
        [HttpDelete("Delete/{commentId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete comment with ID {commentId}.");
                await _commentService.DeleteCommentByIdAsync(commentId);

                _logger.LogInformation($"Successfully deleted comment with ID {commentId}.");
                return Ok(new { message = "Comment deleted successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unexpected error occurred while deleting comment with ID {commentId}.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal server error. Please try again later." });
            }
        }

    }
}

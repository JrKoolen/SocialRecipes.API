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

        public CommentController(CommentService commentService)
        {
            _commentService = commentService; 
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddComment([FromBody] CommentDto comment)
        {
            if (comment == null)
                return BadRequest("Comment cannot be null.");

            await _commentService.AddCommentAsync(comment);
            return Ok(new { message = "Comment added successfully" });
        }

        [HttpGet("ByRecipe/{recipeId}")]
        public async Task<IActionResult> GetCommentsByRecipe(int recipeId)
        {
            var comments = await _commentService.GetCommentsByRecipeIdAsync(recipeId);
            return Ok(comments);
        }

        [HttpGet("ByUser/{userId}")]
        public async Task<IActionResult> GetCommentsByUser(int userId)
        {
            var comments = await _commentService.GetCommentsByUserIdAsync(userId);
            return Ok(comments);
        }

        [HttpDelete("Delete/{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            await _commentService.DeleteCommentByIdAsync(commentId);
            return Ok(new { message = "Comment deleted successfully" });
        }
    }
}

using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Services.IRepositories;
using Microsoft.Extensions.Logging;

namespace SocialRecipes.Services.Services
{
    public class CommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ILogger<CommentService> _logger;

        public CommentService(ICommentRepository commentRepository, ILogger<CommentService> logger)
        {
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task AddCommentAsync(CommentDto comment)
        {
            try
            {
                if (comment == null)
                {
                    _logger.LogWarning("Attempted to add a null comment.");
                    throw new ArgumentNullException(nameof(comment), "Comment cannot be null.");
                }

                _logger.LogInformation("Adding a new comment by User {UserId} to Recipe {RecipeId}.", comment.UserId, comment.RecipeId);
                await _commentRepository.AddCommentAsync(comment);
                _logger.LogInformation("Comment successfully added.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a comment.");
                throw new ApplicationException("An error occurred while adding the comment. Please try again later.", ex);
            }
        }

        public async Task<CommentDto[]> GetCommentsByRecipeIdAsync(int recipeId)
        {
            try
            {
                if (recipeId <= 0)
                {
                    _logger.LogWarning("Invalid RecipeId: {RecipeId}. Must be greater than zero.", recipeId);
                    throw new ArgumentOutOfRangeException(nameof(recipeId), "RecipeId must be greater than zero.");
                }

                _logger.LogInformation("Retrieving comments for RecipeId: {RecipeId}.", recipeId);
                var comments = await _commentRepository.GetCommentsByRecipeIdAsync(recipeId);
                _logger.LogInformation("Successfully retrieved comments for RecipeId: {RecipeId}.", recipeId);

                return comments ?? Array.Empty<CommentDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving comments for RecipeId: {RecipeId}.", recipeId);
                throw new ApplicationException("An error occurred while retrieving comments. Please try again later.", ex);
            }
        }

        public async Task<CommentDto[]> GetCommentsByUserIdAsync(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    _logger.LogWarning("Invalid UserId: {UserId}. Must be greater than zero.", userId);
                    throw new ArgumentOutOfRangeException(nameof(userId), "UserId must be greater than zero.");
                }

                _logger.LogInformation("Retrieving comments for UserId: {UserId}.", userId);
                var comments = await _commentRepository.GetCommentsByUserIdAsync(userId);
                _logger.LogInformation("Successfully retrieved comments for UserId: {UserId}.", userId);

                return comments ?? Array.Empty<CommentDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving comments for UserId: {UserId}.", userId);
                throw new ApplicationException("An error occurred while retrieving comments. Please try again later.", ex);
            }
        }

        public async Task DeleteCommentByIdAsync(int commentId)
        {
            try
            {
                if (commentId <= 0)
                {
                    _logger.LogWarning("Invalid CommentId: {CommentId}. Must be greater than zero.", commentId);
                    throw new ArgumentOutOfRangeException(nameof(commentId), "CommentId must be greater than zero.");
                }

                _logger.LogInformation("Deleting comment with CommentId: {CommentId}.", commentId);
                await _commentRepository.DeleteCommentByIdAsync(commentId);
                _logger.LogInformation("Comment with CommentId: {CommentId} successfully deleted.", commentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting comment with CommentId: {CommentId}.", commentId);
                throw new ApplicationException("An error occurred while deleting the comment. Please try again later.", ex);
            }
        }
    }
}

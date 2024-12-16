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
        // After coming back to the code after receiving feedback i did the following
        // Moved logging to the controller because its a form of output and should be done in the controller.
        // But if it is needed in the service layer it can be done.
        // So i removed logger from the injection.
        // I also removed the try catch block because it was to excessive and not needed
        // In some functions i always returned True and the function was async so i changed it to void.
        // Kept the Logger injection for now but should be removed.
        public async Task AddCommentAsync(CommentDto comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment), "Comment cannot be null.");
            }

            await _commentRepository.AddCommentAsync(comment);
        }

        public async Task<CommentDto[]> GetCommentsByRecipeIdAsync(int recipeId)
        {
            if (recipeId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(recipeId), "RecipeId must be greater than zero.");
            }

            var comments = await _commentRepository.GetCommentsByRecipeIdAsync(recipeId);
            return comments ?? Array.Empty<CommentDto>();
        }

        public async Task<CommentDto[]> GetCommentsByUserIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "UserId must be greater than zero.");
            }

            var comments = await _commentRepository.GetCommentsByUserIdAsync(userId);
            return comments ?? Array.Empty<CommentDto>();
        }

        public async Task DeleteCommentByIdAsync(int commentId)
        {
            if (commentId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(commentId), "CommentId must be greater than zero.");
            }

            await _commentRepository.DeleteCommentByIdAsync(commentId);
        }

    }
}

using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Services.IRepositories;
using Microsoft.Extensions.Logging;

namespace SocialRecipes.Services.Services
{
    public class CommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
        }
        public virtual async Task AddCommentAsync(CommentDto comment)
        {
            await _commentRepository.AddCommentAsync(comment);
        }

        public virtual async Task<CommentDto[]> GetCommentsByRecipeIdAsync(int recipeId)
        {
            var comments = await _commentRepository.GetCommentsByRecipeIdAsync(recipeId);
            return comments ?? Array.Empty<CommentDto>();
        }

        public virtual async Task<CommentDto[]> GetCommentsByUserIdAsync(int userId)
        {
            var comments = await _commentRepository.GetCommentsByUserIdAsync(userId);
            return comments ?? Array.Empty<CommentDto>();
        }

        public virtual async Task DeleteCommentByIdAsync(int commentId)
        {
            await _commentRepository.DeleteCommentByIdAsync(commentId);
        }

    }
}

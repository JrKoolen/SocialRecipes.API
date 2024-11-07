using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.IServices;
using SocialRecipes.Services.IRepositories;

namespace SocialRecipes.Services.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task AddCommentAsync(CommentDto comment)
        {
            await _commentRepository.AddCommentAsync(comment);
        }

        public async Task<CommentDto[]> GetCommentsByRecipeIdAsync(int recipeId)
        {
            return await _commentRepository.GetCommentsByRecipeIdAsync(recipeId);
        }

        public async Task<CommentDto[]> GetCommentsByUserIdAsync(int userId)
        {
            return await _commentRepository.GetCommentsByUserIdAsync(userId);
        }

        public async Task DeleteCommentByIdAsync(int commentId)
        {
            await _commentRepository.DeleteCommentByIdAsync(commentId);
        }
    }
}

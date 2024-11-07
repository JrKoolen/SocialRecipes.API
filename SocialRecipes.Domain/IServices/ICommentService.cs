using SocialRecipes.Domain.Dto.General;

namespace SocialRecipes.Domain.IServices
{
    public interface ICommentService
    {
        Task AddCommentAsync(CommentDto comment);
        Task<CommentDto[]> GetCommentsByRecipeIdAsync(int recipeId);
        Task<CommentDto[]> GetCommentsByUserIdAsync(int userId);
        Task DeleteCommentByIdAsync(int commentId);
    }
}

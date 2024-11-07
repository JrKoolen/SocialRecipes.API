using SocialRecipes.Domain.Dto.General;

namespace SocialRecipes.Services.IRepositories
{
    public interface ICommentRepository
    {
        Task AddCommentAsync(CommentDto comment);
        Task<CommentDto[]> GetCommentsByRecipeIdAsync(int recipeId);
        Task<CommentDto[]> GetCommentsByUserIdAsync(int userId);
        Task DeleteCommentByIdAsync(int commentId);
    }
}

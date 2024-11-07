using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;

namespace SocialRecipes.Domain.IServices
{
    public interface IRecipeService
    {
        Task AddRecipeAsync(AddRecipeDto recipe);
        Task<RecipeDto[]> GetAllRecipesAsync();
        Task<RecipeDto[]> GetAllRecipesFromStatusAsync(string status);
        Task<RecipeDto[]> GetAllRecipesFromStatusAndUserAsync(string status, int userId);
        Task<RecipeDto[]> GetAllRecipesFromUserAsync(int userId);
        Task<RecipeDto> GetRecipeByIdAsync(int id);
        Task DeleteRecipeByIdAsync(int id);
        Task UpdateRecipeAsync(RecipeDto recipe);
    }
}

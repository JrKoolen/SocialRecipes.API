using SocialRecipes.Domain.Dto.IN;
using SocialRecipes.Domain.Dto.General;

namespace SocialRecipes.Services.IRepositories
{
    public interface IRecipeRepository
    {
        Task<bool> AddRecipeAsync(AddRecipeDto recipe);
        Task<RecipeDto> GetRecipeByIdAsync(int id);
        Task<RecipeDto[]> GetAllRecipesFromUserAsync(int userId);
        Task<RecipeDto[]> GetAllRecipesAsync();
        Task<RecipeDto[]> GetAllRecipesFromStatusAsync(string status);
        Task<RecipeDto[]> GetAllRecipesFromStatusAndUserAsync(string status, int userId);
        Task<int>GetTotalRecipesAsync();
        Task DeleteRecipeByIdAsync(int id);
        Task UpdateRecipeAsync(RecipeDto recipe);
    }
}

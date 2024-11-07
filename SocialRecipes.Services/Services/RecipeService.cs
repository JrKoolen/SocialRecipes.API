using SocialRecipes.Services.IRepositories;
using SocialRecipes.Domain.IServices;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;

namespace SocialRecipes.Services.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task AddRecipeAsync(AddRecipeDto recipe)
        {
            await _recipeRepository.AddRecipeAsync(recipe);
        }

        public async Task<RecipeDto[]> GetAllRecipesAsync()
        {
            return await _recipeRepository.GetAllRecipesAsync();
        }

        public async Task<RecipeDto[]> GetAllRecipesFromStatusAsync(string status)
        {
            return await _recipeRepository.GetAllRecipesFromStatusAsync(status);
        }

        public async Task<RecipeDto[]> GetAllRecipesFromStatusAndUserAsync(string status, int userId)
        {
            return await _recipeRepository.GetAllRecipesFromStatusAndUserAsync(status, userId);
        }

        public async Task<RecipeDto[]> GetAllRecipesFromUserAsync(int userId)
        {
            return await _recipeRepository.GetAllRecipesFromUserAsync(userId);
        }

        public async Task<RecipeDto> GetRecipeByIdAsync(int id)
        {
            return await _recipeRepository.GetRecipeByIdAsync(id);
        }

        public async Task DeleteRecipeByIdAsync(int id)
        {
            await _recipeRepository.DeleteRecipeByIdAsync(id);
        }

        public async Task UpdateRecipeAsync(RecipeDto recipe)
        {
            await _recipeRepository.UpdateRecipeAsync(recipe);
        }
    }
}

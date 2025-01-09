using SocialRecipes.Services.IRepositories;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;
using Microsoft.Extensions.Logging;

namespace SocialRecipes.Services.Services
{
    public class RecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository ?? throw new ArgumentNullException(nameof(recipeRepository));
        }

        public virtual async Task AddRecipeAsync(AddRecipeDto recipe)
        {
            await _recipeRepository.AddRecipeAsync(recipe);
        }

        public virtual async Task<RecipeDto[]> GetAllRecipesAsync()
        {
            var recipes = await _recipeRepository.GetAllRecipesAsync();
            return recipes ?? Array.Empty<RecipeDto>();
        }

        public virtual async Task<RecipeDto[]> GetAllRecipesFromStatusAsync(string status)
        {
            var recipes = await _recipeRepository.GetAllRecipesFromStatusAsync(status);
            return recipes ?? Array.Empty<RecipeDto>();
        }

        public virtual async Task<RecipeDto[]> GetAllRecipesFromStatusAndUserAsync(string status, int userId)
        {
            var recipes = await _recipeRepository.GetAllRecipesFromStatusAndUserAsync(status, userId);
            return recipes ?? Array.Empty<RecipeDto>();
        }

        public virtual async Task<RecipeDto[]> GetAllRecipesFromUserAsync(int userId)
        {
            var recipes = await _recipeRepository.GetAllRecipesFromUserAsync(userId);
            return recipes ?? Array.Empty<RecipeDto>();
        }

        public virtual async Task<RecipeDto> GetRecipeByIdAsync(int id)
        {

            var recipe = await _recipeRepository.GetRecipeByIdAsync(id);
            return recipe;
        }

        public virtual async Task DeleteRecipeByIdAsync(int id)
        {
            await _recipeRepository.DeleteRecipeByIdAsync(id);
        }

        public virtual async Task UpdateRecipeAsync(RecipeDto recipe)
        {
            await _recipeRepository.UpdateRecipeAsync(recipe);
        }

        public virtual async Task<RecipeDto[]> GetFeaturedRecipesAsync(string status, int featuredCount)
        {
            var recipes = await _recipeRepository.GetAllRecipesFromStatusAsync(status);
            return recipes
                .OrderByDescending(recipe => recipe.Likes)
                .Take(featuredCount)
                .ToArray();
        }

        public virtual async Task<int> GetTotalRecipesAsync()
        {
            return await _recipeRepository.GetTotalRecipesAsync();
        }
    }
}

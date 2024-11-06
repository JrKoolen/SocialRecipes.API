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

        public void AddRecipe(AddRecipeDto recipe)
        {
            _recipeRepository.AddRecipe(recipe);
        }

        public RecipeDto[] GetAllRecipes()
        {
            return _recipeRepository.GetAllRecipes();
        }

        public RecipeDto[] GetAllRecipesFromStatus(string status)
        {
            return _recipeRepository.GetAllRecipesFromStatus(status);
        }

        public RecipeDto[] GetAllRecipesFromStatusAndUser(string status, int userId)
        {
            return _recipeRepository.GetAllRecipesFromStatusAndUser(status, userId);
        }

        public RecipeDto[] GetAllRecipesFromUser(int userId)
        {
            return _recipeRepository.GetAllRecipesFromUser(userId);
        }

        public RecipeDto GetRecipeById(int id)
        {
            return _recipeRepository.GetRecipeById(id);
        }

        public void DeleteRecipeFromId(int id)
        {
            _recipeRepository.DeleteRecipeFromId(id);
        }

        public void UpdateRecipe(RecipeDto recipe)
        {
            _recipeRepository.UpdateRecipe(recipe);
        }
    }
}

using SocialRecipes.Domain.Dto.IN;
using SocialRecipes.Domain.Dto.General;

namespace SocialRecipes.Domain.IServices
{
    public interface IRecipeService
    {
        public void AddRecipe(AddRecipeDto recipe);
        public RecipeDto GetRecipeById(int id);
        public RecipeDto[] GetAllRecipesFromUser(int userId);
        public RecipeDto[] GetAllRecipes();
        public RecipeDto[] GetAllRecipesFromStatus(string status);
        public RecipeDto[] GetAllRecipesFromStatusAndUser(string status, int userId);
        public void DeleteRecipeFromId(int id);
        public void UpdateRecipe(RecipeDto recipe);
    }
}

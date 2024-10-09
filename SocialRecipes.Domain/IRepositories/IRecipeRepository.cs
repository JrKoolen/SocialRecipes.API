using SocialRecipes.DTO.IN;
using SocialRecipes.DTO.General;
using System.ComponentModel;
namespace SocialRecipes.Domain.IRepositories
{
    public interface IRecipeRepository
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

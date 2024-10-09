using SocialRecipes.DTO.IN;
using SocialRecipes.DTO.General;
using System.ComponentModel;
namespace SocialRecipes.Domain.IRepositories
{
    public interface IRecipeRepositoy
    {
        public void AddRecipe(AddRecipeDto recipe);
        public RecipeDto GetRecipeById(int id);
        public RecipeDto[] GetAllRecipesFromUser(int userId);
        public RecipeDto[] GetAllRecipes();
        public RecipeDto[] GetAllRecipesFromStatus(int status);
        public RecipeDto[] GetAllRecipesFromStatusAndUser(int status, int userId);
        public void DeleteRecipeFromId(int id);
        public void UpdateRecipe(RecipeDto recipe);
    }
}

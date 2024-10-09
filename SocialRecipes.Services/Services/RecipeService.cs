using SocialRecipes.Domain.IRepositories;
using SocialRecipes.Domain.IServices;
using SocialRecipes.DTO.General;
using SocialRecipes.DTO.IN;

namespace SocialRecipes.Services.Services
{
    public class RecipeService : IRecipeService
    {
        public void AddRecipe(AddRecipeDto recipe)
        {
            throw new NotImplementedException();
        }

        public RecipeDto[] GetAllRecipes()
        {
            throw new NotImplementedException();
        }

        public RecipeDto[] GetAllRecipesFromStatus(int status)
        {
            throw new NotImplementedException();
        }

        public RecipeDto[] GetAllRecipesFromStatusAndUser(int status, int userId)
        {
            throw new NotImplementedException();
        }

        public RecipeDto[] GetAllRecipesFromUser(int userId)
        {
            throw new NotImplementedException();
        }

        public RecipeDto GetRecipeById(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteRecipeFromId(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateRecipe(RecipeDto recipe)
        {
            throw new NotImplementedException();
        }
    }
}

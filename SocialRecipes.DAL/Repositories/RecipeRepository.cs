using SocialRecipes.Domain.IRepositories;
using SocialRecipes.DTO.IN;
using MySql.Data.MySqlClient;
using SocialRecipes.DTO.General;

namespace SocialRecipes.DAL.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly string _connectionString;

        public RecipeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

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

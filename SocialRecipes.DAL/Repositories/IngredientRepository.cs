using SocialRecipes.Domain.IRepositories;
using SocialRecipes.DTO.IN;
using MySql.Data.MySqlClient;
using SocialRecipes.DTO.General;

namespace SocialRecipes.DAL.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly string _connectionString;

        public IngredientRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddIngredient(AddIngredientDto ingredient)
        {
            throw new NotImplementedException();
        }

        public IngredientDto GetIngredientsFromRecipeId(int recipeId)
        {
            throw new NotImplementedException();
        }

        public void RemoveIngredient(int ingredientId)
        {
            throw new NotImplementedException();
        }

        public void UpdateIngredient(IngredientDto ingredient)
        {
            throw new NotImplementedException();
        }
    }
}

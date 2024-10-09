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
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO Ingredients (Name, RecipeId, Amount, Metric) VALUES (@Name, @RecipeId, @Amount, @Metric)";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", ingredient.Name);
                    command.Parameters.AddWithValue("@RecipeId", ingredient.RecipeId);
                    command.Parameters.AddWithValue("@Amount", ingredient.Amount);
                    command.Parameters.AddWithValue("@Metric", ingredient.Metric);

                    command.ExecuteNonQuery();
                }
            }
        }

        public IngredientDto[] GetIngredientsFromRecipeId(int recipeId)
        {
            var ingredients = new List<IngredientDto>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"SELECT Id, Name, RecipeId, Amount, Metric FROM Ingredients WHERE RecipeId = @RecipeId";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RecipeId", recipeId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var ingredient = new IngredientDto
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Name"),
                                RecipeId = reader.GetString("RecipeId"),
                                Amount = reader.GetInt32("Amount"),
                                Metric = reader.GetInt32("Metric")
                            };
                            ingredients.Add(ingredient);
                        }
                    }
                }
            }
            return ingredients.ToArray();
        }

        public void RemoveIngredient(int ingredientId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"DELETE FROM Ingredients WHERE Id = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", ingredientId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateIngredient(IngredientDto ingredient)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE Ingredients SET Name = @Name, RecipeId = @RecipeId, Amount = @Amount, Metric = @Metric WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", ingredient.Id);
                    command.Parameters.AddWithValue("@Name", ingredient.Name);
                    command.Parameters.AddWithValue("@RecipeId", ingredient.RecipeId);
                    command.Parameters.AddWithValue("@Amount", ingredient.Amount);
                    command.Parameters.AddWithValue("@Metric", ingredient.Metric);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

using SocialRecipes.Services.IRepositories;
using MySql.Data.MySqlClient;
using SocialRecipes.

namespace SocialRecipes.DAL.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly string _connectionString;

        public IngredientRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        //public void AddIngredient(AddIngredientDto ingredient)
        //{
        //    using (var connection = new MySqlConnection(_connectionString))
        //    {
        //        connection.Open();

        //        string query = @"
        //    INSERT INTO ingredients (Name, Recipe_id, Amount, Metric) 
        //    VALUES (@Name, @RecipeId, @Amount, @Metric)";

        //        using (var command = new MySqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@Name", ingredient.Name);
        //            command.Parameters.AddWithValue("@RecipeId", ingredient.RecipeId); 
        //            command.Parameters.AddWithValue("@Amount", ingredient.Amount);
        //            command.Parameters.AddWithValue("@Metric", ingredient.Metric);

        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}


        public IngredientDto[] GetIngredientsFromRecipeId(int recipeId)
        {
            var ingredients = new List<IngredientDto>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
            SELECT id, name, Recipe_id, amount, metric 
            FROM ingredients 
            WHERE Recipe_id = @RecipeId";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RecipeId", recipeId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var ingredient = new IngredientDto
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name"),
                                RecipeId = reader.GetInt32("Recipe_id"),
                                Amount = reader.GetInt32("amount"),
                                Metric = reader.GetInt32("metric")
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
                string query = @"DELETE FROM ingredients WHERE id = @Id";
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
                string query = @"
            UPDATE ingredients 
            SET Name = @Name, Recipe_id = @RecipeId, Amount = @Amount, Metric = @Metric 
            WHERE id = @Id";

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

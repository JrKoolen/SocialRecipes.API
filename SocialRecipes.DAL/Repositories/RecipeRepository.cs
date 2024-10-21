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
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string userCheckQuery = "SELECT COUNT(*) FROM users WHERE id = @UserId";
                        using (var userCheckCommand = new MySqlCommand(userCheckQuery, connection, transaction))
                        {
                            userCheckCommand.Parameters.AddWithValue("@UserId", recipe.UserId);
                            var userExists = Convert.ToInt32(userCheckCommand.ExecuteScalar()) > 0;

                            if (!userExists)
                            {
                                return;
                            }
                        }

                        string recipeQuery = @"
                    INSERT INTO recipe (title, body, status, user_id, created_at) 
                    VALUES (@Title, @Body, @Status, @User_Id, NOW())";

                        int recipeId;
                        using (var recipeCommand = new MySqlCommand(recipeQuery, connection, transaction))
                        {
                            recipeCommand.Parameters.AddWithValue("@Title", recipe.Title);
                            recipeCommand.Parameters.AddWithValue("@Body", recipe.Body);
                            recipeCommand.Parameters.AddWithValue("@Status", recipe.Status);
                            recipeCommand.Parameters.AddWithValue("@User_Id", recipe.UserId);
                            recipeCommand.ExecuteNonQuery();
                            recipeId = (int)recipeCommand.LastInsertedId; 
                        }

                        if (recipe.Recipes != null && recipe.Recipes.Count > 0)
                        {
                            string ingredientQuery = @"
                        INSERT INTO ingredients (name, amount, metric, Recipe_id) 
                        VALUES (@Name, @Amount, @Metric, @RecipeId)";

                            foreach (var ingredient in recipe.Recipes)
                            {
                                using (var ingredientCommand = new MySqlCommand(ingredientQuery, connection, transaction))
                                {
                                    ingredientCommand.Parameters.AddWithValue("@Name", ingredient.Name);
                                    ingredientCommand.Parameters.AddWithValue("@Amount", ingredient.Amount);
                                    ingredientCommand.Parameters.AddWithValue("@Metric", ingredient.Metric);
                                    ingredientCommand.Parameters.AddWithValue("@RecipeId", recipeId);

                                    ingredientCommand.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }



        public RecipeDto[] GetAllRecipes()
        {
            var recipes = new List<RecipeDto>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"SELECT Id, Title, Description, Body, User_Id, Status, Image, Likes, Created_at FROM Recipe";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var recipe = new RecipeDto
                            {
                                Id = reader.GetInt32("Id"),
                                Title = reader.IsDBNull(reader.GetOrdinal("Title")) ? null : reader.GetString("Title"),
                                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString("Description"),
                                Likes = reader.IsDBNull(reader.GetOrdinal("Likes")) ? 0 : reader.GetInt32("Likes"),
                                Body = reader.IsDBNull(reader.GetOrdinal("Body")) ? null : reader.GetString("Body"),
                                UserId = reader.IsDBNull(reader.GetOrdinal("User_id")) ? 0 : reader.GetInt32("User_id"), 
                                Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : reader.GetString("Status"),
                                DateTime = reader.IsDBNull(reader.GetOrdinal("Created_at")) ? DateTime.MinValue : reader.GetDateTime("Created_at"),
                                Image = reader.IsDBNull(reader.GetOrdinal("Image")) ? null : (byte[])reader["Image"]

                            };
                            recipes.Add(recipe);
                        }
                    }
                }
            }
            return recipes.ToArray();
        }


        public RecipeDto[] GetAllRecipesFromStatus(string status)
        {
            var recipes = new List<RecipeDto>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"SELECT Id, Title, Body, User_Id, Status, Created_at FROM Recipe WHERE Status = @Status";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", status);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var recipe = new RecipeDto
                            {
                                Id = reader.GetInt32("Id"),
                                Title = reader.IsDBNull(reader.GetOrdinal("Title")) ? null : reader.GetString("Title"),
                                Body = reader.IsDBNull(reader.GetOrdinal("Body")) ? null : reader.GetString("Body"),
                                UserId = reader.IsDBNull(reader.GetOrdinal("User_id")) ? 0 : reader.GetInt32("User_id"),
                                Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : reader.GetString("Status"),
                                DateTime = reader.IsDBNull(reader.GetOrdinal("Created_at")) ? DateTime.MinValue : reader.GetDateTime("Created_at")
                            };
                            recipes.Add(recipe);
                        }
                    }
                }
            }

            return recipes.ToArray();
        }

        public RecipeDto[] GetAllRecipesFromStatusAndUser(string status, int userId)
        {
            var recipes = new List<RecipeDto>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"SELECT Id, Title, Body, User_Id, Status, Created_at FROM Recipe WHERE Status = @Status AND User_Id = @User_Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@User_Id", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var recipe = new RecipeDto
                            {
                                Id = reader.GetInt32("Id"),
                                Title = reader.IsDBNull(reader.GetOrdinal("Title")) ? null : reader.GetString("Title"),
                                Body = reader.IsDBNull(reader.GetOrdinal("Body")) ? null : reader.GetString("Body"),
                                UserId = reader.IsDBNull(reader.GetOrdinal("User_id")) ? 0 : reader.GetInt32("User_id"), 
                                Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : reader.GetString("Status"),
                                DateTime = reader.IsDBNull(reader.GetOrdinal("Created_at")) ? DateTime.MinValue : reader.GetDateTime("Created_at")
                            };

                            recipes.Add(recipe);
                        }
                    }
                }
            }
            return recipes.ToArray();
        }


        public RecipeDto[] GetAllRecipesFromUser(int userId)
        {
            var recipes = new List<RecipeDto>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"SELECT Id, Title, Body, User_Id, Status, Created_at FROM Recipe WHERE User_Id = @User_Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@User_Id", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var recipe = new RecipeDto
                            {
                                Id = reader.GetInt32("Id"),
                                Title = reader.IsDBNull(reader.GetOrdinal("Title")) ? null : reader.GetString("Title"),
                                Body = reader.IsDBNull(reader.GetOrdinal("Body")) ? null : reader.GetString("Body"),
                                UserId = reader.IsDBNull(reader.GetOrdinal("User_id")) ? 0 : reader.GetInt32("User_id"), 
                                Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : reader.GetString("Status"),
                                DateTime = reader.IsDBNull(reader.GetOrdinal("Created_at")) ? DateTime.MinValue : reader.GetDateTime("Created_at")
                            };
                            recipes.Add(recipe);
                        }
                    }
                }
            }
            return recipes.ToArray();
        }


        public RecipeDto GetRecipeById(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"SELECT Id, Title, Description, Body, User_Id, Status, Image, Likes, Created_at FROM Recipe WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new RecipeDto
                            {
                                Id = reader.GetInt32("Id"),
                                Title = reader.IsDBNull(reader.GetOrdinal("Title")) ? null : reader.GetString("Title"),
                                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString("Description"),
                                Likes = reader.IsDBNull(reader.GetOrdinal("Likes")) ? 0 : reader.GetInt32("Likes"),
                                Body = reader.IsDBNull(reader.GetOrdinal("Body")) ? null : reader.GetString("Body"),
                                UserId = reader.IsDBNull(reader.GetOrdinal("User_id")) ? 0 : reader.GetInt32("User_id"),
                                Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : reader.GetString("Status"),
                                DateTime = reader.IsDBNull(reader.GetOrdinal("Created_at")) ? DateTime.MinValue : reader.GetDateTime("Created_at"),
                                Image = reader.IsDBNull(reader.GetOrdinal("Image")) ? null : (byte[])reader["Image"]
                            };
                        }
                    }
                }
            }
            return null;
        }


        public void DeleteRecipeFromId(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"DELETE FROM Recipe WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }


        public void UpdateRecipe(RecipeDto recipe)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE Recipe SET Title = @Title, Body = @Body, User_Id = @User_Id, Status = @Status, Created_at = @Created_at WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", recipe.Id);
                    command.Parameters.AddWithValue("@Title", recipe.Title);
                    command.Parameters.AddWithValue("@Body", recipe.Body);
                    command.Parameters.AddWithValue("@User_Id", recipe.UserId);
                    command.Parameters.AddWithValue("@Status", recipe.Status);
                    command.Parameters.AddWithValue("@Created_at", recipe.DateTime);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}

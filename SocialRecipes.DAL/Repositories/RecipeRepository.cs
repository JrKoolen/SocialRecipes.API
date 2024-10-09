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

                string query = @"INSERT INTO recipe (Title, Body, Status) VALUES (@Title, @Body, @Status)";

                using (var command = new MySqlCommand(query, connection))
                {
                    // DONT FORGET TO WRITE THE INGRIEDENTS AND ASSIGN THEM IN THE SERVICE TO THE OTHER REPOSITORY
                    command.Parameters.AddWithValue("@Title", recipe.Title);
                    command.Parameters.AddWithValue("@Body", recipe.Body);
                    command.Parameters.AddWithValue("@Status", recipe.Status);
                    command.ExecuteNonQuery();
                }
            }
        }

        public RecipeDto[] GetAllRecipes()
        {
            var recipes = new List<RecipeDto>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"SELECT Id, Title, Body, User_Id, Status, Created_at FROM Recipe";

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
                string query = @"SELECT Id, Title, Body, User_Id, Status, Created_at FROM Recipe WHERE UserId = @UserId";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
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
                string query = @"SELECT Id, Title, Body, Status, User_id, Created_at FROM Recipe WHERE Id = @Id";

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
                                Body = reader.IsDBNull(reader.GetOrdinal("Body")) ? null : reader.GetString("Body"),
                                UserId = reader.IsDBNull(reader.GetOrdinal("User_id")) ? 0 : reader.GetInt32("User_id"), 
                                Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : reader.GetString("Status"),
                                DateTime = reader.IsDBNull(reader.GetOrdinal("Created_at")) ? DateTime.MinValue : reader.GetDateTime("Created_at")
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
                string query = @"UPDATE Recipe SET Title = @Title, Body = @Body, Use_rId = @UserId, Status = @Status, Created_at = @Created_at WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", recipe.Id);
                    command.Parameters.AddWithValue("@Title", recipe.Title);
                    command.Parameters.AddWithValue("@Body", recipe.Body);
                    command.Parameters.AddWithValue("@UserId", recipe.UserId);
                    command.Parameters.AddWithValue("@Status", recipe.Status);
                    command.Parameters.AddWithValue("@Created_at", recipe.DateTime);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}

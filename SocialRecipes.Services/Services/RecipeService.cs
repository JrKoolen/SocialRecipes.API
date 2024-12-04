using SocialRecipes.Services.IRepositories;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;
using Microsoft.Extensions.Logging;

namespace SocialRecipes.Services.Services
{
    public class RecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly ILogger<RecipeService> _logger;

        public RecipeService(IRecipeRepository recipeRepository, ILogger<RecipeService> logger)
        {
            _recipeRepository = recipeRepository ?? throw new ArgumentNullException(nameof(recipeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> AddRecipeAsync(AddRecipeDto recipe)
        {
            try
            {
                if (recipe == null)
                {
                    _logger.LogWarning("Attempted to add a null recipe.");
                    throw new ArgumentNullException(nameof(recipe), "Recipe cannot be null.");
                }

                _logger.LogInformation("Adding a new recipe titled '{Title}' by User {UserId}.", recipe.Title, recipe.UserId);
                var result = await _recipeRepository.AddRecipeAsync(recipe);
                _logger.LogInformation("Recipe titled '{Title}' added successfully.", recipe.Title);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a recipe.");
                throw new ApplicationException("An error occurred while adding the recipe. Please try again later.", ex);
            }
        }

        public async Task<RecipeDto[]> GetAllRecipesAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all recipes.");
                var recipes = await _recipeRepository.GetAllRecipesAsync();
                _logger.LogInformation("Successfully retrieved all recipes.");
                return recipes ?? Array.Empty<RecipeDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all recipes.");
                throw new ApplicationException("An error occurred while retrieving recipes. Please try again later.", ex);
            }
        }

        public async Task<RecipeDto[]> GetAllRecipesFromStatusAsync(string status)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(status))
                {
                    _logger.LogWarning("Invalid status: '{Status}'. Status cannot be null or empty.", status);
                    throw new ArgumentException("Status cannot be null or empty.", nameof(status));
                }

                _logger.LogInformation("Retrieving recipes with status '{Status}'.", status);
                var recipes = await _recipeRepository.GetAllRecipesFromStatusAsync(status);
                _logger.LogInformation("Successfully retrieved recipes with status '{Status}'.", status);
                return recipes ?? Array.Empty<RecipeDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving recipes with status '{Status}'.", status);
                throw new ApplicationException("An error occurred while retrieving recipes by status. Please try again later.", ex);
            }
        }

        public async Task<RecipeDto[]> GetAllRecipesFromStatusAndUserAsync(string status, int userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(status))
                {
                    _logger.LogWarning("Invalid status: '{Status}'. Status cannot be null or empty.", status);
                    throw new ArgumentException("Status cannot be null or empty.", nameof(status));
                }

                if (userId <= 0)
                {
                    _logger.LogWarning("Invalid UserId: {UserId}. Must be greater than zero.", userId);
                    throw new ArgumentOutOfRangeException(nameof(userId), "UserId must be greater than zero.");
                }

                _logger.LogInformation("Retrieving recipes with status '{Status}' for UserId: {UserId}.", status, userId);
                var recipes = await _recipeRepository.GetAllRecipesFromStatusAndUserAsync(status, userId);
                _logger.LogInformation("Successfully retrieved recipes with status '{Status}' for UserId: {UserId}.", status, userId);
                return recipes ?? Array.Empty<RecipeDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving recipes with status '{Status}' for UserId: {UserId}.", status, userId);
                throw new ApplicationException("An error occurred while retrieving recipes by status and user. Please try again later.", ex);
            }
        }

        public async Task<RecipeDto[]> GetAllRecipesFromUserAsync(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    _logger.LogWarning("Invalid UserId: {UserId}. Must be greater than zero.", userId);
                    throw new ArgumentOutOfRangeException(nameof(userId), "UserId must be greater than zero.");
                }

                _logger.LogInformation("Retrieving all recipes created by UserId: {UserId}.", userId);
                var recipes = await _recipeRepository.GetAllRecipesFromUserAsync(userId);
                _logger.LogInformation("Successfully retrieved recipes for UserId: {UserId}.", userId);
                return recipes ?? Array.Empty<RecipeDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving recipes for UserId: {UserId}.", userId);
                throw new ApplicationException("An error occurred while retrieving recipes by user. Please try again later.", ex);
            }
        }

        public async Task<RecipeDto> GetRecipeByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Invalid RecipeId: {Id}. Must be greater than zero.", id);
                    throw new ArgumentOutOfRangeException(nameof(id), "RecipeId must be greater than zero.");
                }

                _logger.LogInformation("Retrieving recipe with RecipeId: {Id}.", id);
                var recipe = await _recipeRepository.GetRecipeByIdAsync(id);

                if (recipe == null)
                {
                    _logger.LogWarning("Recipe with RecipeId: {Id} not found.", id);
                    throw new KeyNotFoundException($"Recipe with ID {id} not found.");
                }

                _logger.LogInformation("Successfully retrieved recipe with RecipeId: {Id}.", id);
                return recipe;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving recipe with RecipeId: {Id}.", id);
                throw new ApplicationException("An error occurred while retrieving the recipe. Please try again later.", ex);
            }
        }

        public async Task DeleteRecipeByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Invalid RecipeId: {Id}. Must be greater than zero.", id);
                    throw new ArgumentOutOfRangeException(nameof(id), "RecipeId must be greater than zero.");
                }

                _logger.LogInformation("Deleting recipe with RecipeId: {Id}.", id);
                await _recipeRepository.DeleteRecipeByIdAsync(id);
                _logger.LogInformation("Successfully deleted recipe with RecipeId: {Id}.", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting recipe with RecipeId: {Id}.", id);
                throw new ApplicationException("An error occurred while deleting the recipe. Please try again later.", ex);
            }
        }

        public async Task UpdateRecipeAsync(RecipeDto recipe)
        {
            try
            {
                if (recipe == null)
                {
                    _logger.LogWarning("Attempted to update a null recipe.");
                    throw new ArgumentNullException(nameof(recipe), "Recipe cannot be null.");
                }

                if (recipe.Id <= 0)
                {
                    _logger.LogWarning("Invalid RecipeId: {Id}. Must be greater than zero.", recipe.Id);
                    throw new ArgumentOutOfRangeException(nameof(recipe.Id), "RecipeId must be greater than zero.");
                }

                _logger.LogInformation("Updating recipe with RecipeId: {Id}.", recipe.Id);
                await _recipeRepository.UpdateRecipeAsync(recipe);
                _logger.LogInformation("Successfully updated recipe with RecipeId: {Id}.", recipe.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating recipe with RecipeId: {Id}.", recipe?.Id);
                throw new ApplicationException("An error occurred while updating the recipe. Please try again later.", ex);
            }
        }

        public async Task<RecipeDto[]> GetFeaturedRecipesAsync(string status, int featuredCount)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(status))
                {
                    _logger.LogWarning("Invalid status: '{Status}'. Status cannot be null or empty.", status);
                    throw new ArgumentException("Status cannot be null or empty.", nameof(status));
                }

                if (featuredCount <= 0)
                {
                    _logger.LogWarning("Invalid featuredCount: {FeaturedCount}. It must be greater than zero.", featuredCount);
                    throw new ArgumentException("Featured count must be greater than zero.", nameof(featuredCount));
                }

                _logger.LogInformation("Retrieving featured recipes with status '{Status}' and top {FeaturedCount} likes.", status, featuredCount);

                var recipes = await _recipeRepository.GetAllRecipesFromStatusAsync(status);

                if (recipes == null || recipes.Length == 0)
                {
                    _logger.LogInformation("No recipes found with status '{Status}'.", status);
                    return Array.Empty<RecipeDto>();
                }

                var featuredRecipes = recipes
                    .OrderByDescending(recipe => recipe.Likes) 
                    .Take(featuredCount)
                    .ToArray();

                _logger.LogInformation("Successfully retrieved {Count} featured recipes with status '{Status}'.", featuredRecipes.Length, status);
                return featuredRecipes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving featured recipes with status '{Status}'.", status);
                throw new ApplicationException("An error occurred while retrieving featured recipes. Please try again later.", ex);
            }
        }

    }
}

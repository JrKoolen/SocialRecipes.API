using Microsoft.AspNetCore.Mvc;
using SocialRecipes.Services.Services;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SocialRecipes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly ILogger<RecipeController> _logger;
        private readonly RecipeService _recipeService;

        public RecipeController(ILogger<RecipeController> logger, RecipeService recipeService)
        {
            _logger = logger;
            _recipeService = recipeService;
        }

        /// <summary>
        /// Creates a new recipe.
        /// </summary>
        /// <param name="recipeDto">The details of the recipe to be created.</param>
        /// <returns>A success message if the recipe is created, or an error message if it fails.</returns>
        [HttpPost("CreateRecipe")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddRecipeAsync([FromForm] AddRecipeDto recipeDto)
        {
            try
            {
                _logger.LogInformation("Starting AddRecipeAsync in Controller.");

                if (recipeDto == null || string.IsNullOrWhiteSpace(recipeDto.Title))
                {
                    _logger.LogWarning("Invalid input: Recipe data is null or missing title.");
                    return BadRequest(new { message = "Recipe data is invalid. Please provide all required fields." });
                }

                await _recipeService.AddRecipeAsync(recipeDto);

                _logger.LogInformation("Recipe created successfully.");
                return Ok(new { message = "Recipe created successfully.", recipeTitle = recipeDto.Title });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid recipe data provided.");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in AddRecipeAsync in Controller.");
                return StatusCode(500, new { message = "Internal server error. Please try again later." });
            }
        }


        /// <summary>
        /// Updates an existing recipe.
        /// </summary>
        /// <param name="recipe">The updated details of the recipe.</param>
        /// <returns>A success message if the recipe is updated, or an error message if it fails.</returns>
        [HttpPost("UpdateRecipe")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateRecipeAsync([FromForm] RecipeDto recipe)
        {
            try
            {
                if (recipe == null)
                {
                    _logger.LogWarning("Recipe update failed: Recipe data is null.");
                    return BadRequest(new { message = "Recipe data is null." });
                }

                _logger.LogInformation($"Updating recipe with ID {recipe.Id}.");
                await _recipeService.UpdateRecipeAsync(recipe);
                _logger.LogInformation($"Recipe with ID {recipe.Id} updated successfully.");
                return Ok(new { message = $"Recipe with ID {recipe.Id} updated successfully.", recipe });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating recipe with ID {recipe?.Id}.");
                return StatusCode(500, new { message = "Internal server error. Please try again later." });
            }
        }

        /// <summary>
        /// Retrieves a recipe by its ID.
        /// </summary>
        /// <param name="id">The ID of the recipe to retrieve.</param>
        /// <returns>The details of the recipe, or a not found error if the recipe does not exist.</returns>
        [HttpGet("GetRecipeById/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRecipeByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving recipe with ID {id}.");
                var recipe = await _recipeService.GetRecipeByIdAsync(id);
                if (recipe == null)
                {
                    _logger.LogWarning($"Recipe with ID {id} not found.");
                    return NotFound(new { message = $"Recipe with ID {id} not found." });
                }

                var response = new
                {
                    recipe.Id,
                    recipe.Title,
                    recipe.Likes,
                    recipe.Description,
                    recipe.Body,
                    recipe.UserId,
                    recipe.Status,
                    recipe.DateTime,
                    ImageBase64 = recipe.Image != null ? Convert.ToBase64String(recipe.Image) : null
                };

                _logger.LogInformation($"Recipe with ID {id} retrieved successfully.");
                return Ok(new { message = "Recipe retrieved successfully.", recipe = response });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving recipe with ID {id}.");
                return StatusCode(500, new { message = "Internal server error. Please try again later." });
            }
        }

        /// <summary>
        /// Retrieves all recipes created by a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose recipes to retrieve.</param>
        /// <returns>A list of recipes created by the user.</returns>
        [HttpGet("GetAllRecipesFromUser/{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRecipesFromUserAsync(int userId)
        {
            try
            {
                _logger.LogInformation("Authorization Header: {Authorization}", Request.Headers["Authorization"]);
                _logger.LogInformation($"Retrieving all recipes from user with ID {userId}.");
                var recipes = await _recipeService.GetAllRecipesFromUserAsync(userId);
                if (recipes == null || !recipes.Any())
                {
                    _logger.LogWarning($"No recipes found for user with ID {userId}.");
                    return NotFound(new { message = $"No recipes found for user with ID {userId}." });
                }

                _logger.LogInformation($"Successfully retrieved recipes for user with ID {userId}.");
                return Ok(new { message = $"All recipes from user with ID {userId} retrieved successfully.", recipes });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving recipes for user with ID {userId}.");
                return StatusCode(500, new { message = "Internal server error. Please try again later." });
            }
        }

        /// <summary>
        /// Retrieves all recipes that are featured.
        /// </summary>
        /// <returns>A list of featured recipes.</returns>
        [HttpGet("GetFeaturedRecipes")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFeaturedRecipes(int amount)
        {
            try
            {
                _logger.LogInformation("Retrieving all featured recipes.");
                var featuredRecipes = await _recipeService.GetFeaturedRecipesAsync("private", amount);

                if (featuredRecipes == null || featuredRecipes.Length == 0)
                {
                    _logger.LogInformation("No featured recipes found.");
                    return NotFound("No featured recipes found.");
                }

                _logger.LogInformation("Successfully retrieved {Count} featured recipes.", featuredRecipes.Length);
                return Ok(featuredRecipes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving featured recipes.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving featured recipes. Please try again later.");
            }
        }

        [HttpGet("GetTotalRecipes")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTotalRecipes()
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve all recipes.");
                int recipeCount = await _recipeService.GetTotalRecipesAsync();
                _logger.LogInformation("Successfully retrieved all recipes.");
                return Ok(recipeCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while attempting to retrieve all recipes.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal server error. Please try again later." });
            }
        }
        /// <summary>
        /// Deletes a recipe by its ID.
        /// </summary>
        /// <param name="id">The ID of the recipe to delete.</param>
        /// <returns>A success message if the recipe is deleted, or an error if it fails.</returns>
        [HttpDelete("DeleteRecipeFromId/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveRecipeFromIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete recipe with ID {id}.");
                 await _recipeService.DeleteRecipeByIdAsync(id);

                _logger.LogInformation($"Recipe with ID {id} deleted successfully.");
                return Ok(new { message = $"Recipe with ID {id} deleted successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting recipe with ID {id}.");
                return StatusCode(500, new { message = "Internal server error. Please try again later." });
            }
        }

        /// <summary>
        /// Retrieves all recipes created by a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose recipes to retrieve.</param>
        /// <returns>A list of recipes created by the user.</returns>
        [HttpDelete(" DeleteAllRecipesFromUser/{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAllRecipesFromUserAsync(int userId)
        {
            try
            {
                _logger.LogInformation($"Deleteing all recipes from {userId}.");
                var recipes = await _recipeService.GetAllRecipesFromUserAsync(userId);

                if (recipes == null || !recipes.Any())
                {
                    _logger.LogWarning($"No recipes found for user with ID {userId}.");
                    return NotFound(new { message = $"No recipes found for user with ID {userId}." });
                }

                foreach (var recipe in recipes)
                {
                    await _recipeService.DeleteRecipeByIdAsync(recipe.Id);
                }
                _logger.LogInformation($"Successfully retrieved recipes for user with ID {userId}.");
                return Ok(new { message = $"All recipes from user with ID {userId} retrieved successfully."});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while trying to delete recipes for user with ID {userId}.");
                return StatusCode(500, new { message = "Internal server error. Please try again later." });
            }
        }
    }
}

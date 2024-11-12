using Microsoft.AspNetCore.Mvc;
using SocialRecipes.Services.Services;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;

namespace SocialRecipes.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : Controller
    {
        private readonly ILogger<RecipeController> _logger;
        private readonly RecipeService _recipeService;

        public RecipeController(ILogger<RecipeController> logger, RecipeService recipeService)
        {
            _logger = logger;
            _recipeService = recipeService;
        }

        [HttpPost("CreateRecipe")]
        public async Task<IActionResult> AddRecipeAsync([FromBody] AddRecipeDto recipeDto)
        {
            try
            {
                _logger.LogInformation("Starting AddRecipeAsync in Controller.");
                var result = await _recipeService.AddRecipeAsync(recipeDto);
                if (!result)
                {
                    _logger.LogWarning("Recipe creation failed due to invalid data or service error.");
                    return BadRequest("Failed to create recipe. Check input data.");
                }

                _logger.LogInformation("Recipe created successfully.");
                return Ok(new { message = "Recipe created successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in AddRecipeAsync in Controller.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpPost("UpdateRecipe")]
        public async Task<IActionResult> UpdateRecipeAsync(RecipeDto recipe)
        {
            if (recipe == null)
            {
                _logger.LogError("Recipe is null");
                return BadRequest("Recipe is null.");
            }
            _logger.LogInformation($"{recipe.Id} updated");
            await _recipeService.UpdateRecipeAsync(recipe);
            return Ok(new { message = "200", recipe.Id });
        }

        [HttpGet("GetRecipeById/{id}")]
        public async Task<IActionResult> GetRecipeByIdAsync(int id)
        {
            _logger.LogInformation($"Get recipe by id {id}");
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            if (recipe == null)
            {
                return NotFound(new { message = "Recipe not found", id });
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
            return Ok(new { message = "200", recipe = response });
        }

        [HttpGet("GetAllRecipesFromUser/{userId}")]
        public async Task<IActionResult> GetAllRecipesFromUserAsync(int userId)
        {
            _logger.LogInformation($"Get all recipes from user {userId}");
            var recipes = await _recipeService.GetAllRecipesFromUserAsync(userId);
            return Ok(new { message = "200", recipes });
        }

        [HttpGet("GetAllRecipes")]
        public async Task<IActionResult> GetAllRecipesAsync()
        {
            _logger.LogInformation("Get all recipes");
            var recipes = await _recipeService.GetAllRecipesAsync();

            var response = recipes.Select(recipe => new
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
            });

            return Ok(new { message = "200", recipes = response });
        }

        [HttpGet("GetAllRecipesFromStatus/{status}")]
        public async Task<IActionResult> GetAllRecipesFromStatusAsync(string status)
        {
            _logger.LogInformation($"Get all recipes by status {status}");
            var recipes = await _recipeService.GetAllRecipesFromStatusAsync(status);
            return Ok(new { message = "200", recipes });
        }

        [HttpGet("GetAllRecipesFromStatusAndUser/{status}/{userId}")]
        public async Task<IActionResult> GetAllRecipesFromStatusAndUserAsync(string status, int userId)
        {
            _logger.LogInformation($"Get all recipes by status {status} and user {userId}");
            var recipes = await _recipeService.GetAllRecipesFromStatusAndUserAsync(status, userId);
            return Ok(new { message = "200", recipes });
        }

        [HttpDelete("DeleteRecipeFromId")]
        public async Task<IActionResult> RemoveRecipeFromIdAsync(int id)
        {
            _logger.LogInformation($"{id} deleted");
            await _recipeService.DeleteRecipeByIdAsync(id);
            return Ok(new { message = "200", id });
        }
    }
}

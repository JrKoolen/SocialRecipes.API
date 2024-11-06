using Microsoft.AspNetCore.Mvc;
using SocialRecipes.Domain.IServices;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;

namespace SocialRecipes.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : Controller
    {
        private readonly ILogger<RecipeController> _logger;
        private readonly IRecipeService _recipeService;

        public RecipeController(ILogger<RecipeController> logger, IRecipeService recipeService)
        {
            _logger = logger;
            _recipeService = recipeService;
        }
        [HttpPost("CreateRecipe")]
        public IActionResult AddRecipe(AddRecipeDto recipe)
        {
            if (recipe == null)
            {
                _logger.LogError("recipe object sent from client is null.");
                return BadRequest("recipe is null.");
            }
            _recipeService.AddRecipe(recipe);
            _logger.LogInformation($"Creating a new recipe: {recipe.Title}");
            return Ok(new { message = "200", recipe });
        }
        [HttpPost("UpdateRecipe")]
        public IActionResult UpdateRecipe(RecipeDto recipe)
        {
            if (recipe == null)
            {
                _logger.LogError("recipe object sent from client is null.");
                return BadRequest("recipe is null.");
            }
            _logger.LogInformation($"{recipe.Id} updated");
            _recipeService.UpdateRecipe(recipe);
            return Ok(new { message = "200", recipe.Id });
        }
        [HttpGet("GetRecipeById/{id}")]
        public IActionResult GetRecipeById(int id)
        {
            _logger.LogInformation($"Get recipe by id {id}");
            var recipe = _recipeService.GetRecipeById(id);
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
        public IActionResult GetAllRecipesFromUser(int userId)
        {
            _logger.LogInformation($"Get all recipes for user {userId}");
            var recipes = _recipeService.GetAllRecipesFromUser(userId);
            return Ok(new { message = "200", recipes });
        }

        [HttpGet("GetAllRecipes")]
        public IActionResult GetAllRecipes()
        {
            _logger.LogInformation("Get all recipes");
            var recipes = _recipeService.GetAllRecipes();

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
        public IActionResult GetAllRecipesFromStatus(string status)
        {
            _logger.LogInformation($"Get all recipes by status {status}");
            var recipes = _recipeService.GetAllRecipesFromStatus(status);
            return Ok(new { message = "200", recipes });
        }

        [HttpGet("GetAllRecipesFromStatusAndUser/{status}/{userId}")]
        public IActionResult GetAllRecipesFromStatusAndUser(string status, int userId)
        {
            _logger.LogInformation($"Get all recipes by status {status} and user {userId}");
            var recipes = _recipeService.GetAllRecipesFromStatusAndUser(status, userId);
            return Ok(new { message = "200", recipes });
        }
        [HttpDelete("DeleteRecipeFromId")]
        public IActionResult RemoveRecipeFromId(int id)
        {
            _logger.LogInformation($"{id} deleted");
            _recipeService.DeleteRecipeFromId(id);
            return Ok(new { message = "200", id});
        }
    }
}

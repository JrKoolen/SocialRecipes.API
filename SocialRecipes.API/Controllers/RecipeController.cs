using Microsoft.AspNetCore.Mvc;
using SocialRecipes.Domain.IServices;
using SocialRecipes.DTO.General;

namespace SocialRecipes.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : Controller
    {
        private readonly ILogger<RecipeController> _logger;
        private readonly IRecipeService _recipeService;

        public RecipeController(ILogger<RecipeController> logger)
        {
            _logger = logger;
        }
        [HttpPost("CreateUser")]
        public IActionResult AddRecipe([FromBody] SocialRecipes.DTO.IN.AddRecipeDto recipe)
        {
            if (recipe == null)
            {
                _logger.LogError("recipe object sent from client is null.");
                return BadRequest("recipe is null.");
            }
            _recipeService.AddRecipe(recipe);
            _logger.LogInformation($"Creating a new recipe: {recipe.Name}");
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
            return Ok(new { message = "200", recipe });
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
            _logger.LogInformation($"Get all recipes");
            var recipes = _recipeService.GetAllRecipes();
            return Ok(new { message = "200", recipes });
        }

        [HttpGet("GetAllRecipesFromStatus/{status}")]
        public IActionResult GetAllRecipesFromStatus(int status)
        {
            _logger.LogInformation($"Get all recipes by status {status}");
            var recipes = _recipeService.GetAllRecipesFromStatus(status);
            return Ok(new { message = "200", recipes });
        }

        [HttpGet("GetAllRecipesFromStatusAndUser/{status}/{userId}")]
        public IActionResult GetAllRecipesFromStatusAndUser(int status, int userId)
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

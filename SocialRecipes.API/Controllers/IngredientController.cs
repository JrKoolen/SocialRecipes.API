using Microsoft.AspNetCore.Mvc;
using SocialRecipes.Domain.IServices;
using SocialRecipes.DTO.General;
using SocialRecipes.DTO.IN;

namespace SocialRecipes.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngredientController : Controller
    {
        private readonly ILogger<IngredientController> _logger;
        private readonly IIngredientService _ingredientService;

        public IngredientController(ILogger<IngredientController> logger)
        {
            _logger = logger;
        }
        [HttpPost("AddIngredient")]
        public IActionResult AddIngredient([FromBody] AddIngredientDto ingredient)
        {
            if (ingredient == null)
            {
                _logger.LogError("Ingredient object sent from client is null.");
                return BadRequest("Ingredient is null.");
            }

            _ingredientService.AddIngredient(ingredient);
            _logger.LogInformation($"Creating a new ingredient: {ingredient.Name}");
            return Ok(new { message = "Ingredient added successfully", ingredient });
        }

        [HttpDelete("RemoveIngredient/{id}")]
        public IActionResult RemoveIngredient(int id)
        {
            _logger.LogInformation($"Attempting to remove ingredient with ID {id}");

            IngredientDto ingredient = _ingredientService.GetIngredientsFromRecipeId(id);  
            if (ingredient == null)
            {
                _logger.LogWarning($"Ingredient with ID {id} not found.");
                return NotFound(new { message = "Ingredient not found", id });
            }

            _ingredientService.RemoveIngredient(id);
            _logger.LogInformation($"Ingredient with ID {id} removed from database.");
            return Ok(new { message = "Ingredient removed", id });
        }

        [HttpPut("UpdateIngredient")]
        public IActionResult UpdateIngredient([FromBody] IngredientDto ingredient)
        {
            if (ingredient == null)
            {
                _logger.LogError("Ingredient object sent from client is null.");
                return BadRequest("Ingredient is null.");
            }

            _logger.LogInformation($"Updating ingredient with ID {ingredient.Id}");
            var existingIngredient = _ingredientService.GetIngredientsFromRecipeId(ingredient.Id);  
            if (existingIngredient == null)
            {
                _logger.LogWarning($"Ingredient with ID {ingredient.Id} not found.");
                return NotFound(new { message = "Ingredient not found", ingredient.Id });
            }

            _ingredientService.UpdateIngredient(ingredient);
            _logger.LogInformation($"Ingredient with ID {ingredient.Id} successfully updated.");
            return Ok(new { message = "Ingredient updated", ingredient.Id });
        }
    }
}

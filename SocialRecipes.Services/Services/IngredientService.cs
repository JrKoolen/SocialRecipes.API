using SocialRecipes.Domain.IRepositories;
using SocialRecipes.Domain.IServices;
using SocialRecipes.DTO.General;
using SocialRecipes.DTO.IN;

namespace SocialRecipes.Services.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public void AddIngredient(AddIngredientDto ingredient)
        {
            _ingredientRepository.AddIngredient(ingredient);
        }

        public IngredientDto[] GetIngredientsFromRecipeId(int recipeId)
        {
            return _ingredientRepository.GetIngredientsFromRecipeId(recipeId);
        }

        public void RemoveIngredient(int ingredientId)
        {
            _ingredientRepository.RemoveIngredient(ingredientId);
        }

        public void UpdateIngredient(IngredientDto ingredient)
        {
            _ingredientRepository.UpdateIngredient(ingredient);
        }
    }
}

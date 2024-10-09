using SocialRecipes.DTO.General;
using SocialRecipes.DTO.IN;
namespace SocialRecipes.Domain.IServices
{
    public interface IIngredientService
    {
        public void AddIngredient(AddIngredientDto ingredient);
        public void UpdateIngredient(IngredientDto ingredient);
        public IngredientDto GetIngredientsFromRecipeId(int recipeId);
        public void RemoveIngredient(int ingredientId);
    }
}

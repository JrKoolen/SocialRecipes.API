using SocialRecipes.DTO.IN;
using SocialRecipes.DTO.General;
namespace SocialRecipes.Domain.IRepositories
{
    public interface IIngredientRepository
    {
        public void AddIngredient(AddIngredientDto ingredient);
        public void UpdateIngredient(IngredientDto ingredient);
        public IngredientDto[] GetIngredientsFromRecipeId(int recipeId);
        public void RemoveIngredient(int ingredientId);
    }
}

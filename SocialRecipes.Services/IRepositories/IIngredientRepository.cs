using SocialRecipes.DTO.General;

namespace SocialRecipes.Services.IRepositories
{
    public interface IIngredientRepository
    {
        public void UpdateIngredient(IngredientDto ingredient);
        public IngredientDto[] GetIngredientsFromRecipeId(int recipeId);
        public void RemoveIngredient(int ingredientId);
    }
}

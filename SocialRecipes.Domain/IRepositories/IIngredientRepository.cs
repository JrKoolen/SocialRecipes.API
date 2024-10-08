using SocialRecipes.DTO.IN;
using SocialRecipes.DTO.
using System.ComponentModel;
namespace SocialRecipes.Domain.IRepositories
{
    public interface IIngredientRepository
    {
        public void AddIngredient(AddIngredientDto ingredient);
        public void UpdateIngredient(IngredientDto ingredient);
        public void RemoveIngredient(int ingredientId);
    }
}

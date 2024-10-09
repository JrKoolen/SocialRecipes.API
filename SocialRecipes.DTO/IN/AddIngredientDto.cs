namespace SocialRecipes.DTO.IN
{
    public class AddIngredientDto
    {
        public string Name{ get; set; }
        public string ?RecipeId { get; set; }
        public int Amount { get; set; }
        public int Metric { get; set; }
    }
}

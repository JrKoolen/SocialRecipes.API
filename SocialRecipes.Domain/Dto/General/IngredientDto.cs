namespace SocialRecipes.Domain.Dto.General
{
    public class IngredientDto
    {
        public int Id { get; set; } 
        public required string Name { get; set; }
        public int RecipeId { get; set; }
        public int Amount { get; set; }
        public int Metric { get; set; }
    }
}

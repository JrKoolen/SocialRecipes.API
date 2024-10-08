namespace SocialRecipes.DTO.General
{
    public class IngredientDto
    {
        public int Id { get; set; } 
        public required string Name { get; set; }
        public int Amount { get; set; }
        public int Metric { get; set; }
    }
}

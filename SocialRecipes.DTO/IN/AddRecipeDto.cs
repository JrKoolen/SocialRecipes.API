using SocialRecipes.DTO.General;

namespace SocialRecipes.DTO.IN
{
    public class AddRecipeDto
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public List<IngredientDto> Recipes { get; set; } = new List<IngredientDto>();
        public int UserId { get; set; }
        public string Status { get; set; }
    }
}

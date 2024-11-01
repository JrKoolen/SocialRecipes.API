using SocialRecipes.Domain.Dto.General;

namespace SocialRecipes.Domain.Dto.IN
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

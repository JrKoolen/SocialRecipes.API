namespace SocialRecipes.DTO.IN
{
    public class AddRecipe
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
    }
}

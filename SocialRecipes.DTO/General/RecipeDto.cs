namespace SocialRecipes.DTO.General
{
    public class RecipeDto
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string status { get; set; }
        public DateTime DateTime { get; set; }
    }
}

namespace SocialRecipes.DTO.General
{
    public class RecipeDto
    {
        public int Id {  get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public DateTime DateTime { get; set; }
    }
}

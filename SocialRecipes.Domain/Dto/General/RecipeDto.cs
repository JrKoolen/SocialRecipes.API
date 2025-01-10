namespace SocialRecipes.Domain.Dto.General
{
    public class RecipeDto
    {
        public int Id { get; set; } = 0;
        public string Title { get; set; } = "";
        public int Likes { get; set; } = 0;
        public string Description { get; set; } = "";
        public string Body { get; set; } = "";
        public int UserId { get; set; } = 0;
        public string Status { get; set; } = "";
        public DateTime DateTime { get; set; }
        public byte[]? Image { get; set; }
    }
}

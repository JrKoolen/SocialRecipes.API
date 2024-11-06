namespace SocialRecipes.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Recipe Recipe { get; set; }
        public User User { get; set; }
    }
}

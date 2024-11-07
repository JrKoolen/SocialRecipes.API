namespace SocialRecipes.Domain.Dto.General
{
    public class CommentDto
    {
        public int Id { get; set; } 
        public int RecipeId { get; set; } 
        public int UserId { get; set; } 
        public string Content { get; set; } 
        public DateTime CreatedAt { get; set; } 
    }
}
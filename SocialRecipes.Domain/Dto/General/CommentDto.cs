namespace SocialRecipes.Domain.Dto.General
{
    public class CommentDto
    {
        public int Id { get; set; } = 0;
        public int RecipeId { get; set; } = 0;
        public int UserId { get; set; } = 0;
        public string? Content { get; set; } 
        public DateTime? CreatedAt { get; set; } 
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialRecipes.DAL.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; } = 0;

        [ForeignKey("RecipeId")]
        public int RecipeId { get; set; } = 0;

        [ForeignKey("UserId")]
        public int UserId { get; set; } = 0;

        [Required] 
        [Column(TypeName = "text")] 
        public string? Content { get; set; }

        [Column(TypeName = "datetime")] 
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public Recipe? Recipe { get; set; }
        public User? User { get; set; }
    }
}

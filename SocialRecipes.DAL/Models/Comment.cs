using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialRecipes.DAL.Models
{
    public class Comment
    {
        public int? Id { get; set; }

        public int? RecipeId { get; set; }

        public int? UserId { get; set; }

        [Required] 
        [Column(TypeName = "text")] 
        public string? Content { get; set; }

        [Column(TypeName = "datetime")] 
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public Recipe? Recipe { get; set; }
        public User? User { get; set; }
    }
}

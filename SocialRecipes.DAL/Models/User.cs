using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SocialRecipes.DAL.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; } = 0;

        [Required]
        [Column(TypeName = "varchar(255)")] 
        public string? Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")] 
        public string? Email { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")] 
        public string? Password { get; set; }

        [Column(TypeName = "datetime")] 
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Recipe>? Recipes { get; set; }
        public ICollection<Follower>? Followers { get; set; }
        public ICollection<Message>? SentMessages { get; set; }
        public ICollection<Message>? ReceivedMessages { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
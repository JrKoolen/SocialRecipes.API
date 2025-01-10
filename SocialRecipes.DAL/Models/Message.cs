using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialRecipes.DAL.Models
{
    public class Message
    {
        public int? Id { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string? Content { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? SentAt { get; set; } = DateTime.Now;

        public bool? IsRead { get; set; } = false;

        public User? Sender { get; set; } 
        public User? Receiver { get; set; } 
    }
}

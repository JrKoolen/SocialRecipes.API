using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialRecipes.DAL.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; } = 0;
        [ForeignKey("SenderId")]
        public int SenderId { get; set; } = 0;
        [ForeignKey("ReceiverId")]
        public int ReceiverId { get; set; } = 0;

        [Required]
        [Column(TypeName = "text")]
        public string Content { get; set; } = "";

        [Column(TypeName = "datetime")]
        public DateTime? SentAt { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;

        public User? Sender { get; set; } 
        public User? Receiver { get; set; } 
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialRecipes.DAL.Models
{
    public class Recipe
    {
        public int Id { get; set; } = 0;

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string? Title { get; set; }

        public int Likes { get; set; }

        [Required]
        [Column(TypeName = "text")] 
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "longtext")] 
        public string? Body { get; set; }

        public int? UserId { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")] 
        public string? Status { get; set; }

        [Column(TypeName = "datetime")] 
        public DateTime DateTime { get; set; } = DateTime.Now;

        [Column(TypeName = "longblob")] 
        public byte[]? Image { get; set; }

        public User? User { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}

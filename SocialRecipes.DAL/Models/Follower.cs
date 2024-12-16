using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialRecipes.DAL.Models
{
    public class Follower
    {
        public int FollowedUserId { get; set; }

        public int FollowingUserId { get; set; }

        [Column(TypeName = "datetime")] 
        public DateTime FollowingDate { get; set; } = DateTime.Now;

        public User FollowedUser { get; set; }
        public User FollowingUser { get; set; }
    }
}

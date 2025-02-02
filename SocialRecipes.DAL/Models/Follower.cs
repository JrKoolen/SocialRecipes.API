﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialRecipes.DAL.Models
{
    public class Follower
    {
        [ForeignKey("FollowerUserId")]
        public int FollowedUserId { get; set; } = 0;
        [ForeignKey("FollowingUserId")]
        public int FollowingUserId { get; set; } = 0;

        [Column(TypeName = "datetime")] 
        public DateTime? FollowingDate { get; set; } = DateTime.Now;

        public User? FollowedUser { get; set; }
        public User? FollowingUser { get; set; }
    }
}

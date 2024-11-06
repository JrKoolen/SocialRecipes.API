namespace SocialRecipes.Domain.Models
{
    public class Follower
    {
        public int FollowedUserId { get; set; }
        public int FollowingUserId { get; set; }
        public DateTime FollowingDate { get; set; } = DateTime.Now;

        public User FollowedUser { get; set; }
        public User FollowingUser { get; set; }
    }
}

namespace SocialRecipes.Domain.Dto.General
{
    public class FollowerDto
    {
        public int FollowedUserId { get; set; } = 0;
        public int FollowingUserId { get; set; } = 0;
        public DateTime? FollowingDate { get; set; } 
    }
}

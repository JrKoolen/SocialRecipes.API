namespace SocialRecipes.Domain.Dto.General
{
    public class FollowerDto
    {
        public int? FollowedUserId {  get; set; }
        public int? FollowingUserId {  get; set; }
        public DateTime? FollowingDate { get; set; } 
    }
}

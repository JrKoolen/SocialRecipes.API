namespace SocialRecipes.DTO.General
{
    public class FollowerDto
    {
        public int[]? followed_user_id {  get; set; }
        public int[]? following_user_id {  get; set; }
        public DateTime FollowingDate { get; set; } 
    }
}

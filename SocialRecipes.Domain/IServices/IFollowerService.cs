using SocialRecipes.DTO;
using SocialRecipes.DTO.General;
namespace SocialRecipes.Domain.IServices
{
    public interface IFollowerService 
    {
        public FollowerDto GetFollowers(int userid);
        public FollowerDto GetFollowing(int userid);
        public bool Follow(int userid);
        public bool RemoveFollow(int userid);
    }
}

using SocialRecipes.DTO;
using SocialRecipes.DTO.General;
namespace SocialRecipes.API.IServices
{
    public interface IFollowerService 
    {
        public UserDto[] GetFollowers(int userid);
        public UserDto[] GetFollowing(int userid);
        public bool Follow(int userId, int followerId);
        public bool RemoveFollow(int userId, int followerId);
    }
}

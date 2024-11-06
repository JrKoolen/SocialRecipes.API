using SocialRecipes.Domain.Dto.IN;
using SocialRecipes.Domain.Dto.General;
namespace SocialRecipes.Domain.IServices
{
    public interface IFollowerService 
    {
        public UserDto[] GetFollowers(int userid);
        public UserDto[] GetFollowing(int userid);
        public bool Follow(int userId, int followerId);
        public bool RemoveFollow(int userId, int followerId);
    }
}

using SocialRecipes.DTO.General;

namespace SocialRecipes.Services.IRepositories
{
    public interface IFollowerRepository
    {
        public UserDto[] GetFollowers(int userid);
        public UserDto[] GetFollowing(int userid);
        public void Follow(int userId, int followerId);
        public void RemoveFollow(int userId, int followerId);
    }
}

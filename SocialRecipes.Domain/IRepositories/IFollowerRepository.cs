using SocialRecipes.DTO.General;

namespace SocialRecipes.Domain.IRepositories
{
    public interface IFollowerRepository
    {
        public FollowerDto GetFollowers(int userid);
        public FollowerDto GetFollowing(int userid);
        public void Follow(int userId, int followerId);
        public void RemoveFollow(int userId, int followerId);
    }
}

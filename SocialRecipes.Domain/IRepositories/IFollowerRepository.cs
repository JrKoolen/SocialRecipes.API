using SocialRecipes.DTO;
namespace SocialRecipes.Domain.IRepositories
{
    public interface IFollowerRepository
    {
        public void GetFollowers(int userid);
        public void GetFollowing(int userid);
        public void Follow(int userid);
        public void RemoveFollow(int userid);
    }
}

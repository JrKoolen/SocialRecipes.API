using SocialRecipes.Domain.IRepositories;
using SocialRecipes.Domain.IServices;
using SocialRecipes.DTO.General;
using SocialRecipes.DTO.IN;

namespace SocialRecipes.Services.Services
{
    public class FollowerService : IFollowerService
    {
        public bool Follow(int userid)
        {
            throw new NotImplementedException();
        }

        public FollowerDto GetFollowers(int userid)
        {
            throw new NotImplementedException();
        }

        public FollowerDto GetFollowing(int userid)
        {
            throw new NotImplementedException();
        }

        public bool RemoveFollow(int userid)
        {
            throw new NotImplementedException();
        }
    }
}

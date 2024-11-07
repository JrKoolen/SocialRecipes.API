using SocialRecipes.Domain.Dto.General;

namespace SocialRecipes.Services.IRepositories
{
    public interface IFollowerRepository
    {
        Task<UserDto[]> GetFollowersAsync(int userId);
        Task<UserDto[]> GetFollowingAsync(int userId);
        Task FollowAsync(int userId, int followerId);
        Task RemoveFollowAsync(int userId, int followerId);
    }
}

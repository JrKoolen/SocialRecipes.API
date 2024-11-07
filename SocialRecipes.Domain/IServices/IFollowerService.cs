using SocialRecipes.Domain.Dto.General;

namespace SocialRecipes.Domain.IServices
{
    public interface IFollowerService
    {
        Task<UserDto[]> GetFollowersAsync(int userId);
        Task<UserDto[]> GetFollowingAsync(int userId);
        Task<bool> FollowAsync(int userId, int followerId);
        Task<bool> RemoveFollowAsync(int userId, int followerId);
    }
}

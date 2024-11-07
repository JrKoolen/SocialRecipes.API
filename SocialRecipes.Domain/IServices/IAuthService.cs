using SocialRecipes.Domain.Dto.IN;

namespace SocialRecipes.Domain.IServices
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginDto loginDto);
        Task<bool> RegisterAsync(AddUserDto addUserDto);
    }
}

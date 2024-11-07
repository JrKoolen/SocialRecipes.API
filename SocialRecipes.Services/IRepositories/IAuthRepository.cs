using SocialRecipes.Domain.Dto.IN;

namespace SocialRecipes.Services.IRepositories
{
    public interface IAuthRepository
    {
        Task<bool> LoginAsync(LoginDto loginDto);
        Task<bool> RegisterAsync(AddUserDto addUserDto);
    }
}

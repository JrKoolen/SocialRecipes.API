using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;

namespace SocialRecipes.Services.IRepositories
{
    public interface IAuthRepository
    {
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task<bool> RegisterAsync(AddUserDto addUserDto);
    }
}

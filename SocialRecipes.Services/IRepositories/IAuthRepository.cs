using SocialRecipes.DTO.IN;

namespace SocialRecipes.Services.IRepositories
{
    public interface IAuthRepository
    {
        bool Login(LoginDto loginDto);
        bool Register(AddUserDto addUserDto);
    }
}

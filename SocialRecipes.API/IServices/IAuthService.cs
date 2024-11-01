using SocialRecipes.DTO.IN;

namespace SocialRecipes.API.IServices
{
    public interface IAuthService
    {
        bool Login(LoginDto loginDto);
        bool Register(AddUserDto addUserDto);
    }
}

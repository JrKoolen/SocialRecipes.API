using SocialRecipes.DTO.IN;

namespace SocialRecipes.Domain.IServices
{
    public interface IAuthService
    {
        bool Login(LoginDto loginDto);
        bool Register(AddUserDto addUserDto);
    }
}

using SocialRecipes.DTO.IN;

namespace SocialRecipes.Domain.IRepositories
{
    public interface IAuthRepository
    {
        bool Login(LoginDto loginDto);
        bool Register(AddUserDto addUserDto);
    }
}

using SocialRecipes.Domain.Models;
using SocialRecipes.DTO.IN;

namespace SocialRecipes.Domain.IServices
{
    public interface IUserService
    {
        void CreateUser(CreateUserDto userInput);
    }
}

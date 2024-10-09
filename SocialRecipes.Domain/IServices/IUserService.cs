using SocialRecipes.Domain.Models;
using SocialRecipes.DTO.General;
using SocialRecipes.DTO.IN;

namespace SocialRecipes.Domain.IServices
{
    public interface IUserService
    {
        void CreateUser(AddUserDto userInput);
        void DeleteUserById(int id);
        UserDto GetUserById(int id);
    }
}

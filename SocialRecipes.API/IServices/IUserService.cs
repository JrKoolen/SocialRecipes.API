using SocialRecipes.Domain;
using SocialRecipes.DTO.IN;

namespace SocialRecipes.API.IServices
{
    public interface IUserService
    {
        void CreateUser(AddUserDto userInput);
        void DeleteUserById(int id);
        UserDto GetUserById(int id);
    }
}

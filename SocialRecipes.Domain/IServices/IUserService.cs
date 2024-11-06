using SocialRecipes.Domain.Dto.IN;
using SocialRecipes.Domain.Dto.General;

namespace SocialRecipes.Domain.IServices
{
    public interface IUserService
    {
        void CreateUser(AddUserDto userInput);
        void DeleteUserById(int id);
        UserDto GetUserById(int id);
    }
}

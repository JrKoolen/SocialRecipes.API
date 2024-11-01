using SocialRecipes.DTO.General;
using SocialRecipes.DTO.IN;

namespace SocialRecipes.Services.IRepositories
{
    public interface IUserRepository
    {
        void AddUser(AddUserDto user);
        UserDto GetUserById(int id);
        void DeleteUserById(int userId);
    }
}

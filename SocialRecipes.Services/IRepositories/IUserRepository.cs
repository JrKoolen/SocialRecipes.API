using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;

namespace SocialRecipes.Services.IRepositories
{
    public interface IUserRepository
    {
        void AddUser(AddUserDto user);
        UserDto GetUserById(int id);
        void DeleteUserById(int userId);
    }
}

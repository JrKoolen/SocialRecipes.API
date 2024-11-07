using SocialRecipes.Domain.Dto.IN;
using SocialRecipes.Domain.Dto.General;

namespace SocialRecipes.Domain.IServices
{
    public interface IUserService
    {
        Task CreateUserAsync(AddUserDto userInput); 
        Task DeleteUserByIdAsync(int id); 
        Task<UserDto> GetUserByIdAsync(int id); 
    }
}

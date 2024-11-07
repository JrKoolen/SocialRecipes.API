using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;
using System.Threading.Tasks;

namespace SocialRecipes.Services.IRepositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(AddUserDto user); 
        Task<UserDto> GetUserByIdAsync(int id); 
        Task DeleteUserByIdAsync(int userId); 
    }
}
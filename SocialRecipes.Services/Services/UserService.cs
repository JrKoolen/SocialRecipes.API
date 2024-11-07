using SocialRecipes.Services.IRepositories;
using SocialRecipes.Domain.IServices;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;

namespace SocialRecipes.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateUserAsync(AddUserDto userInput)
        {
            await _userRepository.AddUserAsync(userInput);
        }

        public async Task DeleteUserByIdAsync(int id)
        {
            await _userRepository.DeleteUserByIdAsync(id);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }
    }
}

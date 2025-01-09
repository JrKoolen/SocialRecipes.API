using SocialRecipes.Services.IRepositories;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;
using Microsoft.Extensions.Logging;

namespace SocialRecipes.Services.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public virtual async Task DeleteUserByIdAsync(int id)
        {

            await _userRepository.DeleteUserByIdAsync(id);
        }

        public virtual async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return user;
        }

        public virtual async Task<int> GetTotalUsersAsync()
        {
            var userCount = await _userRepository.GetTotalUsersAsync();
            return userCount;
        }
    }
}

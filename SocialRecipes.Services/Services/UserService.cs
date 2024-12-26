using SocialRecipes.Services.IRepositories;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;
using SocialRecipes.Domain.Logic;
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

        public async Task<string> CreateUserAsync(AddUserDto userInput)
        {
            ProcessName processName = new ProcessName(userInput.Name);
            string response = processName.Process();
            await _userRepository.AddUserAsync(userInput);
            return response;
        }

        public async Task DeleteUserByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "UserId must be greater than zero.");
            }

            await _userRepository.DeleteUserByIdAsync(id);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "UserId must be greater than zero.");
            }

            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            return user;
        }
    }
}

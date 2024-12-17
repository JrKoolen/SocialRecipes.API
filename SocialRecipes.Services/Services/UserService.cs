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
        // After coming back to the code after receiving feedback i did the following
        // Moved logging to the controller because its a form of output and should be done in the controller.
        // But if it is needed in the service layer it can be done.
        // So i removed logger from the injection.
        // I also removed the try catch block because it was to excessive and not needed
        // In some functions i always returned True and the function was async so i changed it to void.
        // Kept the Logger injection for now but should be removed.
        public async Task CreateUserAsync(AddUserDto userInput)
        {
            if (userInput == null)
            {
                throw new ArgumentNullException(nameof(userInput), "User input cannot be null.");
            }

            await _userRepository.AddUserAsync(userInput);
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

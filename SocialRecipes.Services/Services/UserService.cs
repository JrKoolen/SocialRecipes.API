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

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task CreateUserAsync(AddUserDto userInput)
        {
            try
            {
                if (userInput == null)
                {
                    _logger.LogWarning("Attempted to create a user with null input.");
                    throw new ArgumentNullException(nameof(userInput), "User input cannot be null.");
                }

                _logger.LogInformation("Creating a new user with Name: {Name}, Email: {Email}.", userInput.Name, userInput.Email);
                await _userRepository.AddUserAsync(userInput);
                _logger.LogInformation("User with Name: {Name}, Email: {Email} created successfully.", userInput.Name, userInput.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a user with Name: {Name}, Email: {Email}.", userInput?.Name, userInput?.Email);
                throw new ApplicationException("An error occurred while creating the user. Please try again later.", ex);
            }
        }

        public async Task DeleteUserByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Invalid UserId: {Id}. Must be greater than zero.", id);
                    throw new ArgumentOutOfRangeException(nameof(id), "UserId must be greater than zero.");
                }

                _logger.LogInformation("Deleting user with UserId: {Id}.", id);
                await _userRepository.DeleteUserByIdAsync(id);
                _logger.LogInformation("User with UserId: {Id} deleted successfully.", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the user with UserId: {Id}.", id);
                throw new ApplicationException("An error occurred while deleting the user. Please try again later.", ex);
            }
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Invalid UserId: {Id}. Must be greater than zero.", id);
                    throw new ArgumentOutOfRangeException(nameof(id), "UserId must be greater than zero.");
                }

                _logger.LogInformation("Retrieving user with UserId: {Id}.", id);
                var user = await _userRepository.GetUserByIdAsync(id);

                if (user == null)
                {
                    _logger.LogWarning("User with UserId: {Id} not found.", id);
                    throw new KeyNotFoundException($"User with ID {id} not found.");
                }

                _logger.LogInformation("User with UserId: {Id} retrieved successfully.", id);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the user with UserId: {Id}.", id);
                throw new ApplicationException("An error occurred while retrieving the user. Please try again later.", ex);
            }
        }
    }
}

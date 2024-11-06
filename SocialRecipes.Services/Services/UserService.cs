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

        public void CreateUser(AddUserDto userInput)
        {
            _userRepository.AddUser(userInput);
        }

        public void DeleteUserByID(int id)
        {
            _userRepository.DeleteUserById(id);
        }

        public void DeleteUserById(int id)
        {
            _userRepository.DeleteUserById(id);
        }

        public UserDto GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }
    }
}

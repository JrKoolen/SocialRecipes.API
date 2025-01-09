using SocialRecipes.Services.IRepositories;
using SocialRecipes.Domain.Dto.IN;
using System.Threading.Tasks;
using SocialRecipes.Domain.Dto.General;

namespace SocialRecipes.Services.Services
{
    public class AuthService 
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public virtual async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            return await _authRepository.LoginAsync(loginDto);
        }

        public virtual async Task<bool> RegisterAsync(AddUserDto addUserDto)
        {
            return await _authRepository.RegisterAsync(addUserDto);
        }
    }
}

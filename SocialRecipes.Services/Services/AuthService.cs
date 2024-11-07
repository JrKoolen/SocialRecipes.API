using SocialRecipes.Services.IRepositories;
using SocialRecipes.Domain.Dto.IN;
using SocialRecipes.Domain.IServices;
using System.Threading.Tasks;

namespace SocialRecipes.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<bool> LoginAsync(LoginDto loginDto)
        {
            return await _authRepository.LoginAsync(loginDto);
        }

        public async Task<bool> RegisterAsync(AddUserDto addUserDto)
        {
            return await _authRepository.RegisterAsync(addUserDto);
        }
    }
}

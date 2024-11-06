using SocialRecipes.Services.IRepositories;
using SocialRecipes.Domain.Dto.IN;
using SocialRecipes.Domain.IServices;
using System.Security.Claims;
using System.Text;

namespace SocialRecipes.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public bool Login(LoginDto loginDto)
        {
            return _authRepository.Login(loginDto);
        }

        public bool Register(AddUserDto addUserDto)
        {
            return _authRepository.Register(addUserDto);
        }
    }
}

using SocialRecipes.Services.IRepositories;
using SocialRecipes.Domain.Dto.IN;
using System.Threading.Tasks;
using SocialRecipes.Domain.Logic;
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

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            return await _authRepository.LoginAsync(loginDto);
        }

        public async Task<string> RegisterAsync(AddUserDto addUserDto)
        {
            ProcessName processName = new ProcessName(addUserDto.Name);
            string response = processName.Process();
            if (response.Contains("valid"))
            {
                await _authRepository.RegisterAsync(addUserDto);
                return "User created succesfully";
            }
            else 
            {
                return response;
            }
        }
    }
}

using SocialRecipes.Domain.Models;
using SocialRecipes.DTO.IN;

namespace SocialRecipes.Domain.IRepositories
{
    public interface IUserRepository
    {
        void CreateUser(CreateUserDto user);
    }
}

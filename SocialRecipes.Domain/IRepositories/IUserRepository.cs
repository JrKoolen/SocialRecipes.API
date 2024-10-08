using SocialRecipes.DTO.IN;

namespace SocialRecipes.Domain.IRepositories
{
    public interface IUserRepository
    {
        void AddUser(AddUserDto user);
    }
}

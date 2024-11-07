using SocialRecipes.Services.IRepositories;
using SocialRecipes.Domain.Dto.IN;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace SocialRecipes.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(AddUserDto user)
        {
            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password 
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserByIdAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user != null)
            {
                return new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
                };
            }

            return null;
        }
    }
}

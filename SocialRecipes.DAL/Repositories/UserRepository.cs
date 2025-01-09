using SocialRecipes.Services.IRepositories;
using SocialRecipes.Domain.Dto.IN;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.DAL.Models;
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
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User data cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
            {
                throw new ArgumentException("register not complete");
            }


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

        public Task<int> GetTotalUsersAsync()
        {
            var  totalUsers = _context.Users.CountAsync();
            return totalUsers;
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

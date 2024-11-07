using SocialRecipes.Services.IRepositories;
using SocialRecipes.Domain.Dto.IN;
using Microsoft.EntityFrameworkCore;
using SocialRecipes.Domain.Models;

namespace SocialRecipes.DAL.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Name == loginDto.Username);

            if (user == null)
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
        }

        public async Task<bool> RegisterAsync(AddUserDto addUserDto)
        {
            var userExists = await _context.Users
                .AnyAsync(u => u.Name == addUserDto.Name);

            if (userExists)
            {
                return false; 
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(addUserDto.Password);

            var newUser = new User
            {
                Name = addUserDto.Name,
                Email = addUserDto.Email,
                Password = hashedPassword,
                CreatedAt = DateTime.Now
            };

            await _context.Users.AddAsync(newUser);
            var result = await _context.SaveChangesAsync();

            return result > 0; 
        }
    }
}

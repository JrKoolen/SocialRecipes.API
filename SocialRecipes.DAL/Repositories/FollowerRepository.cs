using SocialRecipes.Services.IRepositories;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace SocialRecipes.DAL.Repositories
{
    public class FollowerRepository : IFollowerRepository
    {
        private readonly AppDbContext _context;

        public FollowerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto[]> GetFollowersAsync(int userId)
        {
            var followers = await _context.Followers
                .Where(f => f.FollowedUserId == userId)
                .Select(f => new UserDto
                {
                    Id = f.FollowingUser.Id,
                    Name = f.FollowingUser.Name,
                    Email = f.FollowingUser.Email
                })
                .ToArrayAsync();

            return followers;
        }

        public async Task<UserDto[]> GetFollowingAsync(int userId)
        {
            var following = await _context.Followers
                .Where(f => f.FollowingUserId == userId)
                .Select(f => new UserDto
                {
                    Id = f.FollowedUser.Id,
                    Name = f.FollowedUser.Name,
                    Email = f.FollowedUser.Email
                })
                .ToArrayAsync();

            return following;
        }

        public async Task FollowAsync(int userId, int followerId)
        {
            var follower = new Follower
            {
                FollowedUserId = userId,
                FollowingUserId = followerId,
                FollowingDate = DateTime.Now
            };

            await _context.Followers.AddAsync(follower);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RemoveFollowAsync(int userId, int followerId)
        {
            if (userId <= 0 || followerId <= 0)
            {
                return false;
            }

            try
            {
                var followEntry = await _context.Followers
                    .FirstOrDefaultAsync(f => f.FollowingUserId == followerId && f.FollowedUserId == userId);

                if (followEntry != null)
                {
                    _context.Followers.Remove(followEntry);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false; 
            }
            catch
            {
                return false; 
            }
        }

    }
}

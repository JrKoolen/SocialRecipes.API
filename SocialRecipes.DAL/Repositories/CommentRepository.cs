using SocialRecipes.Services.IRepositories;
using SocialRecipes.Domain.Dto.General;
using Microsoft.EntityFrameworkCore;
using SocialRecipes.DAL.Models;

namespace SocialRecipes.DAL.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddCommentAsync(CommentDto commentDto)
        {
            var comment = new Comment
            {
                RecipeId = commentDto.RecipeId,
                UserId = commentDto.UserId,
                Content = commentDto.Content,
                CreatedAt = commentDto.CreatedAt
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<CommentDto[]> GetCommentsByRecipeIdAsync(int recipeId)
        {
            return await _context.Comments
                .Where(c => c.RecipeId == recipeId)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    RecipeId = c.RecipeId,
                    UserId = c.UserId,
                    Content = c.Content,
                    CreatedAt = c.CreatedAt
                }).ToArrayAsync();
        }

        public async Task<CommentDto[]> GetCommentsByUserIdAsync(int userId)
        {
            return await _context.Comments
                .Where(c => c.UserId == userId)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    RecipeId = c.RecipeId,
                    UserId = c.UserId,
                    Content = c.Content,
                    CreatedAt = c.CreatedAt
                }).ToArrayAsync();
        }

        public async Task DeleteCommentByIdAsync(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }
    }
}

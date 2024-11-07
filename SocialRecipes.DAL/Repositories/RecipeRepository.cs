using SocialRecipes.Services.IRepositories;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;
using SocialRecipes.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace SocialRecipes.DAL.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly AppDbContext _context;

        public RecipeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddRecipeAsync(AddRecipeDto recipe)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == recipe.UserId);
            if (!userExists)
            {
                return;
            }

            var newRecipe = new Recipe
            {
                Title = recipe.Title,
                Body = recipe.Body,
                Description = recipe.Description,
                Status = recipe.Status,
                UserId = recipe.UserId,
                DateTime = DateTime.Now
            };

            await _context.Recipes.AddAsync(newRecipe);
            await _context.SaveChangesAsync();
        }

        public async Task<RecipeDto[]> GetAllRecipesAsync()
        {
            return await _context.Recipes
                .Select(r => new RecipeDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    Body = r.Body,
                    UserId = r.UserId,
                    Status = r.Status,
                    DateTime = r.DateTime,
                    Image = r.Image ?? null,
                    Likes = r.Likes
                }).ToArrayAsync();
        }

        public async Task<RecipeDto[]> GetAllRecipesFromStatusAsync(string status)
        {
            return await _context.Recipes
                .Where(r => r.Status == status)
                .Select(r => new RecipeDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    Body = r.Body,
                    UserId = r.UserId,
                    Status = r.Status,
                    DateTime = r.DateTime,
                    Image = r.Image ?? null,
                    Likes = r.Likes
                }).ToArrayAsync();
        }

        public async Task<RecipeDto[]> GetAllRecipesFromStatusAndUserAsync(string status, int userId)
        {
            return await _context.Recipes
                .Where(r => r.Status == status && r.UserId == userId)
                .Select(r => new RecipeDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    Body = r.Body,
                    UserId = r.UserId,
                    Status = r.Status,
                    DateTime = r.DateTime,
                    Image = r.Image ?? null,
                    Likes = r.Likes
                }).ToArrayAsync();
        }

        public async Task<RecipeDto[]> GetAllRecipesFromUserAsync(int userId)
        {
            return await _context.Recipes
                .Where(r => r.UserId == userId)
                .Select(r => new RecipeDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    Body = r.Body,
                    UserId = r.UserId,
                    Status = r.Status,
                    DateTime = r.DateTime,
                    Image = r.Image ?? null,
                    Likes = r.Likes

                }).ToArrayAsync();
        }

        public async Task<RecipeDto> GetRecipeByIdAsync(int id)
        {
            return await _context.Recipes
                .Where(r => r.Id == id)
                .Select(r => new RecipeDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    Body = r.Body,
                    UserId = r.UserId,
                    Status = r.Status,
                    DateTime = r.DateTime,
                    Image = r.Image ?? null,
                    Likes = r.Likes
                }).FirstOrDefaultAsync();
        }

        public async Task DeleteRecipeByIdAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
            }
        }



        public async Task UpdateRecipeAsync(RecipeDto recipe)
        {
            var existingRecipe = await _context.Recipes.FindAsync(recipe.Id);
            if (existingRecipe != null)
            {
                existingRecipe.Title = recipe.Title;
                existingRecipe.Body = recipe.Body;
                existingRecipe.UserId = recipe.UserId;
                existingRecipe.Status = recipe.Status;
                existingRecipe.DateTime = recipe.DateTime; 

                _context.Recipes.Update(existingRecipe);
                await _context.SaveChangesAsync();
            }
        }
    }
}

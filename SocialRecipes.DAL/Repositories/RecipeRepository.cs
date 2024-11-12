using SocialRecipes.Services.IRepositories;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;
using SocialRecipes.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace SocialRecipes.DAL.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<RecipeRepository> _logger;

        public RecipeRepository(AppDbContext context, ILogger<RecipeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<bool> AddRecipeAsync(AddRecipeDto recipe)
        {
            try
            {
                _logger.LogInformation("Starting AddRecipeAsync for user ID {UserId}", recipe.UserId);

                var userExists = await _context.Users.AnyAsync(u => u.Id == recipe.UserId);
                if (!userExists)
                {
                    _logger.LogWarning("User with ID {UserId} does not exist.", recipe.UserId);
                    return false;
                }

                byte[] imageBytes = null;
                if (!string.IsNullOrEmpty(recipe.Image))
                {
                    try
                    {
                        imageBytes = Convert.FromBase64String(recipe.Image);
                        _logger.LogInformation("Image converted successfully for user ID {UserId}", recipe.UserId);
                    }
                    catch (FormatException ex)
                    {
                        _logger.LogError(ex, "Invalid image format for recipe from user ID {UserId}", recipe.UserId);
                        return false;
                    }
                }

                var newRecipe = new Recipe
                {
                    Title = recipe.Title,
                    Body = recipe.Body,
                    Description = recipe.Description,
                    Status = recipe.Status,
                    UserId = recipe.UserId,
                    DateTime = DateTime.Now,
                    Image = imageBytes
                };

                await _context.Recipes.AddAsync(newRecipe);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Recipe created successfully for user ID {UserId}", recipe.UserId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding recipe for user ID {UserId}", recipe.UserId);
                return false;
            }
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

﻿using SocialRecipes.Services.IRepositories;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;
using Microsoft.Extensions.Logging;

namespace SocialRecipes.Services.Services
{
    public class RecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository ?? throw new ArgumentNullException(nameof(recipeRepository));
        }

        public async Task AddRecipeAsync(AddRecipeDto recipe)
        {
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe), "Recipe cannot be null.");
            }

            await _recipeRepository.AddRecipeAsync(recipe);
        }

        public async Task<RecipeDto[]> GetAllRecipesAsync()
        {
            var recipes = await _recipeRepository.GetAllRecipesAsync();
            return recipes ?? Array.Empty<RecipeDto>();
        }

        public async Task<RecipeDto[]> GetAllRecipesFromStatusAsync(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                throw new ArgumentException("Status cannot be null or empty.", nameof(status));
            }

            var recipes = await _recipeRepository.GetAllRecipesFromStatusAsync(status);
            return recipes ?? Array.Empty<RecipeDto>();
        }

        public async Task<RecipeDto[]> GetAllRecipesFromStatusAndUserAsync(string status, int userId)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                throw new ArgumentException("Status cannot be null or empty.", nameof(status));
            }

            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "UserId must be greater than zero.");
            }

            var recipes = await _recipeRepository.GetAllRecipesFromStatusAndUserAsync(status, userId);
            return recipes ?? Array.Empty<RecipeDto>();
        }

        public async Task<RecipeDto[]> GetAllRecipesFromUserAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "UserId must be greater than zero.");
            }

            var recipes = await _recipeRepository.GetAllRecipesFromUserAsync(userId);
            return recipes ?? Array.Empty<RecipeDto>();
        }

        public async Task<RecipeDto> GetRecipeByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "RecipeId must be greater than zero.");
            }

            var recipe = await _recipeRepository.GetRecipeByIdAsync(id);

            if (recipe == null)
            {
                throw new KeyNotFoundException($"Recipe with ID {id} not found.");
            }

            return recipe;
        }

        public async Task DeleteRecipeByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "RecipeId must be greater than zero.");
            }

            await _recipeRepository.DeleteRecipeByIdAsync(id);
        }

        public async Task UpdateRecipeAsync(RecipeDto recipe)
        {
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe), "Recipe cannot be null.");
            }

            if (recipe.Id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(recipe.Id), "RecipeId must be greater than zero.");
            }

            await _recipeRepository.UpdateRecipeAsync(recipe);
        }

        public async Task<RecipeDto[]> GetFeaturedRecipesAsync(string status, int featuredCount)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                throw new ArgumentException("Status cannot be null or empty.", nameof(status));
            }

            if (featuredCount <= 0)
            {
                throw new ArgumentException("Featured count must be greater than zero.", nameof(featuredCount));
            }

            var recipes = await _recipeRepository.GetAllRecipesFromStatusAsync(status);
            if (recipes == null || recipes.Length == 0)
            {
                return Array.Empty<RecipeDto>();
            }

            return recipes
                .OrderByDescending(recipe => recipe.Likes)
                .Take(featuredCount)
                .ToArray();
        }
    }
}

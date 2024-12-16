using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SocialRecipes.Services.IRepositories;
using SocialRecipes.Services.Services;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;

namespace SocialRecipes.Tests.ServiceTests
{
    [TestClass]
    public class RecipeServiceTests
    {
        private Mock<IRecipeRepository> _mockRecipeRepository;
        private Mock<ILogger<RecipeService>> _mockLogger;
        private RecipeService _recipeService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRecipeRepository = new Mock<IRecipeRepository>();
            _mockLogger = new Mock<ILogger<RecipeService>>();
            _recipeService = new RecipeService(_mockRecipeRepository.Object, _mockLogger.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task AddRecipeAsync_ShouldThrowException_WhenRecipeIsNull()
        {
            await _recipeService.AddRecipeAsync(null);
        }

        [TestMethod]
        public async Task GetAllRecipesAsync_ShouldReturnEmptyArray_WhenNoRecipesExist()
        {
            _mockRecipeRepository.Setup(repo => repo.GetAllRecipesAsync()).ReturnsAsync(Array.Empty<RecipeDto>());

            var result = await _recipeService.GetAllRecipesAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public async Task GetAllRecipesFromStatusAsync_ShouldReturnRecipes_WhenStatusIsValid()
        {
            var status = "Published";
            var recipes = new[] { new RecipeDto { Id = 1, Title = "Test Recipe" } };

            _mockRecipeRepository.Setup(repo => repo.GetAllRecipesFromStatusAsync(status)).ReturnsAsync(recipes);

            var result = await _recipeService.GetAllRecipesFromStatusAsync(status);

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(1, result[0].Id);
        }

        [TestMethod]
        public async Task GetRecipeByIdAsync_ShouldThrowException_WhenRecipeNotFound()
        {
            var id = 1;
            _mockRecipeRepository.Setup(repo => repo.GetRecipeByIdAsync(id)).ReturnsAsync((RecipeDto)null);

            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _recipeService.GetRecipeByIdAsync(id));
        }

        [TestMethod]
        public async Task GetRecipeByIdAsync_ShouldReturnRecipe_WhenRecipeExists()
        {
            var id = 1;
            var recipe = new RecipeDto { Id = id, Title = "Test Recipe" };
            _mockRecipeRepository.Setup(repo => repo.GetRecipeByIdAsync(id)).ReturnsAsync(recipe);

            var result = await _recipeService.GetRecipeByIdAsync(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [TestMethod]
        public async Task DeleteRecipeByIdAsync_ShouldCallRepository_WhenIdIsValid()
        {
            var id = 1;

            await _recipeService.DeleteRecipeByIdAsync(id);

            _mockRecipeRepository.Verify(repo => repo.DeleteRecipeByIdAsync(id), Times.Once);
        }

        [TestMethod]
        public async Task UpdateRecipeAsync_ShouldThrowException_WhenRecipeIdIsInvalid()
        {
            var recipe = new RecipeDto { Id = 0, Title = "Invalid Recipe" };

            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => _recipeService.UpdateRecipeAsync(recipe));
        }

        [TestMethod]
        public async Task UpdateRecipeAsync_ShouldCallRepository_WhenRecipeIsValid()
        {
            var recipe = new RecipeDto { Id = 1, Title = "Valid Recipe" };

            await _recipeService.UpdateRecipeAsync(recipe);

            _mockRecipeRepository.Verify(repo => repo.UpdateRecipeAsync(recipe), Times.Once);
        }

        [TestMethod]
        public async Task GetFeaturedRecipesAsync_ShouldReturnEmptyArray_WhenNoRecipesExist()
        {
            var status = "Published";
            var featuredCount = 3;
            _mockRecipeRepository.Setup(repo => repo.GetAllRecipesFromStatusAsync(status)).ReturnsAsync(Array.Empty<RecipeDto>());

            var result = await _recipeService.GetFeaturedRecipesAsync(status, featuredCount);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }
    }
}

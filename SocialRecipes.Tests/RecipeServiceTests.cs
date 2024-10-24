using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SocialRecipes.Domain.IRepositories;
using SocialRecipes.Domain.IServices;
using SocialRecipes.DTO.General;
using SocialRecipes.DTO.IN;
using SocialRecipes.Services.Services;

namespace SocialRecipes.Tests
{
    [TestClass]
    public class RecipeServiceTests
    {
        private Mock<IRecipeRepository> _mockRecipeRepository;
        private IRecipeService _recipeService;

        [TestInitialize]
        public void Setup()
        {
            _mockRecipeRepository = new Mock<IRecipeRepository>();
            _recipeService = new RecipeService(_mockRecipeRepository.Object);
        }

        [TestMethod]
        public void AddRecipe_Should_Call_Repository_AddRecipe()
        {
            // Arrange
            var recipeDto = new AddRecipeDto { Title = "Chocolate Cake" };

            // Act
            _recipeService.AddRecipe(recipeDto);

            // Assert
            _mockRecipeRepository.Verify(repo => repo.AddRecipe(It.Is<AddRecipeDto>(r => r.Title == "Chocolate Cake")), Times.Once);
        }

        [TestMethod]
        public void GetAllRecipes_Should_Return_AllRecipes_From_Repository()
        {
            // Arrange
            var recipes = new[]
            {
                new RecipeDto { Id = 1, Title = "Recipe 1" },
                new RecipeDto { Id = 2, Title = "Recipe 2" }
            };
            _mockRecipeRepository.Setup(repo => repo.GetAllRecipes()).Returns(recipes);

            // Act
            var result = _recipeService.GetAllRecipes();

            // Assert
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("Recipe 1", result[0].Title);
            Assert.AreEqual("Recipe 2", result[1].Title);
        }

        [TestMethod]
        public void GetAllRecipesFromStatus_Should_Call_Repository_With_Correct_Status()
        {
            // Arrange
            var status = "Published";
            var recipes = new[]
            {
                new RecipeDto { Id = 1, Title = "Recipe 1", Status = status },
                new RecipeDto { Id = 2, Title = "Recipe 2", Status = status }
            };
            _mockRecipeRepository.Setup(repo => repo.GetAllRecipesFromStatus(status)).Returns(recipes);

            // Act
            var result = _recipeService.GetAllRecipesFromStatus(status);

            // Assert
            Assert.AreEqual(2, result.Length);
            _mockRecipeRepository.Verify(repo => repo.GetAllRecipesFromStatus(status), Times.Once);
        }

        [TestMethod]
        public void GetRecipeById_Should_Return_Recipe_From_Repository()
        {
            // Arrange
            int recipeId = 1;
            var recipe = new RecipeDto { Id = recipeId, Title = "Recipe 1" };

            _mockRecipeRepository.Setup(repo => repo.GetRecipeById(recipeId)).Returns(recipe);

            // Act
            var result = _recipeService.GetRecipeById(recipeId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(recipeId, result.Id);
        }

        [TestMethod]
        public void DeleteRecipeFromId_Should_Call_Repository_DeleteRecipeFromId()
        {
            // Arrange
            int recipeId = 1;

            // Act
            _recipeService.DeleteRecipeFromId(recipeId);

            // Assert
            _mockRecipeRepository.Verify(repo => repo.DeleteRecipeFromId(It.Is<int>(id => id == recipeId)), Times.Once);
        }

        [TestMethod]
        public void UpdateRecipe_Should_Call_Repository_UpdateRecipe()
        {
            // Arrange
            var recipeDto = new RecipeDto { Id = 1, Title = "Updated Recipe" };

            // Act
            _recipeService.UpdateRecipe(recipeDto);

            // Assert
            _mockRecipeRepository.Verify(repo => repo.UpdateRecipe(It.Is<RecipeDto>(r => r.Title == "Updated Recipe")), Times.Once);
        }

        [TestMethod]
        public void GetAllRecipesFromUser_Should_Return_Recipes_For_User_From_Repository()
        {
            // Arrange
            int userId = 1;
            var recipes = new[]
            {
                new RecipeDto { Id = 1, Title = "Recipe 1", UserId = userId },
                new RecipeDto { Id = 2, Title = "Recipe 2", UserId = userId }
            };
            _mockRecipeRepository.Setup(repo => repo.GetAllRecipesFromUser(userId)).Returns(recipes);

            // Act
            var result = _recipeService.GetAllRecipesFromUser(userId);

            // Assert
            Assert.AreEqual(2, result.Length);
            Assert.IsTrue(result.All(r => r.UserId == userId));
        }

        [TestMethod]
        public void GetAllRecipesFromStatusAndUser_Should_Return_Recipes_From_Status_And_User()
        {
            // Arrange
            int userId = 1;
            string status = "Published";
            var recipes = new[]
            {
                new RecipeDto { Id = 1, Title = "Recipe 1", Status = status, UserId = userId },
                new RecipeDto { Id = 2, Title = "Recipe 2", Status = status, UserId = userId }
            };
            _mockRecipeRepository.Setup(repo => repo.GetAllRecipesFromStatusAndUser(status, userId)).Returns(recipes);

            // Act
            var result = _recipeService.GetAllRecipesFromStatusAndUser(status, userId);

            // Assert
            Assert.AreEqual(2, result.Length);
            Assert.IsTrue(result.All(r => r.Status == status && r.UserId == userId));
        }
    }
}

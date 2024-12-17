using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SocialRecipes.API.Controllers;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Services.IRepositories;
using SocialRecipes.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialRecipes.Tests.ControllerTests
{
    [TestClass]
    public class CommentControllerTests
    {
        private Mock<ICommentRepository> _mockCommentRepository;
        private CommentService _commentService;
        private CommentController _commentController;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockCommentRepository = new Mock<ICommentRepository>();
            var logger = new Mock<ILogger<CommentController>>();
            _commentService = new CommentService(_mockCommentRepository.Object);
            _commentController = new CommentController(logger.Object, _commentService);
        }

        [TestMethod]
        public async Task GetCommentsByRecipe_ShouldReturnOk_WhenCommentsExist()
        {
            // Test ensures that a 200 status code is returned when comments exist for a recipe
            // Test makes sure the right amount of comments are returned for a recipe
            // Arrange
            var recipeId = 1;
            var comments = new[]
            {
                new CommentDto { UserId = 1, RecipeId = recipeId, Content = "Comment Test" },
                new CommentDto { UserId = 2, RecipeId = recipeId, Content = "Comment Test 2" },
                new CommentDto { UserId = 3, RecipeId = 2, Content = "Comment Test 2" }
            };

            _mockCommentRepository.Setup(repo => repo.GetCommentsByRecipeIdAsync(recipeId)).ReturnsAsync(comments.Where(c => c.RecipeId == recipeId).ToArray());


            // Act
            var result = await _commentController.GetCommentsByRecipe(recipeId) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var response = result.Value as IEnumerable<CommentDto>;
            Assert.AreEqual(2, response.Count());

            _mockCommentRepository.Verify(repo => repo.GetCommentsByRecipeIdAsync(recipeId), Times.Once);
        }
    }
}

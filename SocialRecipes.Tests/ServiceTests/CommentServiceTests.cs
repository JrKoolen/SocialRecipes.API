using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SocialRecipes.Services.IRepositories;
using SocialRecipes.Services.Services;
using SocialRecipes.Domain.Dto.General;
using System;
using System.Threading.Tasks;

namespace SocialRecipes.Tests.ServiceTests
{
    [TestClass]
    public class CommentServiceTests
    {
        private Mock<ICommentRepository> _mockCommentRepository;
        private CommentService _commentService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockCommentRepository = new Mock<ICommentRepository>();
            _commentService = new CommentService(_mockCommentRepository.Object);
        }

        [TestMethod]
        public async Task AddCommentAsync_ShouldCallRepository_WhenCommentIsValid()
        {
            // Arrange
            var comment = new CommentDto { Id = 1, Content = "Great recipe!" };

            // Act
            await _commentService.AddCommentAsync(comment);

            // Assert
            _mockCommentRepository.Verify(repo => repo.AddCommentAsync(comment), Times.Once);
        }

        [TestMethod]
        [DataRow(0, DisplayName = "Zero RecipeId")]
        [DataRow(-1, DisplayName = "Negative RecipeId")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task GetCommentsByRecipeIdAsync_ShouldThrowArgumentOutOfRangeException_WhenRecipeIdIsInvalid(int recipeId)
        {
            await _commentService.GetCommentsByRecipeIdAsync(recipeId);
        }

        [TestMethod]
        public async Task GetCommentsByRecipeIdAsync_ShouldReturnEmptyArray_WhenNoCommentsExist()
        {
            // Arrange
            _mockCommentRepository.Setup(repo => repo.GetCommentsByRecipeIdAsync(It.IsAny<int>())).ReturnsAsync((CommentDto[])null);

            // Act
            var result = await _commentService.GetCommentsByRecipeIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public async Task DeleteCommentByIdAsync_ShouldCallRepository_WhenCommentIdIsValid()
        {
            // Arrange
            var commentId = 1;

            // Act
            await _commentService.DeleteCommentByIdAsync(commentId);

            // Assert
            _mockCommentRepository.Verify(repo => repo.DeleteCommentByIdAsync(commentId), Times.Once);
        }
    }
}

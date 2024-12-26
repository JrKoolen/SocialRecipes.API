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
    public class FollowControllerTests
    {
        private Mock<IFollowerRepository> _mockFollowerRepository;
        private Mock<ILogger<FollowController>> _mockLogger;
        private FollowService _followService;
        private FollowController _followController;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockFollowerRepository = new Mock<IFollowerRepository>();
            _mockLogger = new Mock<ILogger<FollowController>>();
            _followService = new FollowService(_mockFollowerRepository.Object);
            _followController = new FollowController(_mockLogger.Object, _followService);
        }

        [TestMethod]
        public async Task FollowAsync_ShouldReturnBadRequest_WhenArgumentExceptionOccurs()
        {
            // Arrange
            var userId = 1;
            var followerId = 2;

            _mockFollowerRepository.Setup(repo => repo.FollowAsync(userId, followerId))
                                   .ThrowsAsync(new ArgumentException("Invalid operation."));

            // Act
            var result = await _followController.FollowAsync(userId, followerId) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            dynamic response = result.Value;

            _mockFollowerRepository.Verify(repo => repo.FollowAsync(userId, followerId), Times.Once);
        }

        [TestMethod]
        public async Task RemoveFollowAsync_ShouldReturnBadRequest_WhenArgumentExceptionOccurs()
        {
            // Arrange
            var userId = 1;
            var followerId = 2;

            _mockFollowerRepository.Setup(repo => repo.RemoveFollowAsync(userId, followerId))
                                   .ThrowsAsync(new ArgumentException("Cannot unfollow yourself."));

            // Act
            var result = await _followController.RemoveFollowAsync(userId, followerId) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode); 
            dynamic response = result.Value;

            _mockFollowerRepository.Verify(repo => repo.RemoveFollowAsync(userId, followerId), Times.Once);
        }
    }
}

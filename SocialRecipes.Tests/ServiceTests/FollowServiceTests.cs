using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SocialRecipes.Services.IRepositories;
using SocialRecipes.Services.Services;

namespace SocialRecipes.Tests.ServiceTests
{
    [TestClass]
    public class FollowServiceTests
    {
        private Mock<IFollowerRepository> _mockFollowerRepository;
        private Mock<ILogger<FollowService>> _mockLogger;
        private FollowService _followService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockFollowerRepository = new Mock<IFollowerRepository>();
            _mockLogger = new Mock<ILogger<FollowService>>();
            _followService = new FollowService(_mockFollowerRepository.Object, _mockLogger.Object);
        }

        [TestMethod]
        [DataRow(null, null, typeof(ArgumentNullException), DisplayName = "Null UserId and FollowerId")]
        [DataRow(null, 1, typeof(ArgumentNullException), DisplayName = "Null UserId")]
        [DataRow(1, null, typeof(ArgumentNullException), DisplayName = "Null FollowerId")]
        [DataRow(0, 1, typeof(ArgumentOutOfRangeException), DisplayName = "Zero UserId")]
        [DataRow(1, 0, typeof(ArgumentOutOfRangeException), DisplayName = "Zero FollowerId")]
        [DataRow(-1, 1, typeof(ArgumentOutOfRangeException), DisplayName = "Negative UserId")]
        [DataRow(1, -1, typeof(ArgumentOutOfRangeException), DisplayName = "Negative FollowerId")]
        public async Task FollowAsync_ShouldThrowException_WhenInputsAreInvalid(int? userId, int? followerId, Type expectedExceptionType)
        {
            // Act
            Exception exception = null;
            try
            {
                await _followService.FollowAsync(userId, followerId);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            // Assert
            Assert.IsNotNull(exception, "Expected an exception to be thrown.");
            Assert.IsInstanceOfType(exception, expectedExceptionType, $"Expected exception of type {expectedExceptionType}, but got {exception.GetType()}.");

            if (expectedExceptionType == typeof(ArgumentNullException))
            {
                Assert.AreEqual("Value cannot be null. (Parameter 'UserId or FollowerId cannot be null.')", exception.Message);
            }
            else if (expectedExceptionType == typeof(ArgumentOutOfRangeException))
            {
                Assert.AreEqual("Specified argument was out of the range of valid values. (Parameter 'UserId or FollowerId must be greater than zero.')", exception.Message);
            }
        }

        [TestMethod]
        [DataRow(null, null, typeof(ArgumentNullException), DisplayName = "Null UserId and FollowerId")]
        [DataRow(null, 1, typeof(ArgumentNullException), DisplayName = "Null UserId")]
        [DataRow(1, null, typeof(ArgumentNullException), DisplayName = "Null FollowerId")]
        [DataRow(-1, 1, typeof(ArgumentOutOfRangeException), DisplayName = "Negative UserId")]
        [DataRow(1, -1, typeof(ArgumentOutOfRangeException), DisplayName = "Negative FollowerId")]
        [DataRow(0, 1, typeof(ArgumentOutOfRangeException), DisplayName = "Zero UserId")]
        [DataRow(1, 0, typeof(ArgumentOutOfRangeException), DisplayName = "Zero FollowerId")]
        public async Task RemoveFollowAsync_ShouldThrowException_WhenInputsAreInvalid(int? userId, int? followerId, Type expectedExceptionType)
        {
            // Act
            Exception thrownException = null;

            try
            {
                await _followService.RemoveFollowAsync(userId, followerId);
            }
            catch (Exception ex)
            {
                thrownException = ex;
            }

            // Assert
            Assert.IsNotNull(thrownException, "Expected an exception to be thrown.");
            Assert.IsInstanceOfType(thrownException, expectedExceptionType,
                $"Expected exception of type {expectedExceptionType}, but got {thrownException.GetType()}.");
        }


        [TestMethod]
        public async Task FollowAsync_ShouldReturnFalse_WhenUserFollowsSelf()
        {
            var userId = 1;

            // Act 
            var exception = await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _followService.FollowAsync(userId, userId));

            // Assert
            Assert.AreEqual("A user cannot follow themselves.", exception.Message, "Expected specific error message for self-following.");
        }

        [TestMethod]
        [DataRow(1, 2, DisplayName = "Valid UserId and FollowerId")]
        //[DataRow(1, 1, DisplayName = "UserId equal to FollowerId (self-follow)")]
        public async Task FollowAsync_ShouldHandleVariousScenarios(int userId, int followerId)
        {
            // Arrange
            if (userId != followerId && userId > 0 && followerId > 0)
            {
                _mockFollowerRepository
                    .Setup(repo => repo.FollowAsync(userId, followerId))
                    .Returns(Task.CompletedTask);
            }

            // Act & Assert
            if (userId == followerId || userId <= 0 || followerId <= 0)
            {
                // Expecting InvalidOperationException for invalid scenarios
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _followService.FollowAsync(userId, followerId));
            }
            else
            {
                // Valid case
                await _followService.FollowAsync(userId, followerId);

                // Verify that the repository method was called exactly once
                _mockFollowerRepository.Verify(repo => repo.FollowAsync(userId, followerId), Times.Once);
            }
        }
        [TestMethod]
        [DataRow(1, 1, DisplayName = "UserId equal to FollowerId (self-follow)")]
        public async Task RemoveFollowAsync_ShouldReturnFalse_WhenUserFollowsSelf(int userId, int followerId)
        {
            // Act 
            var exception = await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _followService.RemoveFollowAsync(userId, followerId));

            // Assert
            Assert.AreEqual("A user cannot unfollow themselves.", exception.Message, "Expected specific error message for self-unfollowing.");
        }
    }
}

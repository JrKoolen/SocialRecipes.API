using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SocialRecipes.Services.IRepositories;
using SocialRecipes.Services.Services;

namespace SocialRecipes.Tests
{
    [TestClass]
    public class FollowTests
    {
        private Mock<IFollowerRepository> _mockFollowerRepository;
        private FollowService _followService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockFollowerRepository = new Mock<IFollowerRepository>();
            _followService = new FollowService(_mockFollowerRepository.Object);
        }

        [TestMethod]
        [DataRow(null, null, false, DisplayName = "Null UserId and FollowerId")]
        [DataRow(null, 1, false, DisplayName = "Null UserId")]
        [DataRow(1, null, false, DisplayName = "Null FollowerId")]
        [DataRow(-1, 1, false, DisplayName = "Negative UserId")]
        [DataRow(1, -1, false, DisplayName = "Negative FollowerId")]
        [DataRow(0, 1, false, DisplayName = "Zero UserId")]
        [DataRow(1, 0, false, DisplayName = "Zero FollowerId")]
        [DataRow(1, 2, true, DisplayName = "Valid UserId and FollowerId")]
        public async Task FollowAsync_ShouldHandleVariousInputs(int? userId, int? followerId, bool expectedResult)
        {
            // Arrange
            if (userId.HasValue && followerId.HasValue && userId > 0 && followerId > 0)
            {
                // Simulate successful repository call
                _mockFollowerRepository
                    .Setup(repo => repo.FollowAsync(userId.Value, followerId.Value))
                    .Returns(Task.CompletedTask);
            }

            // Act
            var result = await _followService.FollowAsync(userId, followerId);

            // Assert
            Assert.AreEqual(expectedResult, result, "The FollowAsync method did not return the expected result.");
        }

        [TestMethod]
        public async Task FollowAsync_ShouldReturnFalse_WhenUserFollowsSelf()
        {
            // Arrange
            var userId = 1;

            // Act
            var result = await _followService.FollowAsync(userId, userId);

            // Assert
            Assert.IsFalse(result, "A user should not be able to follow themselves.");
        }


        [TestMethod]
        [DataRow(null, null, false, DisplayName = "Null UserId and FollowerId")]
        [DataRow(null, 1, false, DisplayName = "Null UserId")]
        [DataRow(1, null, false, DisplayName = "Null FollowerId")]
        [DataRow(-1, 1, false, DisplayName = "Negative UserId")]
        [DataRow(1, -1, false, DisplayName = "Negative FollowerId")]
        [DataRow(0, 1, false, DisplayName = "Zero UserId")]
        [DataRow(1, 0, false, DisplayName = "Zero FollowerId")]
        public async Task RemoveFollowAsync_ShouldReturnFalse_WhenInputsAreInvalid(int? userId, int? followerId, bool expectedResult)
        {
            // Act
            var result = await _followService.RemoveFollowAsync(userId ?? 0, followerId ?? 0);

            // Assert
            Assert.AreEqual(expectedResult, result, "RemoveFollowAsync did not return the expected result for invalid inputs.");
        }
    }
}

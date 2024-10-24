using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SocialRecipes.Domain.IRepositories;
using SocialRecipes.Domain.IServices;
using SocialRecipes.DTO.General;
using SocialRecipes.Services.Services;

namespace SocialRecipes.Tests
{
    [TestClass]
    public class FollowerServiceTests
    {
        private Mock<IFollowerRepository> _mockFollowerRepository;
        private IFollowerService _followerService;

        [TestInitialize]
        public void Setup()
        {
            _mockFollowerRepository = new Mock<IFollowerRepository>();
            _followerService = new FollowerService(_mockFollowerRepository.Object);
        }

        [TestMethod]
        public void Follow_Should_Call_Repository_Follow_And_Return_True_On_Success()
        {
            // Arrange
            int userId = 1;
            int followerId = 2;

            // Act
            var result = _followerService.Follow(userId, followerId);

            // Assert
            _mockFollowerRepository.Verify(repo => repo.Follow(It.Is<int>(u => u == userId), It.Is<int>(f => f == followerId)), Times.Once);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Follow_Should_Return_False_When_Exception_Occurs()
        {
            // Arrange
            int userId = 1;
            int followerId = 2;
            _mockFollowerRepository.Setup(repo => repo.Follow(userId, followerId)).Throws(new System.Exception());

            // Act
            var result = _followerService.Follow(userId, followerId);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetFollowers_Should_Return_Followers_From_Repository()
        {
            // Arrange
            int userId = 1;
            var followers = new[]
            {
                new UserDto { Id = 2, Name = "Follower 1" },
                new UserDto { Id = 3, Name = "Follower 2" }
            };
            _mockFollowerRepository.Setup(repo => repo.GetFollowers(userId)).Returns(followers);

            // Act
            var result = _followerService.GetFollowers(userId);

            // Assert
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("Follower 1", result[0].Name);
            Assert.AreEqual("Follower 2", result[1].Name);
        }

        [TestMethod]
        public void GetFollowing_Should_Return_Following_From_Repository()
        {
            // Arrange
            int userId = 1;
            var following = new[]
            {
                new UserDto { Id = 2, Name = "Following 1" },
                new UserDto { Id = 3, Name = "Following 2" }
            };
            _mockFollowerRepository.Setup(repo => repo.GetFollowing(userId)).Returns(following);

            // Act
            var result = _followerService.GetFollowing(userId);

            // Assert
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("Following 1", result[0].Name);
            Assert.AreEqual("Following 2", result[1].Name);
        }

        [TestMethod]
        public void RemoveFollow_Should_Call_Repository_RemoveFollow_And_Return_True_On_Success()
        {
            // Arrange
            int userId = 1;
            int followerId = 2;

            // Act
            var result = _followerService.RemoveFollow(userId, followerId);

            // Assert
            _mockFollowerRepository.Verify(repo => repo.RemoveFollow(It.Is<int>(u => u == userId), It.Is<int>(f => f == followerId)), Times.Once);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RemoveFollow_Should_Return_False_When_Exception_Occurs()
        {
            // Arrange
            int userId = 1;
            int followerId = 2;

            _mockFollowerRepository.Setup(repo => repo.RemoveFollow(userId, followerId)).Throws(new System.Exception());

            // Act
            var result = _followerService.RemoveFollow(userId, followerId);

            // Assert
            Assert.IsFalse(result);
        }
    }
}

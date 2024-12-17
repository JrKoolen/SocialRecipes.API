using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SocialRecipes.Services.IRepositories;
using SocialRecipes.Services.Services;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SocialRecipes.Tests.ServiceTests
{
    [TestClass]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private UserService _userService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _userService = new UserService(_mockUserRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CreateUserAsync_ShouldThrowArgumentNullException_WhenUserInputIsNull()
        {
            await _userService.CreateUserAsync(null);
        }

        [TestMethod]
        public async Task CreateUserAsync_ShouldCallRepository_WhenUserInputIsValid()
        {
            // Arrange
            var userInput = new AddUserDto { Name = "JohnDoe", Email = "johndoe@example.com" };

            // Act
            await _userService.CreateUserAsync(userInput);

            // Assert
            _mockUserRepository.Verify(repo => repo.AddUserAsync(userInput), Times.Once);
        }

        [TestMethod]
        [DataRow(0, DisplayName = "Zero UserId")]
        [DataRow(-1, DisplayName = "Negative UserId")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task DeleteUserByIdAsync_ShouldThrowArgumentOutOfRangeException_WhenIdIsInvalid(int id)
        {
            await _userService.DeleteUserByIdAsync(id);
        }

        [TestMethod]
        public async Task DeleteUserByIdAsync_ShouldCallRepository_WhenIdIsValid()
        {
            // Arrange
            var userId = 1;

            // Act
            await _userService.DeleteUserByIdAsync(userId);

            // Assert
            _mockUserRepository.Verify(repo => repo.DeleteUserByIdAsync(userId), Times.Once);
        }

        [TestMethod]
        [DataRow(0, DisplayName = "Zero UserId")]
        [DataRow(-1, DisplayName = "Negative UserId")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task GetUserByIdAsync_ShouldThrowArgumentOutOfRangeException_WhenIdIsInvalid(int id)
        {
            await _userService.GetUserByIdAsync(id);
        }

        [TestMethod]
        public async Task GetUserByIdAsync_ShouldThrowKeyNotFoundException_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = 1;
            _mockUserRepository.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync((UserDto)null);

            // Act & Assert
            var exception = await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _userService.GetUserByIdAsync(userId));
            Assert.AreEqual($"User with ID {userId} not found.", exception.Message);
        }

        [TestMethod]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userId = 1;
            var expectedUser = new UserDto { Id = userId, Name = "JohnDoe" };

            _mockUserRepository
                .Setup(repo => repo.GetUserByIdAsync(userId))
                .ReturnsAsync(expectedUser);

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUser.Id, result.Id);
            Assert.AreEqual(expectedUser.Name, result.Name);

            _mockUserRepository.Verify(repo => repo.GetUserByIdAsync(userId), Times.Once);
        }
    }
}

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
    public class UserServiceTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private IUserService _userService;

        [TestInitialize]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _userService = new UserService(_mockUserRepository.Object);
        }

        [TestMethod]
        public void CreateUser_Should_Call_Repository_AddUser()
        {
            // Arrange
            var userInput = new AddUserDto { Name = "John Doe" };

            // Act
            _userService.CreateUser(userInput);

            // Assert
            _mockUserRepository.Verify(repo => repo.AddUser(It.Is<AddUserDto>(u => u.Name == "John Doe")), Times.Once);
        }

        [TestMethod]
        public void DeleteUserById_Should_Call_Repository_DeleteUserById()
        {
            // Arrange
            int userId = 1;

            // Act
            _userService.DeleteUserById(userId);

            // Assert
            _mockUserRepository.Verify(repo => repo.DeleteUserById(It.Is<int>(id => id == userId)), Times.Once);
        }

        [TestMethod]
        public void GetUserById_Should_Return_User_From_Repository()
        {
            // Arrange
            int userId = 1;
            var userDto = new UserDto { Id = userId, Name = "John Doe" };

            _mockUserRepository.Setup(repo => repo.GetUserById(userId)).Returns(userDto);

            // Act
            var result = _userService.GetUserById(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userId, result.Id);
            Assert.AreEqual("John Doe", result.Name);
        }

        [TestMethod]
        public void GetUserById_Should_Return_Null_If_User_Not_Found()
        {
            // Arrange
            int userId = 1;
            _mockUserRepository.Setup(repo => repo.GetUserById(userId)).Returns((UserDto)null);

            // Act
            var result = _userService.GetUserById(userId);

            // Assert
            Assert.IsNull(result);
        }
    }
}

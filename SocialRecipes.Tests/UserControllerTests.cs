using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SocialRecipes.API.Controllers;
using SocialRecipes.Domain.IServices;
using SocialRecipes.DTO.IN;
using SocialRecipes.DTO.General;

namespace SocialRecipes.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        private UserController _controller;
        private Mock<IUserService> _mockUserService;
        private Mock<ILogger<UserController>> _mockLogger;

        [TestInitialize]
        public void Setup()
        {
            _mockUserService = new Mock<IUserService>();
            _mockLogger = new Mock<ILogger<UserController>>();
            _controller = new UserController(_mockLogger.Object, _mockUserService.Object);
        }

        [TestMethod]
        public void CreateUser_Should_Return_BadRequest_When_User_Is_Null()
        {
            // Act
            var result = _controller.CreateUser(null);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("User is null.", badRequestResult.Value);
        }


        [TestMethod]
        public void CreateUser_Should_Return_BadRequest_When_UserName_Is_Empty()
        {
            // Arrange
            var userDto = new AddUserDto { Name = string.Empty };

            // Act
            var result = _controller.CreateUser(userDto);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("User name is required.", badRequestResult.Value);
        }

        [TestMethod]
        public void CreateUser_Should_Return_Ok_When_Valid_User()
        {
            // Arrange
            var userDto = new AddUserDto { Name = "John Doe" };

            _mockUserService.Setup(x => x.CreateUser(It.IsAny<AddUserDto>())).Verifiable();

            // Act
            var result = _controller.CreateUser(userDto);

            // Assert
            Assert.IsNotNull(result, "The result returned by CreateUser is null.");
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult, "The result is not of type OkObjectResult.");
            var response = okResult.Value as AddUserDto;
           // Assert.IsNotNull(response, "The response inside OkObjectResult is null.");
           // Assert.AreEqual(userDto.Name, response.Name);
        }




        [TestMethod]
        public void DeleteUser_Should_Return_NotFound_When_User_Does_Not_Exist()
        {
            // Arrange
            int userId = 123;
            _mockUserService.Setup(x => x.GetUserById(userId)).Returns((UserDto)null);

            // Act
            var result = _controller.DeleteUser(userId);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
//          Assert.AreEqual("User not found", ((dynamic)notFoundResult.Value).message);
//            Assert.AreEqual(userId, ((dynamic)notFoundResult.Value).id);
        }

        [TestMethod]
        public void DeleteUser_Should_Return_Ok_When_User_Is_Deleted()
        {
            // Arrange
            int userId = 123;
            var userDto = new UserDto { Id = userId, Name = "John Doe" };
            _mockUserService.Setup(x => x.GetUserById(userId)).Returns(userDto);
            _mockUserService.Setup(x => x.DeleteUserById(userId)).Verifiable();

            // Act
            var result = _controller.DeleteUser(userId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
//            Assert.AreEqual("User deleted", ((dynamic)okResult.Value).message);
    //        Assert.AreEqual(userId, ((dynamic)okResult.Value).id);
        }
    }
}

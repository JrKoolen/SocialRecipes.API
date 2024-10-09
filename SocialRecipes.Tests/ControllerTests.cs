using SocialRecipes.API;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using SocialRecipes.Domain.IServices;
using SocialRecipes.Domain.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialRecipes.API.Controllers;
using Moq;

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
    }
}


﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Options;
using SocialRecipes.API.Controllers;
using SocialRecipes.Services.Services;
using SocialRecipes.Infrastructure.Settings;
using Microsoft.Extensions.DependencyModel;
using SocialRecipes.DAL.Repositories;
using SocialRecipes.Services.IRepositories;
using SocialRecipes.Domain.Dto.IN;
using SocialRecipes.Domain.Dto.General;
using System.Reflection;

namespace SocialRecipes.Tests.UnitTests
{
    [TestClass]
    public class AuthFlowTests
    {
        // mock the logger, service layer, jwtsettings for the controller and mock the repository interface for the auth service.
        private Mock<ILogger<AuthController>> _logger;
        private Mock<AuthService> _authService;
        private Mock<IOptions<JwtSettings>> _mockJwtSettings;
        private AuthController _authController;
        private Mock<IAuthRepository> _authRepository;
        private AuthService __authService;


        [TestInitialize]
        public void Initialize()
        {
            //controller moq
            _logger = new Mock<ILogger<AuthController>>();
            _mockJwtSettings = new Mock<IOptions<JwtSettings>>();
            _mockJwtSettings.Setup(m => m.Value).Returns(new JwtSettings { });
            _authService = new Mock<AuthService>(_authRepository);
            _authController = new AuthController(_logger.Object, _mockJwtSettings.Object, _authService.Object);

            //service layer moq
            _authRepository = new Mock<IAuthRepository>();
            __authService = new AuthService(_authRepository.Object);

        }

        // check if the moq was succesfull
        [TestMethod]
        public void Test_AuthController_Should_Return_AuthController()
        {
            Assert.IsNotNull(_authController);
        }

        // check if the controller is of the correct type
        [TestMethod]
        public void Test_if_AuthController_Is_Of_The_Correct_Types()
        {
            Assert.IsInstanceOfType(_authController, typeof(ControllerBase));
            Assert.IsInstanceOfType(_authController, typeof(AuthController));
        }

        // check if the requirements of the constructers are not null
        [TestMethod]
        public void Test_if_The_AuthController_ConstructorRequirements_Are_Not_Null()
        {
            Assert.IsNotNull(_authController);
            Assert.IsNotNull(_logger);
            Assert.IsNotNull(_authService);
            Assert.IsNotNull(_mockJwtSettings);
        }

        // check if the output of the controller are all being used and present.
        [TestMethod]
        public async Task Test_LoginAsync_Should_Return_OK_When_Credentials_Are_Valid()
        {
            // Arrange
            LoginDto loginDto = new LoginDto
            {
                Name = "Test",
                Password = "test"
            };

            _authService.Setup(service => service.LoginAsync(It.IsAny<LoginDto>())).ReturnsAsync(new UserDto { Id = 1, Name = "Test" });


            // Act
            var result = await _authController.LoginAsync(loginDto);

            // Assert
            // test if the function was called 
            _authService.Verify(service => service.LoginAsync(It.IsAny<LoginDto>()), Times.Once);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [TestMethod]
        public async Task Test_LoginAsync_Should_Return_Unauthorized_If_Credentials_Are_Invalid()
        {
            // Arrange
            LoginDto loginDto = new LoginDto
            {
                Name = "Invalid",
                Password = "Invalid"
            };

            _authService.Setup(service => service.LoginAsync(It.IsAny<LoginDto>())).ReturnsAsync((UserDto)null);

            // Act
            var result = await _authController.LoginAsync(loginDto);

            // Assert
            var UnauthorizedResult = result as UnauthorizedObjectResult;
            Assert.IsNotNull(UnauthorizedResult);
            Assert.AreEqual(401, UnauthorizedResult.StatusCode);
        }

        [TestMethod]
        public async Task Test_LoginAsync_Should_Return_InternalServerError_When_Exception_Is_Thrown()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Name = "ValidName",
                Password = "ValidPassword"
            };

            _authService.Setup(service => service.LoginAsync(It.IsAny<LoginDto>())).ThrowsAsync(new Exception("Unexpected error"));

            // Act
            var result = await _authController.LoginAsync(loginDto);

            // Assert
            var statusCodeResult = result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        // test if the service layer is calling the repository
        [TestMethod]
        public async Task Test_LoginAsync_Should_Call_Repository_LoginAsync()
        {
            // Arrange
            LoginDto loginDto = new LoginDto
            {
                Name = "Test",
                Password = "Test"
            };

            _authRepository.Setup(repo => repo.LoginAsync(It.IsAny<LoginDto>()))
                               .ReturnsAsync(new UserDto { Id = 1, Name = "Test" });

            // Act
            var result = await __authService.LoginAsync(loginDto);

            // Assert
            _authRepository.Verify(repo => repo.LoginAsync(It.IsAny<LoginDto>()), Times.Once);

            Assert.IsNotNull(result);
            Assert.AreEqual("Test", result.Name);
        }

        // Dto tests
        [TestMethod]
        public void Test_LoginDto_Should_Return_LoginDto()
        {
            LoginDto loginDto = new LoginDto
            {
                Name = "Test",
                Password = "Test"
            };

            Assert.IsNotNull(loginDto);
            Assert.AreEqual("Test", loginDto.Name);
            Assert.AreEqual("Test", loginDto.Password);
        }

        [TestMethod]
        public void Test_UserDto_Should_Return_UserDto()
        {
            UserDto userDto = new UserDto
            {
                Id = 1,
                Name = "Test",
                Email = "Test"
            };

            Assert.IsNotNull(userDto);
            Assert.AreEqual(1, userDto.Id);
            Assert.AreEqual("Test", userDto.Name);
            Assert.AreEqual("Test", userDto.Email);
        }

        // consistency test
        [TestMethod]
        public void Test_UserDto_And_LoginDto_Should_Have_Consistent_namings()
        {
            var loginDtoProperties = typeof(LoginDto).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var userDtoProperties = typeof(UserDto).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Act & Assert
            foreach (var loginDtoProperty in loginDtoProperties)
            {
                var matchingProperty = userDtoProperties
                    .FirstOrDefault(u => u.Name == loginDtoProperty.Name);

                // Check if the properties exist in UserDto
                Assert.IsNotNull(matchingProperty, $"Property '{loginDtoProperty.Name}' found in LoginDto, but not in UserDto.");
            }
        }

        // Register tests 
        [DataTestMethod]
        [DataRow]
        [DataRow(null, "user3@outlook.com", "Password3", false)] 
        [DataRow("User4", null, "Password4", false)] 
        [DataRow("User5", "user5@outlook.com", null, false)]
        public void Test_RegisterAsync_Should_Call_Repository_RegisterAsync(string name, string email, string password, bool expectedResult)
        {
            // Arrange
            AddUserDto addUserDto = new AddUserDto
            {
                Name = name,
                Email = email,
                Password = password
            };

            _authRepository.Setup(repo => repo.RegisterAsync(It.IsAny<AddUserDto>())).ReturnsAsync(expectedResult);

            // Act
            var result = __authService.RegisterAsync(addUserDto);

            // Assert
            _authRepository.Verify(repo => repo.RegisterAsync(It.IsAny<AddUserDto>()), Times.Once);
            Assert.AreEqual(expectedResult, result.Result);
        }
    }
}
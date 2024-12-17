using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SocialRecipes.API.Controllers;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;
using SocialRecipes.Services.IRepositories;
using SocialRecipes.Services.Services;
using System.Threading.Tasks;

namespace SocialRecipes.Tests.ControllerTests
{
    [TestClass]
    public class UserControllerTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<ILogger<UserController>> _mockLogger;
        private UserService _userService;
        private UserController _userController;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockLogger = new Mock<ILogger<UserController>>();

            _userService = new UserService(_mockUserRepository.Object);
            _userController = new UserController(_mockLogger.Object, _userService);
        }
    }
}

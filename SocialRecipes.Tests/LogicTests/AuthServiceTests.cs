using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;
using SocialRecipes.Services.IRepositories;
using SocialRecipes.Services.Services;
using System.Threading.Tasks;

[TestClass]
public class AuthServiceTests
{
    private Mock<IAuthRepository> _mockAuthRepository;
    private AuthService _authService;

    [TestInitialize]
    public void Setup()
    {
        _mockAuthRepository = new Mock<IAuthRepository>();
        _authService = new AuthService(_mockAuthRepository.Object);
    }

    [TestMethod]
    public async Task LoginAsync_ValidCredentials_ReturnsUserDto()
    {
        // Arrange
        LoginDto loginDto = new LoginDto { Name = "test", Password = "test" };
        UserDto expectedUserDto = new UserDto { Id = 1, Name = "test" };

        _mockAuthRepository.Setup(repo => repo.LoginAsync(loginDto))
                           .ReturnsAsync(expectedUserDto);

        // Act
        var result = await _authService.LoginAsync(loginDto);



        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedUserDto, result);
        // ensure there is no unexpected behaviour in the mock repository
        _mockAuthRepository.Verify(repo => repo.LoginAsync(It.IsAny<LoginDto>()), Times.Once);
        _mockAuthRepository.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task RegisterAsync_ValidUser_ReturnsTrue()
    {
        // Arrange
        AddUserDto addUserDto = new AddUserDto { Name = "test", Email = "test", Password = "test" };
        _mockAuthRepository.Setup(repo => repo.RegisterAsync(addUserDto))
                           .ReturnsAsync(true);

        // Act
        var result = await _authService.RegisterAsync(addUserDto);

        // Assert
        Assert.IsTrue(result);

        // Ensure the register assync was called once with user details
        _mockAuthRepository.Verify(repo => repo.RegisterAsync(It.Is<AddUserDto>(dto => 
        dto.Name == "test" && dto.Password == "test" && dto.Email == "test")), Times.Once);
        // Ensure no other calls in the repository
        _mockAuthRepository.VerifyNoOtherCalls();
    }
}

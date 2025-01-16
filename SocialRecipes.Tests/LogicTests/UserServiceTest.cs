using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;
using SocialRecipes.Services.IRepositories;
using SocialRecipes.Services.Services;
using System.Threading.Tasks;

[TestClass]
public class UserServiceTests
{
    private Mock<IUserRepository> _mockUserRepository;
    private UserService _userService;

    [TestInitialize]
    public void Setup()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _userService = new UserService(_mockUserRepository.Object);
    }

    [TestMethod]
    public async Task DeleteUserByIdAsync_WhenCalled_DeletesUser()
    {
        // Arrange
        int userId = 1;

        // Act
        await _userService.DeleteUserByIdAsync(userId);

        // Verify
        _mockUserRepository.Verify(repo => repo.DeleteUserByIdAsync(It.Is<int>(id => id == userId)), Times.Once);
        _mockUserRepository.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task Test_GetUserByIdAsync_WhenCalled_VerifiesRepositoryCall()
    {
        // Arrange
        int userId = 1;

        // Act
        await _userService.GetUserByIdAsync(userId);

        // Assert
        _mockUserRepository.Verify(repo => repo.GetUserByIdAsync(It.Is<int>(id => id == userId)), Times.Once);
        _mockUserRepository.VerifyNoOtherCalls(); 
    }

    [TestMethod]
    public async Task Test_GetTotalUsersAsync_WhenCalled_VerifiesRepositoryCall()
    {
        // Act
        await _userService.GetTotalUsersAsync();

        // Assert
        _mockUserRepository.Verify(repo => repo.GetTotalUsersAsync(), Times.Once);
        _mockUserRepository.VerifyNoOtherCalls();
    }
}

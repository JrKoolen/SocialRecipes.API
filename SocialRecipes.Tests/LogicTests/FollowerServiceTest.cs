using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;
using SocialRecipes.Services.IRepositories;
using SocialRecipes.Services.Services;
using System.Threading.Tasks;

[TestClass]
public class FollowerServiceTests
{
    private Mock<IFollowerRepository> _mockFollowerRepository;
    private FollowService _followService;

    [TestInitialize]
    public void Setup()
    {
        _mockFollowerRepository = new Mock<IFollowerRepository>();
        _followService = new FollowService(_mockFollowerRepository.Object);
    }

    [TestMethod]
    public async Task GetFollowersAsync_WhenCalled_ReturnsFollowers()
    {
        // Arrange
        int userId = 1;
        var expectedFollowers = new UserDto[]
        {
            new UserDto { Id = 2, Name = "Follower1" },
            new UserDto { Id = 3, Name = "Follower2" }
        };

        _mockFollowerRepository.Setup(repo => repo.GetFollowersAsync(userId))
                               .ReturnsAsync(expectedFollowers);

        // Act
        var result = await _followService.GetFollowersAsync(userId);

        // Assert
        Assert.IsNotNull(result); 
        Assert.AreEqual(expectedFollowers.Length, result.Length); 

        _mockFollowerRepository.Verify(repo => repo.GetFollowersAsync(It.Is<int>(id => id == userId)), Times.Once);
        _mockFollowerRepository.VerifyNoOtherCalls();
    }
}
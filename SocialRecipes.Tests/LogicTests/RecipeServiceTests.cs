using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;
using SocialRecipes.Services.IRepositories;
using SocialRecipes.Services.Services;
using System.Threading.Tasks;

[TestClass]
public class RecipeServiceTests
{
    private Mock<IRecipeRepository> _mockRecipeRepository;
    private RecipeService _recipeService;

    [TestInitialize]
    public void Setup()
    {
        _mockRecipeRepository = new Mock<IRecipeRepository>();
        _recipeService = new RecipeService(_mockRecipeRepository.Object);
    }


    [TestMethod]
    public async Task Test_GetAllRecipesAsync_WhenCalled_ReturnsRecipes()
    {
        // Arrange
        var expectedRecipes = new RecipeDto[]
        {
            new RecipeDto(),
            new RecipeDto(),
        };

        _mockRecipeRepository.Setup(repo => repo.GetAllRecipesAsync())
                             .ReturnsAsync(expectedRecipes);

        // Act
        var result = await _recipeService.GetAllRecipesAsync();

        // Assert
        Assert.IsNotNull(result); 

        Assert.AreEqual(expectedRecipes.Length, result.Length);

        CollectionAssert.AreEqual(expectedRecipes, result); 

        // Verify that GetAllRecipesAsync was called exactly once
        _mockRecipeRepository.Verify(repo => repo.GetAllRecipesAsync(), Times.Once);
        // Ensure no other methods were called on the repository
        _mockRecipeRepository.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetFeaturedRecipesAsync_WhenCalled_ReturnsTopFeaturedRecipes()
    {
        // Arrange
        string status = "Published";
        int featuredCount = 3;

        var allRecipes = new RecipeDto[]
        {
            new RecipeDto { Id = 1, Title = "Recipe1", Likes = 10 },
            new RecipeDto { Id = 2, Title = "Recipe2", Likes = 30 },
            new RecipeDto { Id = 3, Title = "Recipe3", Likes = 20 },
            new RecipeDto { Id = 4, Title = "Recipe4", Likes = 50 },
            new RecipeDto { Id = 5, Title = "Recipe5", Likes = 40 }
        };

        _mockRecipeRepository.Setup(repo => repo.GetAllRecipesFromStatusAsync(status))
                             .ReturnsAsync(allRecipes);

        // Act
        var result = await _recipeService.GetFeaturedRecipesAsync(status, featuredCount);

        // Assert
        Assert.IsNotNull(result); 
        Assert.AreEqual(featuredCount, result.Length); 

     
        Assert.IsTrue(result.SequenceEqual(new RecipeDto[]
        {
            allRecipes[3], 
            allRecipes[4], 
            allRecipes[1]  
        }));

        Assert.IsTrue(result[0].Likes >= result[1].Likes);
        Assert.IsTrue(result[0].Title == "Recipe4");

        _mockRecipeRepository.Verify(repo => repo.GetAllRecipesFromStatusAsync(It.Is<string>(s => s == status)), Times.Once);
        _mockRecipeRepository.VerifyNoOtherCalls();
    }


    [TestMethod]
    public async Task GetAllRecipesFromUserAsync_WhenNoRecipes_ReturnsEmptyArray()
    {
        // Arrange
        int userId = 1;
        _mockRecipeRepository.Setup(repo => repo.GetAllRecipesFromUserAsync(userId))
                             .ReturnsAsync((RecipeDto[])null); 

        // Act
        var result = await _recipeService.GetAllRecipesFromUserAsync(userId);

        // Assert
        Assert.IsNotNull(result); 
        Assert.AreEqual(0, result.Length); 

        // mock repository should be called once
        _mockRecipeRepository.Verify(repo => repo.GetAllRecipesFromUserAsync(It.Is<int>(id => id == userId)), Times.Once);
        _mockRecipeRepository.VerifyNoOtherCalls();
    }
}

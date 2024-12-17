using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SocialRecipes.API.Controllers;
using SocialRecipes.Domain.Dto.General;
using SocialRecipes.Domain.Dto.IN;
using SocialRecipes.Services.IRepositories;
using SocialRecipes.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialRecipes.Tests.ControllerTests
{
    [TestClass]
    public class RecipeControllerTests
    {
        private Mock<IRecipeRepository> _mockRecipeRepository;
        private Mock<ILogger<RecipeController>> _mockLogger;
        private RecipeService _recipeService;
        private RecipeController _recipeController;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRecipeRepository = new Mock<IRecipeRepository>();
            _mockLogger = new Mock<ILogger<RecipeController>>();

            _recipeService = new RecipeService(_mockRecipeRepository.Object);
            _recipeController = new RecipeController(_mockLogger.Object, _recipeService);
        }
    }
}

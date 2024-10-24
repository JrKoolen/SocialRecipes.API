using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialRecipes.DAL.Repositories;
using SocialRecipes.Domain.IRepositories;
using SocialRecipes.Domain.IServices;
using SocialRecipes.Services.Services;

namespace DependencyTests.Tests
{
    [TestClass]
    public class DependencyInjectionTests
    {
        private ServiceProvider _serviceProvider;

        [TestInitialize]
        public void Setup()
        {
            string conn = "default";
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped<IUserRepository, UserRepository>(provider =>
            {
                var connectionString = conn;
                return new UserRepository(connectionString);
            });
            serviceCollection.AddScoped<IUserService, UserService>();

            serviceCollection.AddScoped<IRecipeRepository, RecipeRepository>(provider =>
            {
                var connectionString = conn;
                return new RecipeRepository(connectionString);
            });
            serviceCollection.AddScoped<IRecipeService, RecipeService>();

            serviceCollection.AddScoped<IIngredientRepository, IngredientRepository>(provider =>
            {
                var connectionString = conn;
                return new IngredientRepository(connectionString);
            });
            serviceCollection.AddScoped<IIngredientService, IngredientService>();

            serviceCollection.AddScoped<IFollowerRepository, FollowerRepository>(provider =>
            {
                var connectionString = conn;
                return new FollowerRepository(connectionString);
            });
            serviceCollection.AddScoped<IFollowerService, FollowerService>();

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [TestMethod]
        public void UserService_Should_Be_Registered()
        {
            var userService = _serviceProvider.GetService<IUserService>();
            Assert.IsNotNull(userService);
            Assert.IsInstanceOfType(userService, typeof(UserService));
        }

        [TestMethod]
        public void RecipeService_Should_Be_Registered()
        {
            var recipeService = _serviceProvider.GetService<IRecipeService>();
            Assert.IsNotNull(recipeService);
            Assert.IsInstanceOfType(recipeService, typeof(RecipeService));
        }

        [TestMethod]
        public void IngredientService_Should_Be_Registered()
        {
            var ingredientService = _serviceProvider.GetService<IIngredientService>();
            Assert.IsNotNull(ingredientService);
            Assert.IsInstanceOfType(ingredientService, typeof(IngredientService));
        }

        [TestMethod]
        public void FollowerService_Should_Be_Registered()
        {
            var followerService = _serviceProvider.GetService<IFollowerService>();
            Assert.IsNotNull(followerService);
            Assert.IsInstanceOfType(followerService, typeof(FollowerService));
        }

        [TestMethod]
        public void UserRepository_Should_Be_Registered()
        {
            var userRepository = _serviceProvider.GetService<IUserRepository>();
            Assert.IsNotNull(userRepository);
            Assert.IsInstanceOfType(userRepository, typeof(UserRepository));
        }

        [TestMethod]
        public void RecipeRepository_Should_Be_Registered()
        {
            var recipeRepository = _serviceProvider.GetService<IRecipeRepository>();
            Assert.IsNotNull(recipeRepository);
            Assert.IsInstanceOfType(recipeRepository, typeof(RecipeRepository));
        }

        [TestMethod]
        public void IngredientRepository_Should_Be_Registered()
        {
            var ingredientRepository = _serviceProvider.GetService<IIngredientRepository>();
            Assert.IsNotNull(ingredientRepository);
            Assert.IsInstanceOfType(ingredientRepository, typeof(IngredientRepository));
        }

        [TestMethod]
        public void FollowerRepository_Should_Be_Registered()
        {
            var followerRepository = _serviceProvider.GetService<IFollowerRepository>();
            Assert.IsNotNull(followerRepository);
            Assert.IsInstanceOfType(followerRepository, typeof(FollowerRepository));
        }
    }
}

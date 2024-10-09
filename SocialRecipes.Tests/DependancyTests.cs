using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialRecipes.DAL.Repositories;
using SocialRecipes.Domain.IRepositories; // Adjust namespaces as necessary
using SocialRecipes.Domain.IServices;
using SocialRecipes.Services.Services; // Adjust namespaces as necessary

namespace DependancyTests.Tests
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

            // Register the repository
            serviceCollection.AddScoped<IUserRepository, UserRepository>(provider =>
            {
                var connectionString = conn;
                return new UserRepository(connectionString);
            });

            // Register the service
            serviceCollection.AddScoped<IUserService, UserService>();

            // Build the service provider
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [TestMethod]
        public void UserService_Should_Be_Registered()
        {
            // Act
            var userService = _serviceProvider.GetService<IUserService>();

            // Assert
            Assert.IsNotNull(userService);
            Assert.IsInstanceOfType(userService, typeof(UserService));
        }
    }
}

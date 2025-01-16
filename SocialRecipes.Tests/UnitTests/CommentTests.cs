// using System;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using Moq;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Microsoft.Extensions.Options;
// using SocialRecipes.API.Controllers;
// using SocialRecipes.Services.Services;
// using SocialRecipes.Infrastructure.Settings;
// using Microsoft.Extensions.DependencyModel;
// using SocialRecipes.DAL.Repositories;
// using SocialRecipes.Services.IRepositories;
// using SocialRecipes.Domain.Dto.IN;
// using SocialRecipes.Domain.Dto.General;
// using System.Reflection;

// namespace SocialRecipes.Tests.UnitTests
// {
//     [TestClass]
//     public class CommentTests
//     {
//         // mock the logger, service layer, jwtsettings for the controller and mock the repository interface for the comment service.
//         private Mock<ILogger<CommentController>> _logger;
//         private Mock<CommentService> _commentService;
//         private CommentController _commentController;
//         private Mock<ICommentRepository> _commentRepository;

//         [TestInitialize]
//         public void Initialize()
//         {
//             //controller moq
//             _logger = new Mock<ILogger<CommentController>>();
//             _commentService = new Mock<CommentService>(_commentRepository);
//             _commentController = new CommentController(_logger.Object, _commentService.Object);
//         }

//         [TestMethod]
//         public void Test_if_CommentController_Is_Of_The_Correct_Types()
//         {
//             Assert.IsInstanceOfType(_commentController, typeof(ControllerBase));
//             Assert.IsInstanceOfType(_commentController, typeof(CommentController));
//         }

//         [TestMethod]
//         public void Test_Addcomment_should_return_BadRequest_when_comment_is_null()
//         {
//             // Arrange
//             CommentDto comment = null;

//             // Act
//             var result = _commentController.AddComment(comment).Result as BadRequestObjectResult;

//             // Assert
//             Assert.IsNotNull(result);
//             _commentService.Verify(service => service.AddCommentAsync(It.IsAny<CommentDto>()), Times.Once);
//             Assert.AreEqual(400, result.StatusCode);
//         }

//         [TestMethod]
//         public void Test_Addcomment_should_return_Ok_when_comment_is_added_successfully()
//         {
//             // Arrange
//             CommentDto comment = new CommentDto { RecipeId = 1, UserId = 1, Content = "Test " };

//             // Act
//             var result = _commentController.AddComment(comment).Result as OkObjectResult;

//             // Assert
//             Assert.IsNotNull(result);
//             _commentService.Verify(service => service.AddCommentAsync(It.IsAny<CommentDto>()), Times.Once);
//             Assert.AreEqual(200, result.StatusCode);
//         }

//         [TestMethod]
//         public void Test_Addcomment_should_return_InternalServerError_when_exception_is_thrown()
//         {
//             // Arrange
//             CommentDto comment = new CommentDto { RecipeId = 1, UserId = 1, Content = "Test " };
//             _commentService.Setup(service => service.AddCommentAsync(It.IsAny<CommentDto>())).Throws(new Exception());

//             // Act
//             var result = _commentController.AddComment(comment).Result as ObjectResult;

//             // Assert
//             Assert.IsNotNull(result);
//             _commentService.Verify(service => service.AddCommentAsync(It.IsAny<CommentDto>()), Times.Once);
//             Assert.AreEqual(500, result.StatusCode);
//         }
//     }
// }
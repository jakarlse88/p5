using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using TheCarHub.Areas.Admin.Controllers;
using TheCarHub.Models.InputModels;
using TheCarHub.Services;
using Xunit;

namespace TheCarHub.Test
{
    public class AdminListingControllerTests
    {
        [Fact]
        public void TestCreateGet()
        {
            // Arrange
            var controller = new ListingController(null);
            
            // Act
            var result = controller.Create();

            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }

        [Fact]
        public async Task TestCreatePostInputModelNull()
        {
            // Arrange
            var controller = new ListingController(null);
            
            // Act
            var result = await controller.Create(null);

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal("Listing", redirectResult.ControllerName);
            Assert.Equal("Create", redirectResult.ActionName);
        }

        [Fact]
        public async Task TestCreatePostModelStateInvalid()
        {
            // Arrange
            var controller = new ListingController(null);
            controller.ModelState.AddModelError("test", "test");

            // Act
            var result = await controller.Create(new ListingInputModel { Description = "test description" });

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var modelResult = Assert.IsAssignableFrom<ListingInputModel>(viewResult.Model);
            Assert.Equal("test description", modelResult.Description);
        }

        [Fact]
        public async Task TestCreateInputModelValid()
        {
            // Arrange
            var mockService = new Mock<IListingService>();
            mockService
                .Setup(x => x.AddListingAsync(It.IsAny<ListingInputModel>()))
                .Verifiable();
            
            var controller = new ListingController(mockService.Object);
            
            // Act
            var result = await controller.Create(new ListingInputModel());

            // Assert
            var redirectResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal("Home", redirectResult.ControllerName);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task TestGetEditIdNull()
        {
            // Arrange
            var controller = new ListingController(null);

            // Act
            var result = await controller.Edit(null);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestGetEditIdValid()
        {
            // Arrange
            var inputModel = new ListingInputModel
            {
                Id = 666,
                Description = "test"
            };
            
            var mockService = new Mock<IListingService>();
            mockService
                .Setup(x => x.GetListingInputModelByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(inputModel)
                .Verifiable();

            var controller = new ListingController(mockService.Object);
            
            // Act
            var result = await controller.Edit(1);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var modelResult = Assert.IsAssignableFrom<ListingInputModel>(viewResult.Model);
            Assert.Equal("test", modelResult.Description);
            mockService.Verify(x => x.GetListingInputModelByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public async Task TestGetEditInputModelNull()
        {
            // Arrange
            ListingInputModel inputModel = null;
            
            var mockService = new Mock<IListingService>();
            mockService
                .Setup(x => x.GetListingInputModelByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(inputModel)
                .Verifiable();

            var controller = new ListingController(mockService.Object);
            
            // Act
            var result = await controller.Edit(1);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
            
            mockService.Verify(x => x.GetListingInputModelByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task TestPostEditInputModelNull()
        {
            // Arrange
            var controller = new ListingController(null);

            // Act
            var result = await controller.Edit(1, null);

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result);
        }

        [Fact]
        public async Task TestPostEditIdUnequalToInputModelId()
        {
            // Arrange
            var controller = new ListingController(null);

            // Act
            var result = await controller.Edit(1, new ListingInputModel { Id = 2 });

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestPostEditModelStateInvalid()
        {
            // Arrange
            var controller = new ListingController(null);
            controller.ModelState.AddModelError("test", "test");

            // Act
            var result = await controller.Edit(1, new ListingInputModel { Id = 1, Description = "test" });

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var modelResult = Assert.IsAssignableFrom<ListingInputModel>(viewResult.Model);
            Assert.Equal("test", modelResult.Description);
        }

        [Fact]
        public async Task TestPostEditEditSuccessFalse()
        {
            // Arrange
            var mockService = new Mock<IListingService>();
            mockService
                .Setup(x => x.UpdateListingAsync(It.IsAny<ListingInputModel>()))
                .ReturnsAsync(false)
                .Verifiable();
            
            var controller = new ListingController(mockService.Object);
            controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());

            // Act
            var result = await controller.Edit(1, new ListingInputModel { Id = 1, Description = "test" });

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var modelResult = Assert.IsAssignableFrom<ListingInputModel>(viewResult.Model);
            Assert.Equal("test", modelResult.Description);
            Assert.NotNull(controller.TempData["EditError"]);
            Assert.Equal("There was a problem updating the information. Please ensure the data entered is valid, and try again.", 
                controller.TempData["EditError"]);
            
            mockService.Verify(x => x.UpdateListingAsync(It.IsAny<ListingInputModel>()),
                Times.Once);
        }

        [Fact]
        public async Task TestPostEditEditSuccessTrue()
        {
            // Arrange
            var mockService = new Mock<IListingService>();
            mockService
                .Setup(x => x.UpdateListingAsync(It.IsAny<ListingInputModel>()))
                .ReturnsAsync(true)
                .Verifiable();
            
            var controller = new ListingController(mockService.Object);
            controller.TempData = 
                new TempDataDictionary(
                    new DefaultHttpContext(),
                    Mock.Of<ITempDataProvider>());

            // Act
            var result = 
                await controller.Edit(
                    1,
                    new ListingInputModel {Id = 1, Description = "test"});
            
            // Assert
            var redirectResult = 
                Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal("Home", redirectResult.ControllerName);
            Assert.Equal("Index", redirectResult.ActionName);
            
            mockService.Verify(x => x.UpdateListingAsync(It.IsAny<ListingInputModel>()),
                Times.Once);
        }
    }
}
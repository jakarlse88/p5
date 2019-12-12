using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using TheCarHub.Controllers;
using TheCarHub.Models.InputModels;
using TheCarHub.Services;
using Xunit;

namespace TheCarHub.Test
{
    public class ContactControllerTests
    {
        [Fact]
        public void TestIndex()
        {
            // Arrange
            var controller = new ContactController(null);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }

        [Fact]
        public async Task TestSendMessageInputModelNull()
        {
            // Arrange
            var controller = new ContactController(null);
            
            // Act
            var result = await controller.SendMessage(null);

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result);
        }

        [Fact]
        public async Task TestSendMessageModelStateInvalid()
        {
            // Arrange
            var controller = new ContactController(null);
            
            controller.ModelState.AddModelError("test", "test");

            var inputModel = new ContactInputModel();
            
            // Act
            var result = await controller.SendMessage(inputModel);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var modelResult = Assert.IsAssignableFrom<ContactInputModel>(viewResult.Model);
        }

        [Fact]
        public async Task TestSendMessageMessageSuccessFalse()
        {
            // Arrange
            var mockMessageService = new Mock<IMessageService>();
            mockMessageService
                .Setup(x => x.SendEmail(It.IsAny<ContactInputModel>()))
                .ReturnsAsync(false)
                .Verifiable();

            var controller = new ContactController(mockMessageService.Object);
            
            controller.TempData = new TempDataDictionary(
                new DefaultHttpContext(),
                Mock.Of<ITempDataProvider>());
            
            var inputModel = new ContactInputModel();
            
            // Act
            var result = await controller.SendMessage(inputModel);

            // Assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            var modelResult = Assert.IsAssignableFrom<ContactInputModel>(viewResult.Model);
            Assert.Equal("There was a problem sending the message. Please try again, or contact the webmaster if the problem persists",
                controller.TempData["MessageFail"]);
                

            mockMessageService
                .Verify(x => x.SendEmail(It.IsAny<ContactInputModel>()), Times.Once);
        }
    }
}
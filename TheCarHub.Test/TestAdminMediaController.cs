using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TheCarHub.Areas.Admin.Controllers;
using TheCarHub.Models.Entities;
using TheCarHub.Services;
using Xunit;

namespace TheCarHub.Test
{
    public class TestAdminMediaController
    {
        [Fact]
        public async Task TestUploadFilesNull()
        {
            // Arrange
            var controller = new MediaController(null);

            // Act
            var result = await controller.Upload(null);

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result);
        }

        [Fact]
        public async Task TestUploadFilesEmpty()
        {
            // Arrange
            var controller = new MediaController(null);

            // Act
            var result = await controller.Upload(new List<IFormFile>());

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result);
        }

        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public async Task TestUploadFileNamesNull()
        {
            // Arrange
            List<string> fileNames = null;

            var files = new List<IFormFile>
            {
                new Mock<IFormFile>().Object
            };

            var mockMediaService = new Mock<IMediaService>();
            mockMediaService
                .Setup(x => x.UploadFileAzureBlobAsync(It.IsAny<List<IFormFile>>()))
                .ReturnsAsync(fileNames)
                .Verifiable();

            var controller = new MediaController(mockMediaService.Object);

            // Act
            var result = await controller.Upload(files);

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result);
            
            mockMediaService
                .Verify(x => x.UploadFileAzureBlobAsync(It.IsAny<List<IFormFile>>()), Times.Once);
        }

        [Fact]
        public async Task TestUploadFileNamesEmpty()
        {
            // Arrange
            var fileNames = new List<string>();

            var files = new List<IFormFile>
            {
                new Mock<IFormFile>().Object
            };

            var mockMediaService = new Mock<IMediaService>();
            mockMediaService
                .Setup(x => x.UploadFileAzureBlobAsync(It.IsAny<List<IFormFile>>()))
                .ReturnsAsync(fileNames)
                .Verifiable();

            var controller = new MediaController(mockMediaService.Object);

            // Act
            var result = await controller.Upload(files);
            
            mockMediaService
                .Verify(x => x.UploadFileAzureBlobAsync(It.IsAny<List<IFormFile>>()), Times.Once);
        }

        [Fact]
        public async Task TestUploadFilesAllValid()
        {
            // Arrange
            List<string> fileNames = new List<string>
            {
                "asdadasd",
                "asdasda",
                "asdasdasd"
            };

            var files = new List<IFormFile>
            {
                new Mock<IFormFile>().Object
            };

            var mockMediaService = new Mock<IMediaService>();
            mockMediaService
                .Setup(x => x.UploadFileAzureBlobAsync(It.IsAny<List<IFormFile>>()))
                .ReturnsAsync(fileNames)
                .Verifiable();

            var controller = new MediaController(mockMediaService.Object);

            // Act
            var result = await controller.Upload(files);

            Assert.IsAssignableFrom<JsonResult>(result);
            
            mockMediaService
                .Verify(x => x.UploadFileAzureBlobAsync(It.IsAny<List<IFormFile>>()), Times.Once);
        }

        [Fact]
        public async Task TestDeleteFileNameNull()
        {
            // Arrange
            var controller = new MediaController(null);
            
            // Act
            var result = await controller.Delete(null);

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result);
        }

        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public async Task TestDeleteFileNameInvalid()
        {
            // Arrange
            var fileName = "test.jpg";

            Media media = null;
            
            var mockMediaService = new Mock<IMediaService>();
            mockMediaService
                .Setup(x => x.GetMediaByFileNameAsync(It.IsAny<string>()))
                .ReturnsAsync(media);

            var controller = new MediaController(mockMediaService.Object);
            
            // Act
            var result = await controller.Delete(fileName);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task TestDeleteRemovalSuccessFalse()
        {
            // Arrange
            var fileName = "test.jpg";

            var media = new Media();
            
            var mockMediaService = new Mock<IMediaService>();
            mockMediaService
                .Setup(x => x.RemoveMediaObject(It.IsAny<Media>()))
                .Returns(false);

            mockMediaService
                .Setup(x => x.GetMediaByFileNameAsync(It.IsAny<string>()))
                .ReturnsAsync(media);

            var controller = new MediaController(mockMediaService.Object);
            
            // Act
            var result = await controller.Delete(fileName);

            // Assert
            var statusCodeResult = Assert.IsAssignableFrom<StatusCodeResult>(result);
        }

        [Fact]
        public async Task TestDeleteRemovalSuccessTrue()
        {
            // Arrange
            var fileName = "test.jpg";
            
            var media = new Media();

            var mockMediaService = new Mock<IMediaService>();
            mockMediaService
                .Setup(x => x.RemoveAzureBlobMediaObjectAsync(It.IsAny<Media>()))
                .ReturnsAsync(true);
            
            mockMediaService
                .Setup(x => x.GetMediaByFileNameAsync(It.IsAny<string>()))
                .ReturnsAsync(media);

            var controller = new MediaController(mockMediaService.Object);
            
            // Act
            var result = await controller.Delete(fileName);

            // Assert
            Assert.IsAssignableFrom<OkResult>(result);
        }
    }
}
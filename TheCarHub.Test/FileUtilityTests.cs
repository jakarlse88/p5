using System;
using System.IO;
using System.IO.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using TheCarHub.Utilities;
using Xunit;

namespace TheCarHub.Test
{
    public class FileUtilityTests
    {
        [Fact]
        public async Task TestUploadImageToDiskAsyncFileNull()
        {
            // Arrange
            var mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem
                .Setup(x => x.Path.GetExtension(It.IsAny<string>()))
                .Returns(".pdf")
                .Verifiable();

            var fileUtility = new FileUtility(mockFileSystem.Object);

            // Act
            var result = await fileUtility.UploadImageToDiskAsync(null, null, null);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task TestUploadImageToDiskAsyncInvalidExtension()
        {
            // Arrange
            var mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem
                .Setup(x => x.Path.GetExtension(It.IsAny<string>()))
                .Returns(".pdf")
                .Verifiable();

            var fileUtility = new FileUtility(mockFileSystem.Object);

            // Act
            var result = await fileUtility.UploadImageToDiskAsync(
                null,
                null,
                Mock.Of<IFormFile>());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task TestUploadFileAsyncValid()
        {
            // Arrange
            var builder = new ConfigurationBuilder();

            IConfiguration config = builder.Build();

            var mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem
                .Setup(x => x.Path.GetExtension(It.IsAny<string>()))
                .Returns(".jpg");

            mockFileSystem
                .Setup(x => x.Path.Combine(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns("asd");

            mockFileSystem
                .Setup(x => x.Path.GetRandomFileName())
                .Returns("asd");

            mockFileSystem
                .Setup(x => x.File.Create(It.IsAny<string>()))
                .Verifiable();

            var mockFormFile = new Mock<IFormFile>();
            mockFormFile
                .Setup(x => x.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var fileUtility = new FileUtility(mockFileSystem.Object);

            // Act
            var result =
                await fileUtility.UploadImageToDiskAsync(Mock.Of<IWebHostEnvironment>(), config, mockFormFile.Object);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("asd.jpg", result);
        }

        [Fact]
        public async Task TestUploadFileAsyncCopyThrows()
        {
            // Arrange
            var builder = new ConfigurationBuilder();

            IConfiguration config = builder.Build();

            var mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem
                .Setup(x => x.Path.GetExtension(It.IsAny<string>()))
                .Returns(".jpg");

            mockFileSystem
                .Setup(x => x.Path.Combine(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns("asd");

            mockFileSystem
                .Setup(x => x.Path.GetRandomFileName())
                .Returns("asd");

            mockFileSystem
                .Setup(x => x.File.Create(It.IsAny<string>()));

            var mockFormFile = new Mock<IFormFile>();
            mockFormFile
                .Setup(x => x.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception());

            var fileUtility = new FileUtility(mockFileSystem.Object);

            // Act
            var result =
                await fileUtility.UploadImageToDiskAsync(Mock.Of<IWebHostEnvironment>(), config, mockFormFile.Object);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void TestDeleteImageFromDiskFileNameBad(string fileName)
        {
            // Arrange
            var mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem
                .Setup(x => x.Path.Combine(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            var fileUtility = new FileUtility(mockFileSystem.Object);

            // Act
            fileUtility.DeleteImageFromDisk(
                Mock.Of<IWebHostEnvironment>(),
                Mock.Of<IConfiguration>(),
                fileName);

            // Assert
            mockFileSystem
                .Verify(x => x.Path.Combine(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void TestDeleteImageFromDiskWebHostEnvNull()
        {
            // Arrange
            var mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem
                .Setup(x => x.Path.Combine(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            var fileUtility = new FileUtility(mockFileSystem.Object);

            // Act
            fileUtility.DeleteImageFromDisk(
                null,
                Mock.Of<IConfiguration>(),
                "asd");

            // Assert
            mockFileSystem
                .Verify(x => x.Path.Combine(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void TestDeleteImageFromDiskConfigNull()
        {
            // Arrange
            var mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem
                .Setup(x => x.Path.Combine(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            var fileUtility = new FileUtility(mockFileSystem.Object);

            // Act
            fileUtility.DeleteImageFromDisk(
                Mock.Of<IWebHostEnvironment>(),
                null,
                "asd");

            // Assert
            mockFileSystem
                .Verify(x => x.Path.Combine(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void TestDeleteImageFromDiskAsyncFileDoesNotExist()
        {
            // Arrange
            var mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem
                .Setup(x => x.Path.Combine(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            mockFileSystem
                .Setup(x => x.File.Exists(It.IsAny<string>()))
                .Returns(false);

            mockFileSystem
                .Setup(x => x.File.Delete(It.IsAny<string>()))
                .Verifiable();

            var fileUtility = new FileUtility(mockFileSystem.Object);

            // Act
            fileUtility.DeleteImageFromDisk(
                Mock.Of<IWebHostEnvironment>(),
                Mock.Of<IConfiguration>(),
                "test.jpg");

            // Assert
            mockFileSystem
                .Verify(x => x.File.Delete(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public void TesteDeleteImageFromDiskAsyncFileExists()
        {
            // Arrange
            var mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem
                .Setup(x => x.Path.Combine(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            mockFileSystem
                .Setup(x => x.File.Exists(It.IsAny<string>()))
                .Returns(true);

            mockFileSystem
                .Setup(x => x.File.Delete(It.IsAny<string>()))
                .Verifiable();

            var fileUtility = new FileUtility(mockFileSystem.Object);

            // Act
            fileUtility.DeleteImageFromDisk(
                Mock.Of<IWebHostEnvironment>(),
                Mock.Of<IConfiguration>(),
                "test.jpg");

            // Assert
            mockFileSystem
                .Verify(x => x.File.Delete(It.IsAny<string>()), Times.Once());
        }

    }
}
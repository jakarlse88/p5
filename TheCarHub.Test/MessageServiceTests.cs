using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Moq;
using TheCarHub.Models.InputModels;
using TheCarHub.Services;
using Xunit;

namespace TheCarHub.Test
{
    public class MessageServiceTests
    {
        private readonly IConfiguration _configuration;
        private readonly Mock<IWebHostEnvironment> _mockWebHostEnv;
        
        public MessageServiceTests()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<Startup>();

            _configuration = builder.Build();

            _mockWebHostEnv = new Mock<IWebHostEnvironment>();
            _mockWebHostEnv
                .Setup(x => x.WebRootPath)
                .Returns(Directory.GetCurrentDirectory() + 
                         Path.DirectorySeparatorChar +
                        "testroot");
        }

        [Fact]
        public async Task TestSendEmailInputModelNull()
        {
            // Arrange
            var service = new MessageService(_configuration, _mockWebHostEnv.Object);

            // Act
            var result = await service.SendEmail(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task TestSendEmailInputModelNotPopulated()
        {
            // Arrange
            var service = new MessageService(_configuration, _mockWebHostEnv.Object);

            var inputModel = new ContactInputModel();

            // Act
            var result = await service.SendEmail(inputModel);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("", "test@test.com", "test subject", "test message")]
        [InlineData("test name", "", "test subject", "test message")]
        [InlineData("test name", "test@test.com", "", "test message")]
        [InlineData("test name", "test@test.com", "test subject", "")]
        public async Task TestSendEmailInputModelOneFieldNotPopulated(
            string name,
            string email,
            string subject,
            string message)
        {
            // Arrange
            var service = new MessageService(_configuration, _mockWebHostEnv.Object);

            var inputModel = new ContactInputModel
            {
                SenderName = name,
                SenderEmail = email,
                Subject = subject,
                Message = message
            };

            // Act
            var result = await service.SendEmail(inputModel);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task TestSendMailPopulatedProduct()
        {
            // Arrange
            var service = new MessageService(_configuration, _mockWebHostEnv.Object);

            var inputModel = new ContactInputModel
            {
                SenderName = "test name",
                SenderEmail = "test email",
                SenderPhoneNumber = "(111) 222-3333",
                Subject = "test subject",
                Message = "test message"
            };

            // Act
            var result = await service.SendEmail(inputModel);

            // Assert
            Assert.True(result);
        }

    }
}
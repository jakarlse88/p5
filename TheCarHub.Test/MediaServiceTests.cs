using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using TheCarHub.Data;
using TheCarHub.Models.Entities;
using TheCarHub.Repositories;
using TheCarHub.Services;
using TheCarHub.Utilities;
using Xunit;


namespace TheCarHub.Test
{
    public class MediaServiceTests
    {
        private readonly IConfiguration _configuration;
        private readonly Mock<IWebHostEnvironment> _mockWebHostEnv;

        public MediaServiceTests()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<Startup>();

            _configuration = builder.Build();

            _mockWebHostEnv = new Mock<IWebHostEnvironment>();
            _mockWebHostEnv
                .Setup(x => x.WebRootPath)
                .Returns("/Users/jkarlsen/code/p5/TheCarHub.Test/testroot/");
        }

        private DbContextOptions<ApplicationDbContext> BuildDbContextOptions()
        {
            var options =
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
            return options;
        }

        [Fact]
        public async Task TestGetMediaByFileNameAsyncValidFileName()
        {
            // Arrange
            var options = BuildDbContextOptions();

            Media result;

            // Act
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new MediaRepository(context);

                var service = new MediaService(repository, null, null, null);

                result = await service.GetMediaByFileNameAsync("jeep_liberty_2.jpg");
            }

            // Assert
            await using (var context = new ApplicationDbContext(options))
            {
                Assert.NotNull(result);
                Assert.Equal("jeep_liberty_2.jpg", result.FileName);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestGetMediaByFileNameAsyncInvalidFileName()
        {
            // Arrange
            var options = BuildDbContextOptions();

            Media result;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new MediaRepository(context);

                var service = new MediaService(repository, null, null, null);

                result = await service.GetMediaByFileNameAsync("file nope");
            }

            // Assert
            await using (var context = new ApplicationDbContext(options))
            {
                Assert.Null(result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestUpdateMediaCollectionFileNamesNull()
        {
            // Arrange
            var service = new MediaService(
                null,
                null,
                null, null);

            var entity = new Listing();

            // Act
            service.UpdateMediaCollection(null, entity);

            // Assert
            Assert.Empty(entity.Media);
        }

        [Fact]
        [SuppressMessage("ReSharper", "CollectionNeverUpdated.Local")]
        public void TestUpdateMediaCollectionFileNamesEmpty()
        {
            // Arrange
            var service = new MediaService(
                null,
                null,
                null,
                null);

            var entity = new Listing();

            var fileNames = new List<string>();

            // Act
            service.UpdateMediaCollection(fileNames, entity);

            // Assert
            Assert.Empty(entity.Media);
        }

        [Fact]
        public void TestUpdateMediaCollectionValidArguments()
        {
            // Arrange
            var service = new MediaService(
                null,
                null,
                null,
                null);

            var entity = new Listing();

            var fileNames = new List<string>
            {
                "asdaksdlasd",
                "asdlasdasd",
                "asdlasdasdlas"
            };

            // Act
            service.UpdateMediaCollection(fileNames, entity);

            // Assert
            Assert.NotEmpty(entity.Media);
            Assert.IsAssignableFrom<ICollection<Media>>(entity.Media);
            Assert.Equal(3, entity.Media.Count);
        }

        [Fact]
        public void TestRemoveMediaObjectMediaNull()
        {
            // Arrange
            var service = new MediaService(
                null,
                null,
                null,
                null);

            // Act
            var result = service.RemoveMediaObject(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void TestRemoveMediaObjectMediaValidFileExists()
        {
            // Arrange
            var mockFileUtility = new Mock<IFileUtility>();
            mockFileUtility
                .Setup(x => x.DeleteImageFromDisk(
                    It.IsAny<IWebHostEnvironment>(),
                    It.IsAny<IConfiguration>(),
                    It.IsAny<string>()))
                .Verifiable();

            var mockRepository = new Mock<IMediaRepository>();
            mockRepository
                .Setup(x => x.DeleteMedia(It.IsAny<Media>()))
                .Verifiable();

            var media = new Media
            {
                Listing = new Listing()
            };

            var service = new MediaService(
                mockRepository.Object,
                _mockWebHostEnv.Object,
                _configuration,
                mockFileUtility.Object);

            // Act
            var result = service.RemoveMediaObject(media);

            // Assert
            Assert.True(result);
        }
    }
}


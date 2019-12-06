using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using TheCarHub.Data;
using TheCarHub.Models.Entities;
using TheCarHub.Repositories;
using TheCarHub.Services;
using Xunit;

namespace TheCarHub.Test
{
    public class MediaServiceTests
    {
        private DbContextOptions<ApplicationDbContext> BuildDbContextOptions()
        {
            var options =
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
            return options;
        }

        [Fact]
        public async void TestGetAllMedia()
        {
            // Arrange
            var options = BuildDbContextOptions();
            IEnumerable<Media> results;

            // Act
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new MediaRepository(context);
                
                var service = new MediaService(repository);

                results = await service.GetAllMedia();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Equal(6, results.Count());
        }

        [Theory]
        [InlineData(1, "file one")]
        [InlineData(2, "file two")]
        [InlineData(3, "file three")]
        public async void TestGetMediaByIdValidId(int testId, string expectedFileName)
        {
            // Arrange
            var options = BuildDbContextOptions();
            Media result;

            // Act
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new MediaRepository(context);
                
                var service = new MediaService(repository);

                result = await service.GetMediaByIdAsync(testId);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Equal(expectedFileName, result.FileName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(99)]
        public async void TestGetMediaByIdInvalidId(int testId)
        {
            // Arrange
            var options = BuildDbContextOptions();
            Media result;

            // Act
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new MediaRepository(context);
                
                var service = new MediaService(repository);

                result = await service.GetMediaByIdAsync(testId);
            }

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestAddCarNonNullObject()
        {
            // Arrange
            var options = BuildDbContextOptions();
            var testEntity = new Media
            {
                FileName = "test file"
            };

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new MediaRepository(context);
                
                var service = new MediaService(repository);

                service.AddMedia(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Media.ToList();

                Assert.Equal(7, result.Count);
                Assert.Contains(result, m => m.FileName == "test file");

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void TestAddMediaNullObject()
        {
            // Arrange
            var options = BuildDbContextOptions();
            Media testObject = null;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new MediaRepository(context);
                
                var service = new MediaService(repository);

                service.AddMedia(testObject);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Media.ToList();

                Assert.Equal(6, result.Count);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void TestEditMediaNonNullObject()
        {
            // Arrange
            var options = BuildDbContextOptions();

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new MediaRepository(context);
                
                var service = new MediaService(repository);

                var media = context.Media.ToList().FirstOrDefault(i => i.Id == 1);

                media.FileName = "test";

                service.EditMedia(media);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Media.ToList().FirstOrDefault(i => i.Id == 1);

                Assert.Equal("test", result.FileName);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestEditMediaNullObject()
        {
            // Arrange
            var mockRepository = new Mock<IMediaRepository>();
            mockRepository
                .Setup(x => x.EditMedia(It.IsAny<Media>()))
                .Verifiable();

            var service = new MediaService(mockRepository.Object);

//            Media testObject = null;
            
            // Act
            service.DeleteMedia(null);

            // Assert
            mockRepository
                .Verify(x => x.EditMedia(It.IsAny<Media>()), Times.Never);
        }
        
        [Fact]
        public void TestDeleteMediaValidEntity()
        {
            // Arrange
            var options = BuildDbContextOptions();

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new MediaRepository(context);
                
                var service = new MediaService(repository);

                var testObject = context.Media.Include(m => m.Listing).FirstOrDefault(m => m.Id == 1);

                service.DeleteMedia(testObject);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Media.ToList();

                Assert.DoesNotContain(result, l => l.Id == 1);
                Assert.Equal(5, result.Count);
            }
        }

        [Fact]
        public void TestDeleteMediaNull()
        {
            // Arrange
            var options = BuildDbContextOptions();

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new MediaRepository(context);
                
                var service = new MediaService(repository);

                service.DeleteMedia(null);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Media.ToList();

                Assert.Equal(6, result.Count);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestGetMediaByFileNameAsyncValidFileName()
        {
            // Arrange
            var options = BuildDbContextOptions();
            
            Media result;
            
            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new MediaRepository(context);
                
                var service = new MediaService(repository);

                result = await service.GetMediaByFileNameAsync("file one");
            }

            // Assert
            await using (var context = new ApplicationDbContext(options))
            {
                Assert.NotNull(result);
                Assert.Equal("file one", result.FileName);

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
                
                var service = new MediaService(repository);

                result = await service.GetMediaByFileNameAsync("file nope");
            }

            // Assert
            await using (var context = new ApplicationDbContext(options))
            {
                Assert.Null(result);

                context.Database.EnsureDeleted();
            }
        }


    }
}
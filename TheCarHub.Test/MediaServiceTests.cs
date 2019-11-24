using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using TheCarHub.Data;
using TheCarHub.Models.Entities;
using TheCarHub.Repositories;
using TheCarHub.Services;
using Xunit;

namespace TheCarHub.Test
{
    [Collection("DB")]
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

        private void PrepareTestDb(DbContextOptions<ApplicationDbContext> contextOptions)
        {
            var testEntities = new[]
            {
                new Media
                {
                    Id = 1,
                    FileName = "file one"
                },
                new Media
                {
                    Id = 2,
                    FileName = "file two"
                },
                new Media
                {
                    Id = 3,
                    FileName = "file three"
                }
            };

            using (var context = new ApplicationDbContext(contextOptions))
            {
                foreach (var item in testEntities)
                {
                    context.Media.Add(item);
                }

                context.SaveChanges();
            }
        }

        [Fact]
        public async void TestGetAllMedia()
        {
            // Arrange
            var options = BuildDbContextOptions();
            PrepareTestDb(options);
            IEnumerable<Media> results;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new MediaRepository(context);
                var service = new MediaService(repository);

                results = await service.GetAllMedia();
            }

            // Assert
            Assert.Equal(3, results.Count());
        }

        [Theory]
        [InlineData(1, "file one")]
        [InlineData(2, "file two")]
        [InlineData(3, "file three")]
        public async void TestGetMediaByIdValidId(int testId, string expectedFileName)
        {
            // Arrange
            var options = BuildDbContextOptions();
            PrepareTestDb(options);
            Media result;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new MediaRepository(context);
                var service = new MediaService(repository);

                result = await service.GetMediaById(testId);
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
            PrepareTestDb(options);
            Media result;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new MediaRepository(context);
                var service = new MediaService(repository);

                result = await service.GetMediaById(testId);
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
                Id = 1,
                FileName = "Test"
            };

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new MediaRepository(context);
                var service = new MediaService(repository);

                service.AddMedia(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Media.ToList();

                Assert.Single(result);
                Assert.Equal(1, result.First().Id);
            }
        }

        [Fact]
        public void TestAddMediaNullObject()
        {
            // Arrange
            var options = BuildDbContextOptions();
            Media testObject = null;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new MediaRepository(context);
                var service = new MediaService(repository);

                service.AddMedia(testObject);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Media.ToList();

                Assert.Empty(result);
            }
        }

        [Fact]
        public void TestEditMediaNonNullObject()
        {
            // Arrange
            var options = BuildDbContextOptions();
            PrepareTestDb(options);

            // Act
            using (var context = new ApplicationDbContext(options))
            {
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
            }
        }

        [Fact]
        public void TestEditMediaNullObject()
        {
            // Arrange
            var options = BuildDbContextOptions();
            PrepareTestDb(options);

            // Act
            var mockRepository = new Mock<IMediaRepository>();
            mockRepository
                .Setup(x => x.DeleteMedia(It.IsAny<int>()))
                .Verifiable();

            var service = new MediaService(mockRepository.Object);

            service.DeleteMedia(10);

            // Assert
            mockRepository
                .Verify(x => x.DeleteMedia(It.IsAny<int>()), Times.Once);
        }
        
        [Fact]
        public void TestDeleteMediaValidId()
        {
            // Arrange
            var options = BuildDbContextOptions();
            PrepareTestDb(options);

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new MediaRepository(context);
                var service = new MediaService(repository);

                service.DeleteMedia(1);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Media.ToList();

                Assert.DoesNotContain(result, l => l.Id == 1);
            }
        }

        [Fact]
        public void TestDeleteMediaInvalidId()
        {
            // Arrange
            var options = BuildDbContextOptions();
            PrepareTestDb(options);

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new MediaRepository(context);
                var service = new MediaService(repository);

                service.DeleteMedia(4);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Media.ToList();

                Assert.Equal(3, result.Count);
            }
        }
    }
}
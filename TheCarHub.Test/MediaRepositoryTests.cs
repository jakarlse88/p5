using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using TheCarHub.Data;
using TheCarHub.Models.Entities;
using TheCarHub.Repositories;
using Xunit;

namespace TheCarHub.Test
{
//    [Collection("DB")]
    public class MediaRepositoryTests
    {
        private DbContextOptions<ApplicationDbContext> BuildDbContextOptions()
        {
            var options =
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlServer("Server=localhost,1433; Database=MediaRepositoryTestDB; User=sa; Password=reallyStrongPwd123;")
                    .Options;

            return options;
        }

        [Fact]
        public async void TestGetAllMedia()
        {
            // Arrange
            var options = BuildDbContextOptions();
            IEnumerable<Media> result;

            // Act
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new MediaRepository(context);
                
                result = await repository.GetAllMedia();
            }

            await using (var context = new ApplicationDbContext(options))
            {
                // Assert
                var expected = context.Media.ToList();
                Assert.Equal(expected.Count, result.ToList().Count);

                context.Database.EnsureDeleted();
            }
        }

        [Theory]
        [InlineData(1, "file one")]
        [InlineData(2, "file two")]
        [InlineData(3, "file three")]
        public async void TestGetMediaByIdValidId(
            int testId, string expectedFileName)
        {
            // Arrange
            var options = BuildDbContextOptions();
            Media result;

            // Act
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new MediaRepository(context);
                
                result = await repository.GetMediaById(testId);
            }

            await using (var context = new ApplicationDbContext(options))
            {
                // Assert
                Assert.Equal(expectedFileName, result.FileName);

                context.Database.EnsureDeleted();
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-7)]
        [InlineData(7)]
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
                
                result = await repository.GetMediaById(testId);

                // Assert
                Assert.Null(result);
                
                context.Database.EnsureDeleted();
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void TestDeleteMediaValidId(int testId)
        {
            // Arrange
            var options = BuildDbContextOptions();

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new MediaRepository(context);

                repository.DeleteMedia(testId);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var results = context.Listing.ToList();

                Assert.DoesNotContain(results, l => l.Id == testId);
                Assert.Equal(5, results.Count);

                context.Database.EnsureDeleted();
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(7)]
        [InlineData(-1)]
        public void TestDeleteMediaInvalidId(int testId)
        {
            // Arrange
            var options = BuildDbContextOptions();
            int expected;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new MediaRepository(context);

                expected = context.Media.ToList().Count;

                repository.DeleteMedia(testId);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var actual = context.Media.ToList().Count;

                Assert.Equal(expected, actual);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestSaveMediaValidEntity()
        {
            // Arrange
            var options = BuildDbContextOptions();
            var testEntity = new Media
            {
                FileName = "file seven"
            };

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new MediaRepository(context);

                repository.AddMedia(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Media.ToList();

                Assert.Equal("file seven", result.Last().FileName);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void TestSaveMediaNullEntity()
        {
            // Arrange
            var options = BuildDbContextOptions();
            Media testEntity = null;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new MediaRepository(context);

                repository.AddMedia(testEntity);
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
        public void TestUpdateMediaValidEntity()
        {
            // Arrange
            var options = BuildDbContextOptions();

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new MediaRepository(context);

                var testEntity =
                    context
                        .Media
                        .FirstOrDefault(l => l.Id == 1);

                testEntity.FileName = "test file";

                repository.EditMedia(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result =
                    context
                        .Media
                        .FirstOrDefault(l => l.Id == 1);

                Assert.Equal("test file", result.FileName);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void TestUpdateMediaNullEntity()
        {
            // Arrange
            var options = BuildDbContextOptions();
            var contextMock = new Mock<ApplicationDbContext>();
            
            contextMock
                .Setup(x => 
                    x.Update(It.IsAny<Media>()))
                .Verifiable();

            // Act
                var repository = new MediaRepository(contextMock.Object);

                Media testEntity = null;

                repository.EditMedia(testEntity);

            // Assert
            contextMock
                .Verify(x => x.Update(It.IsAny<Media>()), Times.Never);
        }
    }
}
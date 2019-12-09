using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using TheCarHub.Data;
using TheCarHub.Models.Entities;
using TheCarHub.Repositories;
using Xunit;

namespace TheCarHub.Test
{
    public class MediaRepositoryTests
    {
        private DbContextOptions<ApplicationDbContext> BuildTestDbOptions() 
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async void TestGetAllMedia()
        {
            // Arrange
            var options = BuildTestDbOptions();
            IEnumerable<Media> result;

            // Act
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new MediaRepository(context);
                
                result = await repository.GetAllMedia();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Equal(6, result.ToList().Count);
        }

        [Theory]
        [InlineData(1, "file one")]
        [InlineData(2, "file two")]
        [InlineData(3, "file three")]
        public async void TestGetMediaByIdValidId(
            int testId, string expectedFileName)
        {
            // Arrange
            var options = BuildTestDbOptions();
            Media result;

            // Act
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new MediaRepository(context);
                
                result = await repository.GetMediaById(testId);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Equal(expectedFileName, result.FileName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-7)]
        [InlineData(7)]
        public async void TestGetMediaByIdInvalidId(int testId)
        {
            // Arrange
            var options = BuildTestDbOptions();
            Media result;

            // Act
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new MediaRepository(context);
                
                result = await repository.GetMediaById(testId);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void TestDeleteMediaNonNullObject(int testId)
        {
            // Arrange
            var options = BuildTestDbOptions();

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new MediaRepository(context);

                var testObject = context.Media.FirstOrDefault(m => m.Id == testId);
                
                repository.DeleteMedia(testObject);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var results = context.Media.ToList();
                
                Assert.DoesNotContain(results, m => m.Id == testId);

                context.Database.EnsureDeleted();
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(7)]
        [InlineData(-1)]
        public void TestDeleteMediaNull(int testId)
        {
             // Arrange
            var options = BuildTestDbOptions();
            int expected;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new MediaRepository(context);

                expected = context.Media.ToList().Count;
                
                var testObject = context.Media.FirstOrDefault(m => m.Id == testId);

                repository.DeleteMedia(testObject);
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
            var options = BuildTestDbOptions();
            var testEntity = new Media {FileName = "test file"};

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

                Assert.Contains(result, m => m.FileName == "test file");
                Assert.Equal(7, result.Count);
                
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void TestSaveMediaNullEntity()
        {
            // Arrange
            var options = BuildTestDbOptions();
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
            var options = BuildTestDbOptions();

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

                repository.UpdateMedia(testEntity);
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
        public void TestUpdateMediaNullEntity()
        {
            // Arrange
            var mockContext = new Mock<ApplicationDbContext>();
            
            mockContext
                .Setup(x => x.Update(It.IsAny<Media>()))
                .Verifiable();
            
            var repository = new ListingRepository(mockContext.Object);
            
            // Act
            repository.UpdateListing(null);
            
            // Assert
            mockContext.Verify(x => x.Update(It.IsAny<Media>()), Times.Never);
        }
        
        [Fact]
        public void TestGetMediaEntityEntryValidEntity()
        {
            // Arrange
            var options = BuildTestDbOptions();

            EntityEntry<Media> result;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new MediaRepository(context);

                var testEntity = context.Media.FirstOrDefault();

                // Act
                result = repository.GetMediaEntityEntry(testEntity);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotNull(result);
            Assert.IsType<EntityEntry<Media>>(result);
        }

        [Fact]
        public void TestGetMediaEntityEntryNull()
        {
            // Arrange
            var options = BuildTestDbOptions();

            EntityEntry<Media> result;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new MediaRepository(context);

                // Act
                result = repository.GetMediaEntityEntry(null);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Null(result);
        }
    }
}
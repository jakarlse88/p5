using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Data;
using TheCarHub.Models.Entities;
using TheCarHub.Repositories;
using Xunit;

namespace TheCarHub.Test
{
    [Collection("DB")]
    public class MediaRepositoryTests
    {
        private readonly Media[] _testEntities = new Media[]
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

        private DbContextOptions<ApplicationDbContext> BuildTestDbOptions() 
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        private void PrepareTestDb(DbContextOptions<ApplicationDbContext> contextOptions)
        {
            using (var context = new ApplicationDbContext(contextOptions))
            {
                foreach (var item in _testEntities)
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
            var options = BuildTestDbOptions();
            PrepareTestDb(options);
            IEnumerable<Media> result;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new MediaRepository(context);
                result = await repository.GetAllMedia();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Equal(3, result.ToList().Count);
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
            PrepareTestDb(options);
            Media result;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
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
            PrepareTestDb(options);
            Media result;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
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
        public void TestDeleteMediaValidId(int testId)
        {
            // Arrange
            var options = BuildTestDbOptions();
            PrepareTestDb(options);

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new MediaRepository(context);

                repository.DeleteMedia(testId);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var results = context.Listing.ToList();
                
                Assert.DoesNotContain(results, l => l.Id == testId);

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
            var options = BuildTestDbOptions();
            PrepareTestDb(options);
            int expected;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
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
            var options = BuildTestDbOptions();
            var testEntity = _testEntities[0];

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new MediaRepository(context);
                
                repository.AddMedia(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Media.ToList();

                Assert.Equal(1, result.First().Id);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestSaveMediaNullEntity()
        {
            // Arrange
            var options = BuildTestDbOptions();
            Media testEntity = null;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new MediaRepository(context);
                
                repository.AddMedia(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Media.ToList();

                Assert.Empty(result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestUpdateMediaValidEntity()
        {
            // Arrange
            var options = BuildTestDbOptions();
            PrepareTestDb(options);

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new MediaRepository(context);
                
                var testEntity = 
                    context
                    .Media
                    .FirstOrDefault(l => l.Id == 1);

                testEntity.FileName = "Test";

                repository.EditMedia(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = 
                    context
                    .Media
                    .FirstOrDefault(l => l.Id == 1);

                Assert.Equal("Test", result.FileName);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestUpdateMediaNullEntity()
        {
            // Arrange
            var options = BuildTestDbOptions();
            PrepareTestDb(options);

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new MediaRepository(context);
                
                var testEntity = 
                    context
                    .Media
                    .FirstOrDefault(l => l.Id == 10);

                if (testEntity != null)
                {
                    testEntity.FileName = "test";
                }

                repository.EditMedia(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = 
                    context
                    .Media
                    .ToList();

                Assert.DoesNotContain(result, i => i.FileName == "test");

                context.Database.EnsureDeleted();
            }
        }
    }
}
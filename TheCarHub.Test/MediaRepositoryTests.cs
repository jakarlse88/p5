using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Data;
using TheCarHub.Models.Entities;
using TheCarHub.Repositories;
using Xunit;
using Xunit.Sdk;

namespace TheCarHub.Test
{
    public class MediaRepositoryTests
    {
        private readonly Media[] _testEntities = new Media[]
        {
            new Media
            {
                Id = 1,
                FileName = "RandomFileName1",
                Caption = "Caption1",
            },
            new Media
            {
                Id = 2,
                FileName = "RandomFileName2",
                Caption = "Caption2",
            },
            new Media
            {
                Id = 3,
                FileName = "RandomFileName3",
                Caption = "Caption3",
            },
        };

        private DbContextOptions<ApplicationDbContext> BuildTestDbOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public void TestAddNonNullObject()
        {
            // Arrange
            var options = BuildTestDbOptions();
            var testEntity = _testEntities[0];

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new MediaRepository(context);

                repository.Add(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var media = context.Media.ToList();

                Assert.Single(media);
                
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestAddNull()
        {
            // Arrange
            var options = BuildTestDbOptions();
            Media testEntity = null;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new MediaRepository(context);

                repository.Add(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var media = context.Media.ToList();

                Assert.Empty(media);

                context.Database.EnsureDeleted();
            }
        }
    }
}
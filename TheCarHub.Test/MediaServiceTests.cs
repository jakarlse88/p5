using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

                var service = new MediaService(repository, null, null);

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

                var service = new MediaService(repository, null, null);

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
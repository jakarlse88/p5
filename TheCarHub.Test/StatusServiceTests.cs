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
    [Collection("DB")]
    public class StatusServiceTests
    {
        private DbContextOptions<ApplicationDbContext> BuildDbContextOptions()
        {
            var options =
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            return options;
        }

        [Theory]
        [InlineData(1, "Available")]
        [InlineData(2, "Sold")]
        public async Task TestGetStatusByIdAsyncValidArg(int statusId, string expected)
        {
            // Arrange
            var options = BuildDbContextOptions();
            Status result;

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new StatusRepository(context);

                var service = new StatusService(repository);

                // Act
                result = await service.GetStatusByIdAsync(statusId);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Equal(expected, result.Name);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        [InlineData(-1)]
        public async Task TestGetStatusByIdAsyncInvalidArg(int statusId)
        {
            // Arrange
            var options = BuildDbContextOptions();
            Status result;

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new StatusRepository(context);

                var service = new StatusService(repository);

                // Act
                result = await service.GetStatusByIdAsync(statusId);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Null(result);
        }
    }
}
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TheCarHub.Data;
using TheCarHub.Models.Entities;
using TheCarHub.Repositories;
using Xunit;

namespace TheCarHub.Test
{
    [Collection("DB")]
    public class CarRepositoryTests
    {
        private DbContextOptions<ApplicationDbContext> BuildTestDbOptions() 
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }
        
        [Fact]
        public void TestGetCarEntityEntryValidEntity()
        {
            // Arrange
            var options = BuildTestDbOptions();

            EntityEntry<Car> result;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new CarRepository(context);

                var testEntity = context.Car.FirstOrDefault();

                // Act
                result = repository.GetCarEntityEntry(testEntity);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotNull(result);
            Assert.IsType<EntityEntry<Car>>(result);
        }

        [Fact]
        public void TestGetListingEntityEntryNull()
        {
            // Arrange
            var options = BuildTestDbOptions();

            EntityEntry<Car> result;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new CarRepository(context);

                // Act
                result = repository.GetCarEntityEntry(null);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Null(result);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
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
    public class CarServiceTests
    {
        private DbContextOptions<ApplicationDbContext> BuildDbContextOptions(SqliteConnection connection)
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;
        }

        [Fact]
        public async void TestGetAllCars()
        {
            // Arrange
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = BuildDbContextOptions(connection);
                IEnumerable<Car> results;

                // Act
                await using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();

                    var repository = new CarRepository(context);
                    var service = new CarService(repository);

                    results = await service.GetAllCars();
                }

                await using (var context = new ApplicationDbContext(options))
                {
                    // Assert
                    Assert.Equal(6, results.Count());

                    context.Database.EnsureDeleted();
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Theory]
        [InlineData(1, "Mazda")]
        [InlineData(2, "Jeep")]
        [InlineData(3, "Ford")]
        public async void TestGetCarByIdValidId(int testId, string expectedModel)
        {
            // Arrange
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = BuildDbContextOptions(connection);
                Car result;

                // Act
                await using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();

                    var repository = new CarRepository(context);
                    var service = new CarService(repository);

                    result = await service.GetCarById(testId);
                }

                await using (var context = new ApplicationDbContext(options))
                {
                    // Assert
                    Assert.Equal(expectedModel, result.Make);

                    context.Database.EnsureDeleted();
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(99)]
        public async void TestGetCarByIdInvalidId(int testId)
        {
            // Arrange
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = BuildDbContextOptions(connection);
                Car result;

                // Act
                await using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();

                    var repository = new CarRepository(context);
                    var service = new CarService(repository);

                    result = await service.GetCarById(testId);
                }

                await using (var context = new ApplicationDbContext(options))
                {
                    // Assert
                    Assert.Null(result);

                    context.Database.EnsureDeleted();
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void TestAddCarNonNullObject()
        {
            // Arrange
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = BuildDbContextOptions(connection);

                var testEntity = new Car
                {
                    Make = "Test"
                };

                // Act
                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();

                    var repository = new CarRepository(context);
                    var service = new CarService(repository);

                    service.AddCar(testEntity);
                }

                // Assert
                using (var context = new ApplicationDbContext(options))
                {
                    var result = context.Car.ToList();

                    Assert.Equal(7, result.Count);
                    Assert.Contains(result, c => c.Make == "Test");

                    context.Database.EnsureDeleted();
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void TestAddNullObject()
        {
            // Arrange
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = BuildDbContextOptions(connection);
                Car testObject = null;

                // Act
                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();

                    var repository = new CarRepository(context);
                    var service = new CarService(repository);

                    service.AddCar(testObject);
                }

                // Assert
                using (var context = new ApplicationDbContext(options))
                {
                    var result = context.Car.ToList();

                    Assert.Equal(6, result.Count);

                    context.Database.EnsureDeleted();
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void TestEditNonNullObject()
        {
            // Arrange
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = BuildDbContextOptions(connection);

                // Act
                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();

                    var repository = new CarRepository(context);
                    var service = new CarService(repository);

                    var car = context.Car.ToList().FirstOrDefault(i => i.Id == 1);

                    car.Model = "VW";

                    service.EditCar(car);
                }

                // Assert
                using (var context = new ApplicationDbContext(options))
                {
                    var result = context.Car.ToList().FirstOrDefault(i => i.Id == 1);

                    Assert.Equal("VW", result.Model);

                    context.Database.EnsureDeleted();
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task TestEditNullObject()
        {
            // Arrange
            var mockRepository = new Mock<ICarRepository>();
            mockRepository
                .Setup(x => x.EditCar(It.IsAny<Car>()))
                .Verifiable();

            var service = new CarService(mockRepository.Object);

            // Act
            var testEntity = await service.GetCarById(10);

            service.EditCar(testEntity);

            // Assert
            mockRepository
                .Verify(x => x.EditCar(It.IsAny<Car>()), Times.Never);
        }

        [Fact]
        public void TestDeleteCarValidId()
        {
            // Arrange
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = BuildDbContextOptions(connection);

                // Act
                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();
                    
                    var repository = new CarRepository(context);
                    var service = new CarService(repository);

                    service.DeleteCar(1);
                }

                // Assert
                using (var context = new ApplicationDbContext(options))
                {
                    var result = context.Car.ToList();

                    Assert.DoesNotContain(result, l => l.Id == 1);
                    Assert.Equal(5, result.Count);

                    context.Database.EnsureDeleted();
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void TestDeleteCarInvalidId()
        {
            // Arrange
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = BuildDbContextOptions(connection);

                // Act
                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();
                    
                    var repository = new CarRepository(context);
                    var service = new CarService(repository);

                    service.DeleteCar(666);
                }

                // Assert
                using (var context = new ApplicationDbContext(options))
                {
                    var result = context.Car.ToList();

                    Assert.Equal(6, result.Count);

                    context.Database.EnsureDeleted();
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
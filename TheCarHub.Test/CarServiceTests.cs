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
    public class CarServiceTests
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
        public async void TestGetAllCars()
        {
            // Arrange
            var options = BuildDbContextOptions();
            IEnumerable<Car> results;

            // Act
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new CarRepository(context);
                var service = new CarService(repository);

                results = await service.GetAllCars();
            }

            // Assert
            Assert.Equal(6, results.Count());
        }

        [Theory]
        [InlineData(1, "Mazda")]
        [InlineData(2, "Jeep")]
        [InlineData(3, "Ford")]
        public async void TestGetCarByIdValidId(int testId, string expectedMake)
        {
            // Arrange
            var options = BuildDbContextOptions();
            Car result;

            // Act
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new CarRepository(context);
                
                var service = new CarService(repository);

                result = await service.GetCarById(testId);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Equal(expectedMake, result.Make);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(99)]
        public async void TestGetCarByIdInvalidId(int testId)
        {
            // Arrange
            var options = BuildDbContextOptions();
            Car result;

            // Act
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new CarRepository(context);
                
                var service = new CarService(repository);

                result = await service.GetCarById(testId);
            }

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestAddCarNonNullObject()
        {
            // Arrange
            var options = BuildDbContextOptions();
            var testEntity = new Car
            {
                Model = "Test"
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
                Assert.Equal("Test", result.Last().Model);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void TestAddNullObject()
        {
            // Arrange
            var options = BuildDbContextOptions();
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
            }
        }

        [Fact]
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void TestEditNonNullObject()
        {
            // Arrange
            var options = BuildDbContextOptions();

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

        [Fact]
        public void TestEditNullObject()
        {
            // Arrange
            var mockRepository = new Mock<ICarRepository>();
            mockRepository
                .Setup(x => x.DeleteCar(It.IsAny<int>()))
                .Verifiable();

            var service = new CarService(mockRepository.Object);

            // Act
            service.DeleteCar(10);

            // Assert
            mockRepository
                .Verify(x => x.DeleteCar(It.IsAny<int>()), Times.Once);
        }
        
        [Fact]
        public void TestDeleteCarValidId()
        {
            // Arrange
            var options = BuildDbContextOptions();

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

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestDeleteCarInvalidId()
        {
            // Arrange
            var options = BuildDbContextOptions();

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new CarRepository(context);
                
                var service = new CarService(repository);

                service.DeleteCar(7);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Car.ToList();

                Assert.Equal(6, result.Count);

                context.Database.EnsureDeleted();
            }
        }
    }
}
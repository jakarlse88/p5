using System;
using System.Collections.Generic;
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

        private void PrepareTestDb(DbContextOptions<ApplicationDbContext> contextOptions)
        {
            var testEntities = new List<Car>
            {
                new Car
                {
                    Id = 1,
                    Model = "Mazda"
                },
                new Car
                {
                    Id = 2,
                    Model = "BMW"
                },
                new Car
                {
                    Id = 3,
                    Model = "Mercedes"
                }
            };

            using (var context = new ApplicationDbContext(contextOptions))
            {
                foreach (var item in testEntities)
                {
                    context.Car.Add(item);
                }

                context.SaveChanges();
            }
        }

        [Fact]
        public async void TestGetAllCars()
        {
            // Arrange
            var options = BuildDbContextOptions();
            PrepareTestDb(options);
            IEnumerable<Car> results;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new CarRepository(context);
                var service = new CarService(repository);

                results = await service.GetAllCars();
            }

            // Assert
            Assert.Equal(3, results.Count());
        }

        [Theory]
        [InlineData(1, "Mazda")]
        [InlineData(2, "BMW")]
        [InlineData(3, "Mercedes")]
        public async void TestGetCarByIdValidId(int testId, string expectedModel)
        {
            // Arrange
            var options = BuildDbContextOptions();
            PrepareTestDb(options);
            Car result;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new CarRepository(context);
                var service = new CarService(repository);

                result = await service.GetCarById(testId);
            }

            // Assert
            Assert.Equal(expectedModel, result.Model);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(99)]
        public async void TestGetCarByIdInvalidId(int testId)
        {
            // Arrange
            var options = BuildDbContextOptions();
            PrepareTestDb(options);
            Car result;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
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
                Id = 1,
                Model = "Test"
            };

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new CarRepository(context);
                var service = new CarService(repository);

                service.Add(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Car.ToList();

                Assert.Single(result);
                Assert.Equal(1, result.First().Id);
            }
        }

        [Fact]
        public void TestAddNullObject()
        {
            // Arrange
            var options = BuildDbContextOptions();
            Car testObject = null;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new CarRepository(context);
                var service = new CarService(repository);

                service.Add(testObject);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Car.ToList();

                Assert.Empty(result);
            }
        }

        [Fact]
        public void TestEditNonNullObject()
        {
            // Arrange
            var options = BuildDbContextOptions();
            PrepareTestDb(options);

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new CarRepository(context);
                var service = new CarService(repository);

                var car = context.Car.ToList().FirstOrDefault(i => i.Id == 1);

                car.Model = "VW";

                service.Edit(car);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Car.ToList().FirstOrDefault(i => i.Id == 1);

                Assert.Equal("VW", result.Model);
            }
        }

        [Fact]
        public void TestEditNullObject()
        {
            // Arrange
            var options = BuildDbContextOptions();
            PrepareTestDb(options);

            // Act
            var mockRepository = new Mock<ICarRepository>();
            mockRepository
                .Setup(x => x.Delete(It.IsAny<int>()))
                .Verifiable();

            var service = new CarService(mockRepository.Object);

            service.Delete(10);

            // Assert
            mockRepository
                .Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }
    }
}
using TheCarHub.Data;
using TheCarHub.Repositories;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using TheCarHub.Models;
using System.Collections.Generic;
using System.Linq;

namespace TheCarHub.Tests
{
    [Collection("DB")]
    public class CarRepositoryTests
    {
        private readonly Car[] _testEntities = new Car[6]
        {
            new Car {
                Id = 1,
                VIN = "",
                Year = new DateTime(1991),
                Make = "Mazda",
                Model = "Miata",
                Trim = "LE",
            },
            new Car {
                Id = 2,
                VIN = "",
                Year = new DateTime(2007),
                Make = "Jeep",
                Model = "Liberty",
                Trim = "Sport",
            },
            new Car {
                Id = 3,
                VIN = "",
                Year = new DateTime(2017),
                Make = "Ford",
                Model = "Explorer",
                Trim = "XLT",
            },
            new Car {
                Id = 4,
                VIN = "",
                Year = new DateTime(2008),
                Make = "Honda",
                Model = "Civic",
                Trim = "LX",
            },
            new Car {
                Id = 5,
                VIN = "",
                Year = new DateTime(2016),
                Make = "Volkswagen",
                Model = "GTI",
                Trim = "S",
            },
            new Car {
                Id = 6,
                VIN = "",
                Year = new DateTime(2013),
                Make = "Ford",
                Model = "Edge",
                Trim = "SEL",
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
                foreach (var carEntity in _testEntities)
                {
                    context.Car.Add(carEntity);
                }

                context.SaveChanges();
            }
        }

        [Fact]
        public async void TestGetAllCarsValidId()
        {
            // Arrange
            var options = BuildTestDbOptions();
            PrepareTestDb(options);
            IList<Car> result;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new CarRepository(context);
                result = await repository.GetAllCars();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Equal(6, result.Count);
        }

        [Theory]
        [InlineData(1, "Mazda")]
        [InlineData(2, "Jeep")]
        [InlineData(3, "Ford")]
        [InlineData(4, "Honda")]
        [InlineData(5, "Volkswagen")]
        [InlineData(6, "Ford")]
        public async void TestGetCarByIdValidId(int testId, string expectedMake)
        {
            // Arrange
            var options = BuildTestDbOptions();
            PrepareTestDb(options);
            Car result;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new CarRepository(context);
                result = await repository.GetCarById(testId);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Equal(expectedMake, result.Make);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-7)]
        [InlineData(7)]
        public async void TestGetCarByIdInvalidId(int testId)
        {
            // Arrange
            var options = BuildTestDbOptions();
            PrepareTestDb(options);
            Car result;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new CarRepository(context);
                result = await repository.GetCarById(testId);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void TestDeleteCarValidId(int testId)
        {
            // Arrange
            var options = BuildTestDbOptions();
            PrepareTestDb(options);

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new CarRepository(context);

                repository.DeleteCar(testId);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var results = context.Car.ToList();
                Assert.DoesNotContain(results, c => c.Id == testId);

                context.Database.EnsureDeleted();
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(7)]
        [InlineData(-1)]
        public void TestDeleteCarInvalidId(int testId)
        {
             // Arrange
            var options = BuildTestDbOptions();
            PrepareTestDb(options);
            int expected;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new CarRepository(context);

                expected = context.Car.ToList().Count;

                repository.DeleteCar(testId);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                int actual = context.Car.ToList().Count;
                
                Assert.Equal(expected, actual);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestSaveCarValidCarEntity()
        {
            // Arrange
            var options = BuildTestDbOptions();
            var testEntity = _testEntities[0];

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new CarRepository(context);
                
                repository.SaveCar(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Car.ToList();

                Assert.Equal(1, result.First().Id);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestSaveCarNullEntity()
        {
            // Arrange
            var options = BuildTestDbOptions();
            Car testEntity = null;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new CarRepository(context);
                
                repository.SaveCar(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Car.ToList();

                Assert.Empty(result);

                context.Database.EnsureDeleted();
            }
        }
        
        [Fact]
        public void TestUpdateCarValidCar()
        {
            // Arrange
            var options = BuildTestDbOptions();
            PrepareTestDb(options);

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new CarRepository(context);
                
                var testEntity = 
                    context.Car.FirstOrDefault(c => c.Id == 1);

                testEntity.Model = "Test";

                repository.UpdateCar(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = 
                    context.Car.FirstOrDefault(c => c.Id == 1);

                Assert.Equal("Test", result.Model);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestUpdateCarNullEntity()
        {
            // Arrange
            var options = BuildTestDbOptions();
            PrepareTestDb(options);

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new CarRepository(context);
                
                var testEntity = 
                    context.Car.FirstOrDefault(c => c.Id == 10);

                repository.UpdateCar(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = 
                    context.Car.FirstOrDefault(c => c.Id == 1);

                Assert.Equal("Mazda", result.Make);

                context.Database.EnsureDeleted();
            }
        }
    }
}
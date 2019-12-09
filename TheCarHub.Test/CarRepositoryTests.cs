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
        public async void TestGetAllCarsValidId()
        {
            // Arrange
            var options = BuildTestDbOptions();
            IList<Car> result;

            // Act
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
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
            Car result;

            // Act
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new CarRepository(context);
                
                result = await repository.GetCarByIdAsync(testId);

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
            Car result;

            // Act
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new CarRepository(context);
                
                result = await repository.GetCarByIdAsync(testId);

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

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new CarRepository(context);

                repository.DeleteCar(testId);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var results = context.Car.ToList();
                
                Assert.DoesNotContain(results, c => c.Id == testId);
                Assert.Equal(5, results.Count);

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
            int expected;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new CarRepository(context);

                expected = context.Car.ToList().Count;

                repository.DeleteCar(testId);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var actual = context.Car.ToList().Count;
                
                Assert.Equal(expected, actual);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestAddCarValidCarEntity()
        {
            // Arrange
            var options = BuildTestDbOptions();
            var testEntity = new Car
            {
                Make = "Dacia"
            };

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new CarRepository(context);
                
                repository.AddCar(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Car.ToList();

                Assert.Equal(1, result.First().Id);
                Assert.Contains(result, c => c.Make == "Dacia");

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void TestSaveCarNullEntity()
        {
            // Arrange
            var options = BuildTestDbOptions();
            Car testEntity = null;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new CarRepository(context);
                
                repository.AddCar(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Car.ToList();

                Assert.Equal(6, result.Count);

                context.Database.EnsureDeleted();
            }
        }
        
        [Fact]
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void TestUpdateCarValidCar()
        {
            // Arrange
            var options = BuildTestDbOptions();

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
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
            var mockContext = new Mock<ApplicationDbContext>();
            
            mockContext
                .Setup(x => x.Update(It.IsAny<Car>()))
                .Verifiable();
            
            var repository = new CarRepository(mockContext.Object);
            
            // Act
            repository.UpdateCar(null);
            
            // Assert
            mockContext.Verify(x => x.Update(It.IsAny<Car>()), Times.Never);
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
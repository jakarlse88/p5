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
        private readonly CarEntity[] _testEntities = new CarEntity[6]
        {
            new CarEntity {
                CarId = 1,
                ListingEntityId = 1,
                VIN = "",
                Year = new DateTime(1991),
                Make = "Mazda",
                Model = "Miata",
                Trim = "LE",
                PurchaseDate = new DateTime(2019, 1, 7),
                PurchasePrice = 1800,
                Repairs = "Full restoration",
                RepairCost = 7600,
                LotDate = new DateTime(2019, 4, 7),
                SellingPrice = 1800 + 7600 + 500,
                SaleDate = new DateTime(2019, 4, 8)
            },
            new CarEntity {
                CarId = 2,
                ListingEntityId = 2,
                VIN = "",
                Year = new DateTime(2007),
                Make = "Jeep",
                Model = "Liberty",
                Trim = "Sport",
                PurchaseDate = new DateTime(2019, 4, 2),
                PurchasePrice = 4500m,
                Repairs = "Front wheel bearings",
                RepairCost = 350m,
                LotDate = new DateTime(2019, 4, 7),
                SellingPrice = 4500 + 350 + 500,
                SaleDate = null
            },
            new CarEntity {
                CarId = 3,
                ListingEntityId = 3,
                VIN = "",
                Year = new DateTime(2017),
                Make = "Ford",
                Model = "Explorer",
                Trim = "XLT",
                PurchaseDate = new DateTime(2019, 4, 5),
                PurchasePrice = 24350,
                Repairs = "Tyres, brakes",
                RepairCost = 1100,
                LotDate = new DateTime(2019, 4, 9),
                SellingPrice = 24350 + 1100 + 500, 
                SaleDate = null
            },
            new CarEntity {
                CarId = 4,
                ListingEntityId = 4,
                VIN = "",
                Year = new DateTime(2008),
                Make = "Honda",
                Model = "Civic",
                Trim = "LX",
                PurchaseDate = new DateTime(2019, 4, 6),
                PurchasePrice = 4000,
                Repairs = "Ac, brakes",
                RepairCost = 475,
                LotDate = new DateTime(2019, 4, 9),
                SellingPrice = 4000 + 475 + 500,
                SaleDate = new DateTime(2019, 4, 9)
            },
            new CarEntity {
                CarId = 5,
                ListingEntityId = 5,
                VIN = "",
                Year = new DateTime(2016),
                Make = "Volkswagen",
                Model = "GTI",
                Trim = "S",
                PurchaseDate = new DateTime(2019, 4, 6),
                PurchasePrice = 15250,
                Repairs = "Tyres",
                RepairCost = 440,
                LotDate = new DateTime(2019, 4, 10),
                SellingPrice = 15250 + 440 + 500,
                SaleDate = new DateTime(2019, 4, 12)
            },
            new CarEntity {
                CarId = 6,
                ListingEntityId = 6,
                VIN = "",
                Year = new DateTime(2013),
                Make = "Ford",
                Model = "Edge",
                Trim = "SEL",
                PurchaseDate = new DateTime(2019, 4, 7),
                PurchasePrice = 10990,
                Repairs = "Tyres, brakes, AC",
                RepairCost = 950,
                LotDate = new DateTime(2019, 4, 11),
                SellingPrice = 10990 + 950 + 500,
                SaleDate = new DateTime(2019, 4, 12)
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
                    context.Cars.Add(carEntity);
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
            IList<CarEntity> result;

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
            CarEntity result;

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
            CarEntity result;

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
                var results = context.Cars.ToList();
                Assert.DoesNotContain(results, c => c.CarId == testId);

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

                expected = context.Cars.ToList().Count;

                repository.DeleteCar(testId);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                int actual = context.Cars.ToList().Count;
                
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
                var result = context.Cars.ToList();

                Assert.Equal(1, result.First().CarId);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestSaveCarNullEntity()
        {
            // Arrange
            var options = BuildTestDbOptions();
            CarEntity testEntity = null;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new CarRepository(context);
                
                repository.SaveCar(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Cars.ToList();

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
                    context.Cars.FirstOrDefault(c => c.CarId == 1);

                testEntity.Model = "Test";

                repository.UpdateCar(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = 
                    context.Cars.FirstOrDefault(c => c.CarId == 1);

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
                    context.Cars.FirstOrDefault(c => c.CarId == 10);

                repository.UpdateCar(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = 
                    context.Cars.FirstOrDefault(c => c.CarId == 1);

                Assert.Equal("Mazda", result.Make);

                context.Database.EnsureDeleted();
            }
        }
    }
}
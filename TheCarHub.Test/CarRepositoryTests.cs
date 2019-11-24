using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Data;
using TheCarHub.Models.Entities;
using TheCarHub.Repositories;
using Xunit;

namespace TheCarHub.Test
{
//    [Collection("DB")]
    public class CarRepositoryTests
    {
        private readonly Car[] _testEntities = new Car[6]
        {
            new Car
            {
//                Id = 1,
                VIN = "",
                Year = new DateTime(1991),
                Make = "Mazda",
                Model = "Miata",
                Trim = "LE",
            },
            new Car
            {
//                Id = 2,
                VIN = "",
                Year = new DateTime(2007),
                Make = "Jeep",
                Model = "Liberty",
                Trim = "Sport",
            },
            new Car
            {
//                Id = 3,
                VIN = "",
                Year = new DateTime(2017),
                Make = "Ford",
                Model = "Explorer",
                Trim = "XLT",
            },
            new Car
            {
//                Id = 4,
                VIN = "",
                Year = new DateTime(2008),
                Make = "Honda",
                Model = "Civic",
                Trim = "LX",
            },
            new Car
            {
//                Id = 5,
                VIN = "",
                Year = new DateTime(2016),
                Make = "Volkswagen",
                Model = "GTI",
                Trim = "S",
            },
            new Car
            {
//                Id = 6,
                VIN = "",
                Year = new DateTime(2013),
                Make = "Ford",
                Model = "Edge",
                Trim = "SEL",
            }
        };

        private DbContextOptions<ApplicationDbContext> BuildTestDbOptions(SqliteConnection connection)
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
                IList<Car> result;
                var options = BuildTestDbOptions(connection);
                
                // Act
                await using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();
                    
                    var repository = new CarRepository(context);
                    
                    result = await repository.GetAllCars();
                }

                await using (var context = new ApplicationDbContext(options))
                {
                    // Assert
                    Assert.Equal(context.Car.Count(), result.Count);

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
        [InlineData(4, "Honda")]
        [InlineData(5, "Volkswagen")]
        [InlineData(6, "Ford")]
        public async void TestGetCarByIdValidId(int testId, string expectedMake)
        {
            // Arrange
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = BuildTestDbOptions(connection);
                Car result;
                
                // Act
                await using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();
                    
                    var repository = new CarRepository(context);
                    
                    result = await repository.GetCarById(testId);

                    context.Database.EnsureDeleted();
                }
                
                // Assert
                Assert.Equal(expectedMake, result.Make);
            }
            finally
            {
                connection.Close();
            }
        }
//
        [Theory]
        [InlineData(0)] //SQLite AI starts rowID at 0
        [InlineData(-7)]
        [InlineData(70)]
        public async void TestGetCarByIdInvalidId(int testId)
        {
            // Arrange
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = BuildTestDbOptions(connection);
                
                Car result;
                
                // Act
                await using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();
                    
                    var repository = new CarRepository(context);
                    
                    result = await repository.GetCarById(testId);

                    context.Database.EnsureDeleted();
                }

                // Assert
                Assert.Null(result);
            }
            finally
            {
                connection.Close();
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void TestDeleteCarValidId(int testId)
        {
            // Arrange
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = BuildTestDbOptions(connection);

                // Act
                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();
                    
                    var repository = new CarRepository(context);

                    repository.DeleteCar(testId);
                }

                using (var context = new ApplicationDbContext(options))
                {
                    // Assert
                    Assert.DoesNotContain(context.Car.ToList(), c => c.Id == testId);

                    context.Database.EnsureDeleted();
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Theory]
        [InlineData(77)]
        [InlineData(-1)]
        public void TestDeleteCarInvalidId(int testId)
        {
            // Arrange
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = BuildTestDbOptions(connection);
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
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void TestAddCarValidCarEntity()
        {
            // Arrange
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = BuildTestDbOptions(connection);
                var testEntity = new Car
                {
                    VIN = "",
                    Year = new DateTime(1991),
                    Make = "Dacia",
                    Model = "Sandero",
                    Trim = "",
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

                    Assert.Contains(result, c => c.Make == "Dacia");

                    context.Database.EnsureDeleted();
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void TestSaveCarNullEntity()
        {
            // Arrange
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = BuildTestDbOptions(connection);
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
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void TestUpdateCarValidCar()
        {
            // Arrange
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = BuildTestDbOptions(connection);

                // Act
                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();
                    
                    var repository = new CarRepository(context);

                    var testEntity =
                        context.Car.FirstOrDefault(c => c.Id == 1);

                    testEntity.Model = "Test";

                    repository.EditCar(testEntity);
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
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void TestUpdateCarNullEntity()
        {
            // Arrange
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = BuildTestDbOptions(connection);

                // Act
                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();
                    
                    var repository = new CarRepository(context);

                    var testEntity =
                        context.Car.FirstOrDefault(c => c.Id == 10);

                    repository.EditCar(testEntity);
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
            finally
            {
                connection.Close();
            }
        }
    }
}
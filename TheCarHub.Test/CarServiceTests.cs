using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Data;
using TheCarHub.Models.Entities;
using TheCarHub.Models.InputModels;
using TheCarHub.Repositories;
using TheCarHub.Services;
using Xunit;

namespace TheCarHub.Test
{
    public class CarServiceTests
    {
        private DbContextOptions<ApplicationDbContext> DbContextOptions
        {
            get
            {
                var options =
                    new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString())
                        .Options;

                return options;
            }
        }

        [Fact]
        public void TestMapCarValuesInputModelNull()
        {
            // Arrange
            var options = DbContextOptions;

            using (var context = new ApplicationDbContext(options))
            {
                var repository = new CarRepository(context);

                var service = new CarService(repository);
                
                // Act
                Action testAction = () => service.MapCarValues(null, null);
            
                // Assert
                var ex = Assert.Throws<Exception>(testAction);
                Assert.Equal("InputModel argument cannot be null.", ex.Message);
            }
        }

        [Fact]
        public void TestMapCarValuesCarEntityNull()
        {
            // Arrange
            var options = DbContextOptions;

            using (var context = new ApplicationDbContext(options))
            {
                var repository = new CarRepository(context);

                var service = new CarService(repository);
                
                // Act
                Action testAction = () => service.MapCarValues(new CarInputModel(), null);
            
                // Assert
                var ex = Assert.Throws<Exception>(testAction);
                Assert.Equal("Car entity not found.", ex.Message);
            }
        }

        [Fact]
        public void TestMapCarValuesValidArguments()
        {
            // Arrange
            var options = DbContextOptions;
            
            Car entity;
            
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var repository = new CarRepository(context);

                var service = new CarService(repository);

                entity = context.Car.FirstOrDefault(c => c.Id == 1);
                Assert.NotNull(entity);

                var inputModel = new CarInputModel
                {
                    Id = 1,
                    Make = "test make",
                    Model = "test model",
                    Trim = "test trim",
                    VIN = "test VIN"
                };

                // Act
                service.MapCarValues(inputModel, entity);

                context.Database.EnsureDeleted();
            }
            
            // Assert
            Assert.Equal("test make", entity.Make);
            Assert.Equal("test model", entity.Model);
            Assert.Equal("test trim", entity.Trim);
            Assert.Equal("test VIN", entity.VIN);
        }
    }
}
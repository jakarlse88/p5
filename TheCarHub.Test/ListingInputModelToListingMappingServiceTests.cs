using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using TheCarHub.Data;
using TheCarHub.Models.Entities;
using TheCarHub.Models.InputModels;
using TheCarHub.Repositories;
using TheCarHub.Services;
using Xunit;

namespace TheCarHub.Test
{
    public class ListingInputModelToListingMappingServiceTests
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
        public async Task TestMapSourceNull()
        {
            // Arrange
            var options = BuildDbContextOptions();
            Listing result;

            await using (var context = new ApplicationDbContext(options))
            {
                var statusRepository = new StatusRepository(context);
                var mappingService =
                    new ListingInputModelToListingMappingService(statusRepository);

                // Act
                result = await mappingService.Map(null);
            }

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task TestMapSourceCarNull()
        {
            // Arrange
            var options = BuildDbContextOptions();
            var source = new ListingInputModel
            {
                Car = null
            };
            Listing result;

            await using (var context = new ApplicationDbContext(options))
            {
                var statusRepository = new StatusRepository(context);
                var mappingService =
                    new ListingInputModelToListingMappingService(statusRepository);

                // Act
                result = await mappingService.Map(source);
            }

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void TestMapSourceRepairJobNull()
        {
            // Arrange
            var options = BuildDbContextOptions();
            var source = new ListingInputModel
            {
                RepairJob = null
            };
            Listing result;

            await using (var context = new ApplicationDbContext(options))
            {
                var statusRepository = new StatusRepository(context);
                var mappingService =
                    new ListingInputModelToListingMappingService(statusRepository);

                // Act
                result = await mappingService.Map(source);
            }

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1988)]
        [InlineData(2021)]
        public async Task TestMapPopulatedCarInvalidCarYear(int carYear)
        {
            // Arrange
            var options = BuildDbContextOptions();

            var source = new ListingInputModel
            {
                Car = new CarInputModel(),
                RepairJob = new RepairJobInputModel(),
                CarYear = carYear
            };

            source.Car.VIN = "VIN";
            source.Car.Make = "BMW";
            source.Car.Model = "320i";
            source.Car.Trim = "";

            Listing result;

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var statusRepository = new StatusRepository(context);
                var mappingService =
                    new ListingInputModelToListingMappingService(statusRepository);

                // Act
                result = await mappingService.Map(source);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotNull(result);
            Assert.Equal("VIN", result.Car.VIN);
            Assert.Equal("BMW", result.Car.Make);
            Assert.Equal("320i", result.Car.Model);
            Assert.Equal("", result.Car.Trim);
            Assert.Equal(new DateTime(2019, 1, 1), result.Car.Year);
        }

        [Theory]
        [InlineData(1990)]
        [InlineData(2012)]
        [InlineData(2020)]
        public async Task TestMapPopulatedCarValidCarYear(int carYear)
        {
            // Arrange
            var options = BuildDbContextOptions();

            var source = new ListingInputModel
            {
                Car = new CarInputModel(),
                RepairJob = new RepairJobInputModel(),
                CarYear = carYear
            };

            source.Car.VIN = "VIN";
            source.Car.Make = "BMW";
            source.Car.Model = "320i";
            source.Car.Trim = "";

            Listing result;

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var statusRepository = new StatusRepository(context);
                var mappingService =
                    new ListingInputModelToListingMappingService(statusRepository);

                // Act
                result = await mappingService.Map(source);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotNull(result);
            Assert.Equal("VIN", result.Car.VIN);
            Assert.Equal("BMW", result.Car.Make);
            Assert.Equal("320i", result.Car.Model);
            Assert.Equal("", result.Car.Trim);
            Assert.Equal(new DateTime(carYear, 1, 1), result.Car.Year);
        }

        [Fact]
        public async Task TestMapPopulatedRepairJob()
        {
            // Arrange
            var options = BuildDbContextOptions();

            var source = new ListingInputModel
            {
                Car = new CarInputModel(),
                RepairJob = new RepairJobInputModel(),
            };

            source.RepairJob.Cost = 100;
            source.RepairJob.Description = "Test repairs.";

            Listing result;

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var statusRepository = new StatusRepository(context);
                var mappingService =
                    new ListingInputModelToListingMappingService(statusRepository);

                // Act
                result = await mappingService.Map(source);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotNull(result.RepairJob);
            Assert.Equal(100, result.RepairJob.Cost);
            Assert.Equal("Test repairs.", result.RepairJob.Description);
        }

        [Fact]
        public async Task TestMapPopulatedImgNames()
        {
            // Arrange
            var options = BuildDbContextOptions();

            var source = new ListingInputModel
            {
                Car = new CarInputModel(),
                RepairJob = new RepairJobInputModel(),
                ImgNames =
                {
                    "asd.jpg",
                    "123.jpg",
                    "qwer.jpg",
                    "456.jpg"
                }
            };

            Listing result;

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var statusRepository = new StatusRepository(context);
                var mappingService =
                    new ListingInputModelToListingMappingService(statusRepository);

                // Act
                result = await mappingService.Map(source);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotNull(result.Media);
            Assert.Equal(4, result.Media.Count);
            Assert.Contains(result.Media, m => m.FileName == "qwer.jpg");
        }

        [Fact]
        public async Task TestMapPopulatedListing()
        {
            // Arrange
            var options = BuildDbContextOptions();

            var source = new ListingInputModel
            {
                Car = new CarInputModel(),
                RepairJob = new RepairJobInputModel(),
                Title = "Test title",
                Description = "Test description",
                PurchaseDate = DateTime.Today,
                PurchasePrice = 1000,
                SellingPrice = 2000,
            };

            Listing result;

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var statusRepository = new StatusRepository(context);
                var mappingService =
                    new ListingInputModelToListingMappingService(statusRepository);

                // Act
                result = await mappingService.Map(source);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test title", result.Title);
            Assert.Equal("Test description", result.Description);
            Assert.Equal(DateTime.Today, result.PurchaseDate);
            Assert.Equal(1000, result.PurchasePrice);
            Assert.Equal(2000, result.SellingPrice);
        }

    }
}
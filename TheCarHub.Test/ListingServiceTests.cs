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
//    [Collection("DB")]
    public class ListingServiceTests
    {
        private DbContextOptions<ApplicationDbContext> BuildDbContextOptions()
        {
            var options =
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlServer("Server=localhost,1433; Database=ListingServiceTestDB; User=sa; Password=reallyStrongPwd123;")
                    .Options;

            return options;
        }

        [Fact]
        public async Task TestGetAllListingsAsync()
        {
            // Arrange
            var options = BuildDbContextOptions();
            IEnumerable<Listing> result;

            // Act
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var listingRepository = new ListingRepository(context);
                
                var service = new ListingService(listingRepository, null);
                
                result = await service.GetAllListings();
            }

            // Assert
            await using (var context = new ApplicationDbContext(options))
            {
                Assert.Equal(
                    context.Listing.ToList().Count, result.Count());

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async void TestGetListingById()
        {
            // Arrange
            var options = BuildDbContextOptions();
            Listing result;

            // Act
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var listingRepository = new ListingRepository(context);
                var service = new ListingService(listingRepository, new StatusRepository(context));

                result = await service.GetListingById(1);
            }

            // Assert
            await using (var context = new ApplicationDbContext(options))
            {
                Assert.Equal(1, result.Id);
                
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async void TestEditNonNullObject()
        {
            // Arrange
            var options = BuildDbContextOptions();

            // Act
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var listingRepository = new ListingRepository(context);
                
                var service = new ListingService(listingRepository, null);

                var listing = await service.GetListingById(1);

                listing.Title = "edited";

                service.EditListing(listing);
            }

            // Assert
            await using (var context = new ApplicationDbContext(options))
            {
                var result = context.Listing.FirstOrDefault(l => l.Id == 1);
                
                Assert.Equal("edited", result.Title);
                
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestEditNullObject()
        {
            // Arrange
            Listing testObject = null;

            var mockRepository = new Mock<IListingRepository>();
            
            mockRepository
                .Setup(mr => mr.EditListing(It.IsAny<Listing>()))
                .Verifiable();
            
            var service = new ListingService(mockRepository.Object, null);

            // Act
            service.EditListing(testObject);

            // Assert
            mockRepository
                .Verify(mr => mr.EditListing(It.IsAny<Listing>()), Times.Never);
        }

        [Fact]
        public void TestAddNullObject()
        {
            // Arrange
            var options = BuildDbContextOptions();
            ListingInputModel testObject = null;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var listingRepository = new ListingRepository(context);
                var service = new ListingService(listingRepository, new StatusRepository(context));

                service.AddListing(testObject);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Listing.ToList();

                Assert.Equal(6, result.Count);
                
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestDeleteValidId()
        {
            // Arrange
            var options = BuildDbContextOptions();

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var listingRepository = new ListingRepository(context);
                
                var service = new ListingService(listingRepository, null);

                service.DeleteListing(1);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Listing.ToList();

                Assert.DoesNotContain(result, l => l.Id == 1);
                Assert.Equal(5, result.Count);
                
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestDeleteInvalidId()
        {
            // Arrange
            var options = BuildDbContextOptions();

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                var listingRepository = new ListingRepository(context);
                
                var service = new ListingService(listingRepository, null);

                service.DeleteListing(666);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Listing.ToList();

                Assert.Equal(6, result.Count);
                
                context.Database.EnsureDeleted();
            }
        }
    }
}
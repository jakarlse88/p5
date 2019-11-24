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
    [Collection("DB")]
    public class ListingServiceTests
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
            var testEntities = new List<Listing>
            {
                new Listing
                {
                    Id = 1,
                    Title = "test one"
                },
                new Listing
                {
                    Id = 2,
                    Title = "test two"
                },
                new Listing
                {
                    Id = 3,
                    Title = "test three"
                }
            };

            using (var context = new ApplicationDbContext(contextOptions))
            {
                context.Database.EnsureCreated();
                
                foreach (var item in testEntities)
                {
                    context.Listing.Add(item);
                }

                context.SaveChanges();
            }
        }

        [Fact]
        public async Task TestGetAllListingsAsync()
        {
            // Arrange
            var options = BuildDbContextOptions();
            PrepareTestDb(options);
            IEnumerable<Listing> result;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var listingRepository = new ListingRepository(context);
                var service = new ListingService(listingRepository, new StatusRepository(context));
                result = await service.GetAllListings();
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
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
            PrepareTestDb(options);
            Listing result;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var listingRepository = new ListingRepository(context);
                var service = new ListingService(listingRepository, new StatusRepository(context));

                result = await service.GetListingById(1);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
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
            PrepareTestDb(options);

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var listingRepository = new ListingRepository(context);
                var service = new ListingService(listingRepository, new StatusRepository(context));

                var listing = await service.GetListingById(1);

                listing.Title = "edited";

                service.EditListing(listing);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var listingRepository = new ListingRepository(context);
                var service = new ListingService(listingRepository, new StatusRepository(context));

                var result = await service.GetListingById(1);

                Assert.Equal("edited", result.Title);
                
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async void TestEditNullObject()
        {
            // Arrange
            var options = BuildDbContextOptions();
            PrepareTestDb(options);
            Listing testObject = null;

            // Act
            var mockRepository = new Mock<IListingRepository>();
            
            mockRepository
                .Setup(mr => mr.EditListing(It.IsAny<Listing>()))
                .Verifiable();
            
            var mockStatusRepository = new Mock<IStatusRepository>();

            var service = new ListingService(mockRepository.Object, mockStatusRepository.Object);

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
                var listingRepository = new ListingRepository(context);
                var service = new ListingService(listingRepository, new StatusRepository(context));

                service.AddListing(testObject);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Listing.ToList();

                Assert.Empty(result);
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestDeleteValidId()
        {
            // Arrange
            var options = BuildDbContextOptions();
            PrepareTestDb(options);

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var listingRepository = new ListingRepository(context);
                var service = new ListingService(listingRepository, new StatusRepository(context));

                service.DeleteListing(1);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Listing.ToList();

                Assert.DoesNotContain(result, l => l.Id == 1);
                
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestDeleteInvalidId()
        {
            // Arrange
            var options = BuildDbContextOptions();
            PrepareTestDb(options);

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var listingRepository = new ListingRepository(context);
                var service = new ListingService(listingRepository, new StatusRepository(context));

                service.DeleteListing(4);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Listing.ToList();

                Assert.Equal(3, result.Count);
                
                context.Database.EnsureDeleted();
            }
        }
    }
}
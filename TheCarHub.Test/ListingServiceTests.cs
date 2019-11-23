//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using TheCarHub.Data;
//using TheCarHub.Models.Entities;
//using TheCarHub.Repositories;
//using TheCarHub.Services;
//using Xunit;
//
//namespace TheCarHub.Test
//{
//    public class ListingServiceTests
//    {
//        private readonly Mock<IStatusRepository> _mockStatusRepository;
//
//        public ListingServiceTests()
//        {
//            _mockStatusRepository = new Mock<IStatusRepository>();
//            
//            _mockStatusRepository.Setup()
//        }
//        
//        private DbContextOptions<ApplicationDbContext> BuildDbContextOptions()
//        {
//            var options =
//                new DbContextOptionsBuilder<ApplicationDbContext>()
//                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
//                    .Options;
//            return options;
//        }
//
//        private void PrepareTestDb(DbContextOptions<ApplicationDbContext> contextOptions)
//        {
//            var testEntities = new List<Listing>
//            {
//                new Listing
//                {
//                    Id = 1,
//                    Title = "test one"
//                },
//                new Listing
//                {
//                    Id = 2,
//                    Title = "test two"
//                },
//                new Listing
//                {
//                    Id = 3,
//                    Title = "test three"
//                }
//            };
//
//            using (var context = new ApplicationDbContext(contextOptions))
//            {
//                foreach (var item in testEntities)
//                {
//                    context.Listing.Add(item);
//                }
//
//                context.SaveChanges();
//            }
//        }
//
//        [Fact]
//        public async Task TestGetAllListingsAsync()
//        {
//            // Arrange
//            var options = BuildDbContextOptions();
//            PrepareTestDb(options);
//            IEnumerable<Listing> result;
//
//            // Act
//            using (var context = new ApplicationDbContext(options))
//            {
//                var repository = new ListingRepository(context);
//                var service = new ListingService(repository);
//                result = await service.GetAllListings();
//            }
//
//            // Assert
//            using (var context = new ApplicationDbContext(options))
//            {
//                Assert.Equal(
//                    context.Listing.ToList().Count, result.Count());
//            }
//        }
//
//        [Fact]
//        public async void TestGetListingById()
//        {
//            // Arrange
//            var options = BuildDbContextOptions();
//            PrepareTestDb(options);
//            Listing result;
//
//            // Act
//            using (var context = new ApplicationDbContext(options))
//            {
//                var repository = new ListingRepository(context);
//                var service = new ListingService(repository);
//
//                result = await service.GetListingById(1);
//            }
//
//            // Assert
//            using (var context = new ApplicationDbContext(options))
//            {
//                Assert.Equal(1, result.Id);
//            }
//        }
//
//        [Fact]
//        public async void TestEditNonNullObject()
//        {
//            // Arrange
//            var options = BuildDbContextOptions();
//            PrepareTestDb(options);
//
//            // Act
//            using (var context = new ApplicationDbContext(options))
//            {
//                var repository = new ListingRepository(context);
//                var service = new ListingService(repository);
//
//                var listing = await service.GetListingById(1);
//
//                listing.Title = "edited";
//
//                service.EditListing(listing);
//            }
//
//            // Assert
//            using (var context = new ApplicationDbContext(options))
//            {
//                var repository = new ListingRepository(context);
//                var service = new ListingService(repository);
//
//                var result = await service.GetListingById(1);
//
//                Assert.Equal("edited", result.Title);
//            }
//        }
//
//        [Fact]
//        public async void TestEditNullObject()
//        {
//            // Arrange
//            var options = BuildDbContextOptions();
//            PrepareTestDb(options);
//            Listing testObject = null;
//
//            // Act
//            var mockRepository = new Mock<IListingRepository>();
//
//            mockRepository
//                .Setup(mr => mr.EditListing(It.IsAny<Listing>()))
//                .Verifiable();
//
//            var service = new ListingService(mockRepository.Object);
//
//            service.EditListing(testObject);
//
//            // Assert
//            mockRepository
//                .Verify(mr => mr.EditListing(It.IsAny<Listing>()), Times.Never);
//        }
//
//        [Fact]
//        public void TestAddNonNullObject()
//        {
//            // Arrange
//            var options = BuildDbContextOptions();
//
//            var testEntity = new Listing
//            {
//                Id = 8,
//                Title = "Test"
//            };
//
//            // Act
//            using (var context = new ApplicationDbContext(options))
//            {
//                var repository = new ListingRepository(context);
//                var service = new ListingService(repository);
//
//                service.AddListing(testEntity);
//            }
//
//            // Assert
//            using (var context = new ApplicationDbContext(options))
//            {
//                var result = context.Listing.ToList();
//
//                Assert.Single(result);
//                Assert.Equal(8, result.First().Id);
//            }
//        }
//
//        [Fact]
//        public void TestAddNullObject()
//        {
//            // Arrange
//            var options = BuildDbContextOptions();
//            Listing testObject = null;
//
//            // Act
//            using (var context = new ApplicationDbContext(options))
//            {
//                var repository = new ListingRepository(context);
//                var service = new ListingService(repository);
//
//                service.AddListing(testObject);
//            }
//
//            // Assert
//            using (var context = new ApplicationDbContext(options))
//            {
//                var result = context.Listing.ToList();
//
//                Assert.Empty(result);
//            }
//        }
//
//        [Fact]
//        public void TestDeleteValidId()
//        {
//            // Arrange
//            var options = BuildDbContextOptions();
//            PrepareTestDb(options);
//
//            // Act
//            using (var context = new ApplicationDbContext(options))
//            {
//                var repository = new ListingRepository(context);
//                var service = new ListingService(repository);
//
//                service.DeleteListing(1);
//            }
//
//            // Assert
//            using (var context = new ApplicationDbContext(options))
//            {
//                var result = context.Listing.ToList();
//
//                Assert.DoesNotContain(result, l => l.Id == 1);
//            }
//        }
//
//        [Fact]
//        public void TestDeleteInvalidId()
//        {
//            // Arrange
//            var options = BuildDbContextOptions();
//            PrepareTestDb(options);
//
//            // Act
//            using (var context = new ApplicationDbContext(options))
//            {
//                var repository = new ListingRepository(context);
//                var service = new ListingService(repository);
//
//                service.DeleteListing(4);
//            }
//
//            // Assert
//            using (var context = new ApplicationDbContext(options))
//            {
//                var result = context.Listing.ToList();
//
//                Assert.Equal(3, result.Count);
//            }
//        }
//    }
//}
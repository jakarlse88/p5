using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Moq;
using TheCarHub.Data;
using TheCarHub.Models;
using TheCarHub.Repositories;
using Xunit;
using System.Linq;
using TheCarHub.Services;
using System.Threading.Tasks;

namespace TheCarHub.Tests
{
    public class ListingServiceTests
    {
        [Fact]
        public async Task TestGetAllListingsAsync()
        {
            // Arrange
            var options = 
                new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var testListings = new List<Listing>
            {
                new Listing {
                    Id = 1,
                    Title = "test one"
                },
                new Listing {
                    Id = 2,
                    Title = "test two"
                },
                new Listing {
                    Id = 3, 
                    Title = "test three"
                }
            };

            IEnumerable<Listing> result;

                var mockRepository = new Mock<IListingRepository>();

                mockRepository
                    .Setup(x => x.GetAllListings())
                    .ReturnsAsync(testListings);

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var service = new ListingService(mockRepository.Object);
                
                result = await service.GetAllListings();
            }

            // Assert
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void TestGetListingById()
        {
            // Arrange
            var mockRepository = new Mock<IListingRepository>();
            mockRepository
                .Setup(x => x.GetListingById(It.IsAny<int>()))
                .Verifiable();

            // Act
            var service = new ListingService(mockRepository.Object);
            var result = service.GetListingById(1);

            // Assert
            mockRepository
                .Verify(x => x.GetListingById(It.IsAny<int>()), Times.Once);
        }
    }
}
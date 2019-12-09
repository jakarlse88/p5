using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

                var repository = new ListingRepository(context);

                var service = new ListingService(repository, null, null, null, null, null);

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

                var repository = new ListingRepository(context);

                var service = new ListingService(repository, null, null, null, null, null);

                result = await service.GetListingByIdAsync(1);
            }

            // Assert
            await using (var context = new ApplicationDbContext(options))
            {
                Assert.Equal(1, result.Id);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void TestEditListingBothArgsNull()
        {
            // Arrange
            var mockRepository = new Mock<IListingRepository>();

            mockRepository
                .Setup(lr => lr.UpdateListing(It.IsAny<Listing>()))
                .Verifiable();

            var service = new ListingService(mockRepository.Object, null,null, null, null, null);

            // Act
            service.EditListing(null, null);

            // Assert
            mockRepository
                .Verify(ml => ml.UpdateListing(It.IsAny<Listing>()), Times.Never);
        }

        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void TestEditListingListingArgNull()
        {
            // Arrange
            var mockRepository = new Mock<IListingRepository>();

            mockRepository
                .Setup(lr => lr.UpdateListing(It.IsAny<Listing>()))
                .Verifiable();

            var service = new ListingService(mockRepository.Object, null, null, null, null, null);

            var inputModel = new ListingInputModel();

            // Act
            service.EditListing(inputModel, null);

            // Assert
            mockRepository
                .Verify(ml => ml.UpdateListing(It.IsAny<Listing>()), Times.Never);
        }

        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void TestEditListingInputModelArgNull()
        {
            // Arrange
            var mockRepository = new Mock<IListingRepository>();

            mockRepository
                .Setup(lr => lr.UpdateListing(It.IsAny<Listing>()))
                .Verifiable();

            var service = new ListingService(mockRepository.Object, null, null, null, null, null);

            var listing = new Listing();

            // Act
            service.EditListing(null, listing);

            // Assert
            mockRepository
                .Verify(ml => ml.UpdateListing(It.IsAny<Listing>()), Times.Never);
        }

        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void TestAddNullObject()
        {
            // Arrange
            var options = BuildDbContextOptions();
            ListingInputModel testObject = null;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new ListingRepository(context);

                var service = new ListingService(repository, null, null, null, null, null);

                service.AddListingAsync(testObject);
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

                var repository = new ListingRepository(context);

                var service = new ListingService(repository, null, null, null, null, null);

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

                var repository = new ListingRepository(context);

                var service = new ListingService(repository, null, null, null, null, null);

                service.DeleteListing(7);
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
        public void TestUpdateListingExperimentalAsyncArgumentNull()
        {
            // Arrange
            var options = BuildDbContextOptions();

            var mockRepository = new Mock<IListingRepository>();
            mockRepository
                .Setup(x => x.GetListingById(It.IsAny<int>()))
                .Verifiable();
            
            var service = new ListingService(
                mockRepository.Object,
                null,
                null,
                null,
                null,
                null);
            
            // Act
            service.UpdateListingExperimentalAsync(null);

            // Assert
            mockRepository
                .Verify(x => x.GetListingById(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void TestUpdateListingExperimentalAsyncSourceInvalidId()
        {
            // Arrange
            var options = BuildDbContextOptions();

            var mockRepository = new Mock<IListingRepository>();
            mockRepository
                .Setup(x => x.GetListingById(It.IsAny<int>()))
                .Verifiable();
            
            var service = new ListingService(
                mockRepository.Object,
                null,
                null,
                null,
                null,
                null);
            
            // Act
            service.UpdateListingExperimentalAsync(null);

            // Assert
            mockRepository
                .Verify(x => x.GetListingById(It.IsAny<int>()), Times.Never);
        }


    }
}
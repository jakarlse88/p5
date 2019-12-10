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
    public class ListingRepositoryTests
    {
        private DbContextOptions<ApplicationDbContext> BuildTestDbOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async void TestGetAllListingsValidId()
        {
            // Arrange
            var options = BuildTestDbOptions();
            IList<Listing> result;

            // Act
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new ListingRepository(context);

                result = await repository.GetAllListings();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Equal(6, result.Count);
        }

        [Theory]
        [InlineData(1, "one description")]
        [InlineData(2, "two description")]
        [InlineData(3, "three description")]
        [InlineData(4, "four description")]
        [InlineData(5, "five description")]
        [InlineData(6, "six description")]
        public async void TestGetListingByIdValidId(int testId, string expectedMake)
        {
            // Arrange
            var options = BuildTestDbOptions();
            Listing result;

            // Act
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new ListingRepository(context);

                result = await repository.GetListingById(testId);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Equal(expectedMake, result.Description);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-7)]
        [InlineData(7)]
        public async void TestGetListingByIdInvalidId(int testId)
        {
            // Arrange
            var options = BuildTestDbOptions();
            Listing result;

            // Act
            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new ListingRepository(context);

                result = await repository.GetListingById(testId);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TestSaveListingValidListingEntity()
        {
            // Arrange
            var options = BuildTestDbOptions();
            var testEntity = new Listing
            {
                Description = "test description"
            };

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new ListingRepository(context);

                repository.AddListing(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var results = context.Listing.ToList();

//                Assert.Equal(7, results.Last().Id);
                Assert.Equal("test description", results.Last().Description);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void TestSaveListingNullEntity()
        {
            // Arrange
            var options = BuildTestDbOptions();
            Listing testEntity = null;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new ListingRepository(context);

                repository.AddListing(testEntity);
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
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void TestUpdateListingValidEntity()
        {
            // Arrange
            var options = BuildTestDbOptions();

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new ListingRepository(context);

                var testEntity =
                    context
                        .Listing
                        .FirstOrDefault(l => l.Id == 1);

                testEntity.Description = "test description";

                repository.UpdateListing(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result =
                    context
                        .Listing
                        .FirstOrDefault(l => l.Id == 1);

                Assert.Equal("test description", result.Description);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void TestUpdateListingsNullEntity()
        {
            // Arrange
            var mockContext = new Mock<ApplicationDbContext>();

            mockContext
                .Setup(x => x.Update(It.IsAny<Listing>()))
                .Verifiable();

            var repository = new ListingRepository(mockContext.Object);

            // Act
            repository.UpdateListing(null);

            // Assert
            mockContext.Verify(x => x.Update(It.IsAny<Listing>()), Times.Never);
        }

        [Fact]
        public void TestGetListingEntityEntryValidEntity()
        {
            // Arrange
            var options = BuildTestDbOptions();

            EntityEntry<Listing> result;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new ListingRepository(context);

                var testEntity = context.Listing.FirstOrDefault();

                // Act
                result = repository.GetListingEntityEntry(testEntity);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotNull(result);
            Assert.IsType<EntityEntry<Listing>>(result);
        }

        [Fact]
        public void TestGetListingEntityEntryNull()
        {
            // Arrange
            var options = BuildTestDbOptions();

            EntityEntry<Listing> result;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var repository = new ListingRepository(context);

                // Act
                result = repository.GetListingEntityEntry(null);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Null(result);
        }
    }
}
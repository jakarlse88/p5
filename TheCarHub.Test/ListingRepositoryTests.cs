using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Data;
using TheCarHub.Models;
using TheCarHub.Repositories;
using Xunit;

namespace TheCarHub.Tests
{
    public class ListingRepositoryTests
    {
        private readonly ListingEntity[] _testEntities = new ListingEntity[]
        {
            new ListingEntity {
                ListingId = 1,
                Description = "One",
                ListingStatus = "Available",
                DateCreated = DateTime.Now,
                DateLastUpdated = null
            },
                new ListingEntity {
                ListingId = 2,
                Description = "Two",
                ListingStatus = "Available",
                DateCreated = DateTime.Now,
                DateLastUpdated = null
            },
                new ListingEntity {
                ListingId = 3,
                Description = "Three",
                ListingStatus = "Available",
                DateCreated = DateTime.Now,
                DateLastUpdated = null
            },
                new ListingEntity {
                ListingId = 4,
                Description = "Four",
                ListingStatus = "Available",
                DateCreated = DateTime.Now,
                DateLastUpdated = null
            },
                new ListingEntity {
                ListingId = 5,
                Description = "Five",
                ListingStatus = "Available",
                DateCreated = DateTime.Now,
                DateLastUpdated = null
            },
                new ListingEntity {
                ListingId = 6,
                Description = "Six",
                ListingStatus = "Available",
                DateCreated = DateTime.Now,
                DateLastUpdated = null
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
                foreach (var listingEntity in _testEntities)
                {
                    context.Listings.Add(listingEntity);
                }

                context.SaveChanges();
            }
        }

        [Fact]
        public async void TestGetAllListingsValidId()
        {
            // Arrange
            var options = BuildTestDbOptions();
            PrepareTestDb(options);
            IList<ListingEntity> result;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new ListingRepository(context);
                result = await repository.GetAllListings();

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Equal(6, result.Count);
        }

        [Theory]
        [InlineData(1, "One")]
        [InlineData(2, "Two")]
        [InlineData(3, "Three")]
        [InlineData(4, "Four")]
        [InlineData(5, "Five")]
        [InlineData(6, "Six")]
        public async void TestGetListingByIdValidId(int testId, string expectedMake)
        {
            // Arrange
            var options = BuildTestDbOptions();
            PrepareTestDb(options);
            ListingEntity result;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
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
            PrepareTestDb(options);
            ListingEntity result;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new ListingRepository(context);
                result = await repository.GetListingById(testId);

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
        public void TestDeleteListingValidId(int testId)
        {
            // Arrange
            var options = BuildTestDbOptions();
            PrepareTestDb(options);

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new ListingRepository(context);

                repository.DeleteListing(testId);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var results = context.Listings.ToList();
                Assert.DoesNotContain(results, l => l.ListingId == testId);

                context.Database.EnsureDeleted();
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(7)]
        [InlineData(-1)]
        public void TestDeleteListingInvalidId(int testId)
        {
             // Arrange
            var options = BuildTestDbOptions();
            PrepareTestDb(options);
            int expected;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new ListingRepository(context);

                expected = context.Listings.ToList().Count;

                repository.DeleteListing(testId);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                int actual = context.Listings.ToList().Count;
                
                Assert.Equal(expected, actual);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestSaveListingValidListingEntity()
        {
            // Arrange
            var options = BuildTestDbOptions();
            var testEntity = _testEntities[0];

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new ListingRepository(context);
                
                repository.SaveListing(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Listings.ToList();

                Assert.Equal(1, result.First().ListingId);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestSaveListingNullEntity()
        {
            // Arrange
            var options = BuildTestDbOptions();
            ListingEntity testEntity = null;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new ListingRepository(context);
                
                repository.SaveListing(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Listings.ToList();

                Assert.Empty(result);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestUpdateListingValidEntity()
        {
            // Arrange
            var options = BuildTestDbOptions();
            PrepareTestDb(options);

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new ListingRepository(context);
                
                var testEntity = 
                    context
                    .Listings
                    .FirstOrDefault(l => l.ListingId == 1);

                testEntity.Description = "Test";

                repository.UpdateListing(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = 
                    context
                    .Listings
                    .FirstOrDefault(l => l.ListingId == 1);

                Assert.Equal("Test", result.Description);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestUpdateListingsNullEntity()
        {
            // Arrange
            var options = BuildTestDbOptions();
            PrepareTestDb(options);

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new ListingRepository(context);
                
                var testEntity = 
                    context
                    .Listings
                    .FirstOrDefault(l => l.ListingId == 10);

                repository.UpdateListing(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = 
                    context
                    .Listings
                    .FirstOrDefault(l => l.ListingId == 1);

                Assert.Equal("One", result.Description);

                context.Database.EnsureDeleted();
            }
        }
    }
}
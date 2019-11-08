using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Data;
using TheCarHub.Models.Entities;
using TheCarHub.Repositories;
using Xunit;

namespace TheCarHub.Test
{
    public class ListingRepositoryTests
    {
        private readonly Listing[] _testEntities = new Listing[]
        {
            new Listing {
                Id = 1,
                Description = "One",
                Status = "Available",
                DateCreated = DateTime.Now,
                DateLastUpdated = null
            },
                new Listing {
                Id = 2,
                Description = "Two",
                Status = "Available",
                DateCreated = DateTime.Now,
                DateLastUpdated = null
            },
                new Listing {
                Id = 3,
                Description = "Three",
                Status = "Available",
                DateCreated = DateTime.Now,
                DateLastUpdated = null
            },
                new Listing {
                Id = 4,
                Description = "Four",
                Status = "Available",
                DateCreated = DateTime.Now,
                DateLastUpdated = null
            },
                new Listing {
                Id = 5,
                Description = "Five",
                Status = "Available",
                DateCreated = DateTime.Now,
                DateLastUpdated = null
            },
                new Listing {
                Id = 6,
                Description = "Six",
                Status = "Available",
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
                    context.Listing.Add(listingEntity);
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
            IList<Listing> result;

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
            Listing result;

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
            Listing result;

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
                var results = context.Listing.ToList();
                Assert.DoesNotContain(results, l => l.Id == testId);

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

                expected = context.Listing.ToList().Count;

                repository.DeleteListing(testId);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var actual = context.Listing.ToList().Count;
                
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
                
                repository.AddListing(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context.Listing.ToList();

                Assert.Equal(1, result.First().Id);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestSaveListingNullEntity()
        {
            // Arrange
            var options = BuildTestDbOptions();
            Listing testEntity = null;

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new ListingRepository(context);
                
                repository.AddListing(testEntity);
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
                    .Listing
                    .FirstOrDefault(l => l.Id == 1);

                testEntity.Description = "Test";

                repository.EditListing(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = 
                    context
                    .Listing
                    .FirstOrDefault(l => l.Id == 1);

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
                    .Listing
                    .FirstOrDefault(l => l.Id == 10);

                repository.EditListing(testEntity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = 
                    context
                    .Listing
                    .FirstOrDefault(l => l.Id == 1);

                Assert.Equal("One", result.Description);

                context.Database.EnsureDeleted();
            }
        }
    }
}
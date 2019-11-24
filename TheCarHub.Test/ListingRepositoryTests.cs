using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TheCarHub.Data;
using TheCarHub.Models.Entities;
using TheCarHub.Repositories;
using Xunit;

namespace TheCarHub.Test
{
//    [Collection("DB")]
    public class ListingRepositoryTests
    {
        private DbContextOptions<ApplicationDbContext> BuildTestDbOptions(SqliteConnection connection)
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;
        }

        [Fact]
        public async void TestGetAllListings()
        {
            // Arrange
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = BuildTestDbOptions(connection);
                IList<Listing> result;

                // Act
                await using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();

                    var repository = new ListingRepository(context);

                    result = await repository.GetAllListings();

                    context.Database.EnsureDeleted();
                }

                await using (var context = new ApplicationDbContext(options))
                {
                    // Assert
                    Assert.Equal(6, result.Count);

                    context.Database.EnsureDeleted();
                }
            }
            finally
            {
                connection.Close();
            }
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
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = BuildTestDbOptions(connection);
                Listing result;

                // Act
                await using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();

                    var repository = new ListingRepository(context);

                    result = await repository.GetListingById(testId);

                    context.Database.EnsureDeleted();
                }

                await using (var context = new ApplicationDbContext(options))
                {
                    // Assert
                    Assert.Equal(expectedMake, result.Description);

                    context.Database.EnsureDeleted();
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-7)]
        [InlineData(7)]
        public async void TestGetListingByIdInvalidId(int testId)
        {
            // Arrange
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = BuildTestDbOptions(connection);
                Listing result;

                // Act
                await using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();

                    var repository = new ListingRepository(context);

                    result = await repository.GetListingById(testId);
                }

                await using (var context = new ApplicationDbContext(options))
                {
                    // Assert
                    Assert.Null(result);

                    context.Database.EnsureDeleted();
                }
            }
            finally
            {
                connection.Close();
            }
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
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = BuildTestDbOptions(connection);

                // Act
                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();

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
            finally
            {
                connection.Close();
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(7)]
        [InlineData(-1)]
        public void TestDeleteListingInvalidId(int testId)
        {
            // Arrange
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = BuildTestDbOptions(connection);
                int expected;

                // Act
                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();

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
            finally
            {
                connection.Close();
            }
        }

        // Note: uses InMemory because of SQLite limitations
        [Fact]
        public void TestSaveListingValidListingEntity()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString(), new InMemoryDatabaseRoot())
                .Options;

            var testEntity = new Listing
            {
//                    Car = car,
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
                var result = context.Listing.ToList();

                Assert.Equal("test description", result.Last().Description);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void TestSaveListingNullEntity()
        {
            // Arrange
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = BuildTestDbOptions(connection);
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
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void TestUpdateListingValidEntity()
        {
            // Arrange
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = BuildTestDbOptions(connection);

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

                    repository.EditListing(testEntity);
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
            finally
            {
                connection.Close();
            }
        }

//        [Fact]
//        public void TestUpdateListingsNullEntity()
//        {
//            // Arrange
//            var connection = new SqliteConnection("Datasource=:memory:");
//            connection.Open();
//
//            try
//            {
//                var options = BuildTestDbOptions(connection);
//
//                // Act
//                using (var context = new ApplicationDbContext(options))
//                {
//                    context.Database.EnsureCreated();
//                    
//                    var repository = new ListingRepository(context);
//                
//                    var testEntity = 
//                        context
//                            .Listing
//                            .FirstOrDefault(l => l.Id == 10);
//
//                    repository.EditListing(testEntity);
//                }
//
//                // Assert
//                using (var context = new ApplicationDbContext(options))
//                {
//                    var result = 
//                        context
//                            .Listing
//                            .FirstOrDefault(l => l.Id == 10);
//
//                    Assert.Equal("One", result.Description);
//
//                    context.Database.EnsureDeleted();
//                }
//            }
//            finally
//            {
//                connection.Close();
//            }
//        }
    }
}
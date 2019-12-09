using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using TheCarHub.Data;
using TheCarHub.Models.Entities;
using TheCarHub.Repositories;
using Xunit;

namespace TheCarHub.Test
{
    public class RepairJobRepositoryTests
    {
        private DbContextOptions<ApplicationDbContext> DbContextOptions
        {
            get
            {
                var options =
                    new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString())
                        .Options;

                return options;
            }
        }
        
        private static void SeedRepairJobData(ApplicationDbContext context)
        {
            context.RepairJob.AddRange(
                new RepairJob
                {
                    Id = 1,
                    ListingId = 1,
                    Description = "one description",
                    Cost = 10
                },
                new RepairJob
                {
                    Id = 2,
                    ListingId = 2,
                    Description = "two description",
                    Cost = 20
                },
                new RepairJob
                {
                    Id = 3,
                    ListingId = 3,
                    Description = "three description",
                    Cost = 30
                },
                new RepairJob
                {
                    Id = 4,
                    ListingId = 4,
                    Description = "four description",
                    Cost = 40
                },
                new RepairJob
                {
                    Id = 5,
                    ListingId = 5,
                    Description = "five description",
                    Cost = 50
                },
                new RepairJob
                {
                    Id = 6,
                    ListingId = 6,
                    Description = "six description",
                    Cost = 60
                });

            context.SaveChanges();
        }
        
        [Fact]
        public void TestGetCarEntityEntryValidEntity()
        {
            // Arrange
            EntityEntry<RepairJob> result;

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                context.Database.EnsureCreated();

                SeedRepairJobData(context);

                var repository = new RepairJobRepository(context);

                var testEntity = context.RepairJob.FirstOrDefault();

                // Act
                result = repository.GetRepairJobEntityEntry(testEntity);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotNull(result);
            Assert.IsType<EntityEntry<RepairJob>>(result);
        }

        [Fact]
        public void TestGetListingEntityEntryNull()
        {
            // Arrange
            EntityEntry<RepairJob> result;

            using (var context = new ApplicationDbContext(DbContextOptions))
            {
                context.Database.EnsureCreated();

                var repository = new RepairJobRepository(context);

                // Act
                result = repository.GetRepairJobEntityEntry(null);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Null(result);
        }

        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void TestUpdateRepairJobNullArg()
        {
            // Arrange
            var mockContext = new Mock<ApplicationDbContext>();
            
            mockContext
                .Setup(x => x.Update(It.IsAny<RepairJob>()))
                .Verifiable();
            
            var repository = new RepairJobRepository(mockContext.Object);

            RepairJob testEntity = null;
            
            // Act
            repository.UpdateRepairJob(testEntity);

            // Assert
            mockContext
                .Verify(x => x.Update(It.IsAny<RepairJob>()),
                    Times.Never);
        }

        [Fact]
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void TestUpdateRepairJobValidEntity()
        {
            // Arrange
            var options = DbContextOptions;
            
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                SeedRepairJobData(context);

                var repository = new RepairJobRepository(context);
                
                var entity = context
                    .RepairJob
                    .FirstOrDefault(rj => rj.Id == 1);

                entity.Description = "Altered description";
                
                // Act
                repository.UpdateRepairJob(entity);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var result = context
                    .RepairJob
                    .FirstOrDefault(rj => rj.Id == 1);
                
                Assert.NotNull(result);
                Assert.Equal("Altered description", result.Description);

                context.Database.EnsureDeleted();
            }
        }

        [Theory]
        [InlineData(1, "one description")]
        [InlineData(2, "two description")]
        [InlineData(3, "three description")]
        [InlineData(4, "four description")]
        [InlineData(5, "five description")]
        [InlineData(6, "six description")]
        public async Task TestGetRepairJobByIdAsyncValidId(int id, string expected)
        {
            // Arrange
            var options = DbContextOptions;
            RepairJob result;
            
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                SeedRepairJobData(context);

                var repository = new RepairJobRepository(context);
                
                // Act
                result = await repository.GetRepairJobByIdAsync(id);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expected, result.Description);
        }

        [Fact]
        public async Task TestGetRepairJobByIdAsyncInvalidId()
        {
            // Arrange
            var options = DbContextOptions;
            RepairJob result;

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                SeedRepairJobData(context);

                var repository = new RepairJobRepository(context);
                
                // Act
                result = await repository.GetRepairJobByIdAsync(666);

                context.Database.EnsureDeleted();
            }

            // Assert
            Assert.Null(result);
        }

    }
}
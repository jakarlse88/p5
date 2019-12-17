using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

//                SeedRepairJobData(context);

                var repository = new RepairJobRepository(context);

                var testEntity =
                    context.RepairJob.FirstOrDefault(
                        rj => rj.Id == 1);

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
    }
}
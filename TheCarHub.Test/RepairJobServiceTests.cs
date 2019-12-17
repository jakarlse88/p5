using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Data;
using TheCarHub.Models.Entities;
using TheCarHub.Models.InputModels;
using TheCarHub.Repositories;
using TheCarHub.Services;
using Xunit;

namespace TheCarHub.Test
{
    public class RepairJobServiceTests
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
        public void TestMapRepairJobValuesNullRepairJobInputModel()
        {
            // Arrange
            var service = new RepairJobService(null);
            
            // Act
            void TestAction() => service.MapRepairJobValues(null, null);

            // Assert
            var ex = Assert.Throws<Exception>(TestAction);
            Assert.Equal("InputModel cannot be null.", ex.Message);
        }

        [Fact]
        public void TestMapRepairJobValuesNullEntity()
        {
            // Arrange
            var service = new RepairJobService(null);

            var inputModel = new RepairJobInputModel();
            
            // Act
            void TestAction() => service.MapRepairJobValues(inputModel, null);

            // Assert
            var ex = Assert.Throws<Exception>(TestAction);
            Assert.Equal("RepairJob entity not found.", ex.Message);
        }

        [Fact]
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void TestMapRepairJobValidArguments()
        {
            // Arrange
            var options = DbContextOptions;
            
            var inputModel = new RepairJobInputModel
            {
                Id = 2,
                Cost = 666,
                Description = "test description"
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
//                SeedRepairJobData(context);
                
                var repository = new RepairJobRepository(context);
                
                var service = new RepairJobService(repository);

                var entity =
                    context.RepairJob.FirstOrDefault(rj => rj.Id == 2);
                
                // Act
                service.MapRepairJobValues(inputModel, entity);
                
                // Assert
                Assert.Equal(666, entity.Cost);
                Assert.Equal("test description", entity.Description);

                context.Database.EnsureDeleted();

            }
        }

    }
}
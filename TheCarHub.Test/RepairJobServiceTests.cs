using System;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task TestUpdateRepairJobExperimentalAsyncValidSource()
        {
            // Arrange
            var options = DbContextOptions;
            
            var inputModel = new RepairJobInputModel
            {
                Id = 1,
                Description = "Altered description",
                Cost = 666
            };

            await using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                
                SeedRepairJobData(context);

                var repository = new RepairJobRepository(context);
                var service = new RepairJobService(repository);

                // Act
                await service.UpdateRepairJobAsync(inputModel);
            }

            // Assert
            await using (var context = new ApplicationDbContext(options))
            {
                var entity = context.RepairJob.FirstOrDefault(c => c.Id == 1);

                Assert.NotNull(entity);
                Assert.Equal("Altered description", entity.Description);
                Assert.Equal(666, entity.Cost);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task TestUpdateRepairJobExperimentalAsyncInvalidSource()
        {
            // Arrange
            var mockRepository = new Mock<IRepairJobRepository>();

            mockRepository
                .Setup(x => x.UpdateRepairJob(It.IsAny<RepairJob>()))
                .Verifiable();

            var service = new RepairJobService(mockRepository.Object);

            var testInputModel = new RepairJobInputModel {Id = 666};

            // Act
            await service.UpdateRepairJobAsync(testInputModel);

            // Assert
            mockRepository
                .Verify(x => x.UpdateRepairJob(It.IsAny<RepairJob>()), Times.Never);
        }
    }
}
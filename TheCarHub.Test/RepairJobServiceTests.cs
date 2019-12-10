using System;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Data;
using TheCarHub.Models.Entities;

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

        // TODO: test MapRepairJobValues()
    }
}
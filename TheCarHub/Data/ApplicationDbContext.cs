using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Models;
using TheCarHub.Models.Entities;

namespace TheCarHub.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Car { get; set; }
        public DbSet<Listing> Listing { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<Status> Status { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Listing>()
                .Property(l => l.PurchasePrice)
                .HasColumnType("money");

            builder.Entity<Listing>()
                .Property(l => l.SellingPrice)
                .HasColumnType("money");

            builder.Entity<RepairJob>()
                .Property(rj => rj.Cost)
                .HasColumnType("money");

            builder.Entity<RepairJob>()
                .Property(rj => rj.Tax)
                .HasColumnType("money");
            
            builder.Entity<MediaTag>()
                .HasKey(mt => new { TagId = mt.TagId, MediaId = mt.MediaId });

            builder.Entity<ListingTag>()
                .HasKey(lt => new { ListingId = lt.ListingId, TagId = lt.TagId });

//            builder.Entity<Car>()
//                .HasMany<Listing>()
//                .WithOne()
//                .HasForeignKey(l => l.CarId);
//            
            builder.Entity<Car>()
                .HasData(
                    new Car {
                    Id = 1,
                    VIN = "",
                    Year = new DateTime(1991, 1, 1),
                    Make = "Mazda",
                    Model = "Miata",
                    Trim = "LE",
                },
                new Car {
                    Id = 2,
                    VIN = "",
                    Year = new DateTime(2007, 1, 1),
                    Make = "Jeep",
                    Model = "Liberty",
                    Trim = "Sport",
                },
                new Car {
                    Id = 3,
                    VIN = "",
                    Year = new DateTime(2017, 1, 1),
                    Make = "Ford",
                    Model = "Explorer",
                    Trim = "XLT",
                },
                new Car {
                    Id = 4,
                    VIN = "",
                    Year = new DateTime(2008, 1, 1),
                    Make = "Honda",
                    Model = "Civic",
                    Trim = "LX",
                },
                new Car {
                    Id = 5,
                    VIN = "",
                    Year = new DateTime(2016, 1, 1),
                    Make = "Volkswagen",
                    Model = "GTI",
                    Trim = "S",
                },
                new Car {
                    Id = 6,
                    VIN = "",
                    Year = new DateTime(2013, 1, 1),
                    Make = "Ford",
                    Model = "Edge",
                    Trim = "SEL",
                });

            builder.Entity<Listing>()
                .HasData(
                    new Listing {
                        Id = 1,
                        CarId = 1,
                        StatusId = 2
                    },
                    new Listing {
                        Id = 2,
                        CarId = 2,
                        StatusId = 2
                    },
                    new Listing {
                        Id = 3,
                        CarId = 3,
                        StatusId = 1
                    },
                    new Listing {
                        Id = 4,
                        CarId = 4,
                        StatusId = 1
                    },
                    new Listing {
                        Id = 5,
                        CarId = 5,
                        StatusId = 2
                    },
                    new Listing {
                        Id = 6,
                        CarId = 6,
                        StatusId = 2
                    }
                );

            builder.Entity<Status>()
                .HasData(
                    new Status
                    {
                        Id = 1,
                        Name = "Available"
                    },
                    new Status
                    {
                        Id = 2,
                        Name = "Sold"
                    }
                );
        }
    }
}

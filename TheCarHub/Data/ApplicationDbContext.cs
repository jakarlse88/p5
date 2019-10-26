using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Models;

namespace TheCarHub.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CarEntity> Cars { get; set; }
        public DbSet<ListingEntity> Listings { get; set; }
        public DbSet<ImageEntity> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CarEntity>()
                .HasKey(x => x.CarId);

            builder.Entity<ListingEntity>()
                .HasKey(x => x.ListingId);

            builder.Entity<ImageEntity>()
                .HasKey(x => x.ImageEntityId);

            builder.Entity<ImageEntity>()
                .HasOne(x => x.Listing)
                .WithMany(y => y.Images)
                .HasForeignKey(z => z.ListingEntityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ListingEntity>()
                .HasOne(x => x.Car)
                .WithOne(y => y.Listing)
                .HasForeignKey<CarEntity>(z => z.ListingEntityId);

            builder.Entity<ListingEntity>()
                .HasData(
                     new ListingEntity {
                        ListingId = 1,
                        Description = "",
                        ListingStatus = "Available",
                        DateCreated = DateTime.Now,
                        DateLastUpdated = null
                    },
                     new ListingEntity {
                        ListingId = 2,
                        Description = "",
                        ListingStatus = "Available",
                        DateCreated = DateTime.Now,
                        DateLastUpdated = null
                    },
                     new ListingEntity {
                        ListingId = 3,
                        Description = "",
                        ListingStatus = "Available",
                        DateCreated = DateTime.Now,
                        DateLastUpdated = null
                    },
                     new ListingEntity {
                        ListingId = 4,
                        Description = "",
                        ListingStatus = "Available",
                        DateCreated = DateTime.Now,
                        DateLastUpdated = null
                    },
                     new ListingEntity {
                        ListingId = 5,
                        Description = "",
                        ListingStatus = "Available",
                        DateCreated = DateTime.Now,
                        DateLastUpdated = null
                    },
                     new ListingEntity {
                        ListingId = 6,
                        Description = "",
                        ListingStatus = "Available",
                        DateCreated = DateTime.Now,
                        DateLastUpdated = null
                    });

                builder.Entity<CarEntity>()
                    .HasData(
                        new CarEntity {
                        CarId = 1,
                        ListingEntityId = 1,
                        VIN = "",
                        Year = new DateTime(1991),
                        Make = "Mazda",
                        Model = "Miata",
                        Trim = "LE",
                        PurchaseDate = new DateTime(2019, 1, 7),
                        PurchasePrice = 1800,
                        Repairs = "Full restoration",
                        RepairCost = 7600,
                        LotDate = new DateTime(2019, 4, 7),
                        SellingPrice = 1800 + 7600 + 500,
                        SaleDate = new DateTime(2019, 4, 8)
                    },
                    new CarEntity {
                        CarId = 2,
                        ListingEntityId = 2,
                        VIN = "",
                        Year = new DateTime(2007),
                        Make = "Jeep",
                        Model = "Liberty",
                        Trim = "Sport",
                        PurchaseDate = new DateTime(2019, 4, 2),
                        PurchasePrice = 4500m,
                        Repairs = "Front wheel bearings",
                        RepairCost = 350m,
                        LotDate = new DateTime(2019, 4, 7),
                        SellingPrice = 4500 + 350 + 500,
                        SaleDate = null
                    },
                    new CarEntity {
                        CarId = 3,
                        ListingEntityId = 3,
                        VIN = "",
                        Year = new DateTime(2017),
                        Make = "Ford",
                        Model = "Explorer",
                        Trim = "XLT",
                        PurchaseDate = new DateTime(2019, 4, 5),
                        PurchasePrice = 24350,
                        Repairs = "Tyres, brakes",
                        RepairCost = 1100,
                        LotDate = new DateTime(2019, 4, 9),
                        SellingPrice = 24350 + 1100 + 500, 
                        SaleDate = null
                    },
                    new CarEntity {
                        CarId = 4,
                        ListingEntityId = 4,
                        VIN = "",
                        Year = new DateTime(2008),
                        Make = "Honda",
                        Model = "Civic",
                        Trim = "LX",
                        PurchaseDate = new DateTime(2019, 4, 6),
                        PurchasePrice = 4000,
                        Repairs = "Ac, brakes",
                        RepairCost = 475,
                        LotDate = new DateTime(2019, 4, 9),
                        SellingPrice = 4000 + 475 + 500,
                        SaleDate = new DateTime(2019, 4, 9)
                    },
                    new CarEntity {
                        CarId = 5,
                        ListingEntityId = 5,
                        VIN = "",
                        Year = new DateTime(2016),
                        Make = "Volkswagen",
                        Model = "GTI",
                        Trim = "S",
                        PurchaseDate = new DateTime(2019, 4, 6),
                        PurchasePrice = 15250,
                        Repairs = "Tyres",
                        RepairCost = 440,
                        LotDate = new DateTime(2019, 4, 10),
                        SellingPrice = 15250 + 440 + 500,
                        SaleDate = new DateTime(2019, 4, 12)
                    },
                    new CarEntity {
                        CarId = 6,
                        ListingEntityId = 6,
                        VIN = "",
                        Year = new DateTime(2013),
                        Make = "Ford",
                        Model = "Edge",
                        Trim = "SEL",
                        PurchaseDate = new DateTime(2019, 4, 7),
                        PurchasePrice = 10990,
                        Repairs = "Tyres, brakes, AC",
                        RepairCost = 950,
                        LotDate = new DateTime(2019, 4, 11),
                        SellingPrice = 10990 + 950 + 500,
                        SaleDate = new DateTime(2019, 4, 12)
                    });
        }
    }
}

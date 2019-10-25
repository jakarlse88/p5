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

        public DbSet<CarItem> Cars { get; set; }
        public DbSet<ListingItem> Listings { get; set; }
        public DbSet<ImageItem> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CarItem>()
                .HasKey(x => x.CarId);

            builder.Entity<ListingItem>()
                .HasKey(x => x.ListingItemId);

            builder.Entity<ImageItem>()
                .HasKey(x => x.ImageItemId);

            builder.Entity<ImageItem>()
                .HasOne(x => x.Listing)
                .WithMany(y => y.Images)
                .HasForeignKey(z => z.ListingItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ListingItem>()
                .HasOne(x => x.Car)
                .WithOne(y => y.Listing)
                .HasForeignKey<CarItem>(z => z.ListingItemId);

            builder.Entity<ListingItem>()
                .HasData(
                     new ListingItem {
                        ListingItemId = 1,
                        Description = "",
                        ListingStatus = "Available",
                        DateCreated = DateTime.Now,
                        DateLastUpdated = null
                    },
                     new ListingItem {
                        ListingItemId = 2,
                        Description = "",
                        ListingStatus = "Available",
                        DateCreated = DateTime.Now,
                        DateLastUpdated = null
                    },
                     new ListingItem {
                        ListingItemId = 3,
                        Description = "",
                        ListingStatus = "Available",
                        DateCreated = DateTime.Now,
                        DateLastUpdated = null
                    },
                     new ListingItem {
                        ListingItemId = 4,
                        Description = "",
                        ListingStatus = "Available",
                        DateCreated = DateTime.Now,
                        DateLastUpdated = null
                    },
                     new ListingItem {
                        ListingItemId = 5,
                        Description = "",
                        ListingStatus = "Available",
                        DateCreated = DateTime.Now,
                        DateLastUpdated = null
                    },
                     new ListingItem {
                        ListingItemId = 6,
                        Description = "",
                        ListingStatus = "Available",
                        DateCreated = DateTime.Now,
                        DateLastUpdated = null
                    });

                builder.Entity<CarItem>()
                    .HasData(
                        new CarItem {
                        CarId = 1,
                        ListingItemId = 1,
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
                    new CarItem {
                        CarId = 2,
                        ListingItemId = 2,
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
                    new CarItem {
                        CarId = 3,
                        ListingItemId = 3,
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
                    new CarItem {
                        CarId = 4,
                        ListingItemId = 4,
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
                    new CarItem {
                        CarId = 5,
                        ListingItemId = 5,
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
                    new CarItem {
                        CarId = 6,
                        ListingItemId = 6,
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

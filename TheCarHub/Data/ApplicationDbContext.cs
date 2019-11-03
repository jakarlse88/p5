using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
                .Property(rj => rj.HourlyRate)
                .HasColumnType("money");

            builder.Entity<SparePart>()
                .Property(sp => sp.Cost)
                .HasColumnType("money");

            builder.Entity<MediaTag>()
                .HasKey(mt => new { mt.TagId, mt.MediaId });

            builder.Entity<ListingTag>()
                .HasKey(lt => new { lt.ListingId, lt.TagId });

            builder.Entity<MediaTag>()
                .HasOne(mt => mt.Tag)
                .WithMany(m => m.MediaTags)
                .HasForeignKey(mt => mt.TagId);

            builder.Entity<MediaTag>()
                .HasOne(mt => mt.Media)
                .WithMany(m => m.MediaTags)
                .HasForeignKey(mt => mt.MediaId);

            builder.Entity<ListingTag>()
                .HasOne(lt => lt.Listing)
                .WithMany(l => l.ListingTags)
                .HasForeignKey(lt => lt.ListingId);

            builder.Entity<ListingTag>()
                .HasOne(lt => lt.Tag)
                .WithMany(l => l.ListingTags)
                .HasForeignKey(lt => lt.TagId);

            builder.Entity<Media>()
                .HasOne(m => m.Listing)
                .WithMany(l => l.Media)
                .HasForeignKey(m => m.ListingId);

            builder.Entity<Listing>()
                .HasOne(l => l.RepairJob)
                .WithOne(rj => rj.Listing)
                .HasForeignKey<RepairJob>(rj => rj.ListingId);

            builder.Entity<Car>()
                .HasMany(c => c.Listings)
                .WithOne(l => l.Car)
                .HasForeignKey(c => c.CarId);

            builder.Entity<Car>()
                .HasData(
                    new Car {
                    Id = 1,
                    VIN = "",
                    Year = new DateTime(1991, 1, 1),
                    Make = "Mazda",
                    Model = "Miata",
                    Trim = "LE",
                    Listings = new List<Listing>()
                },
                new Car {
                    Id = 2,
                    VIN = "",
                    Year = new DateTime(2007, 1, 1),
                    Make = "Jeep",
                    Model = "Liberty",
                    Trim = "Sport",
                    Listings = new List<Listing>()
                },
                new Car {
                    Id = 3,
                    VIN = "",
                    Year = new DateTime(2017, 1, 1),
                    Make = "Ford",
                    Model = "Explorer",
                    Trim = "XLT",
                    Listings = new List<Listing>()
                },
                new Car {
                    Id = 4,
                    VIN = "",
                    Year = new DateTime(2008, 1, 1),
                    Make = "Honda",
                    Model = "Civic",
                    Trim = "LX",
                    Listings = new List<Listing>()
                },
                new Car {
                    Id = 5,
                    VIN = "",
                    Year = new DateTime(2016, 1, 1),
                    Make = "Volkswagen",
                    Model = "GTI",
                    Trim = "S",
                    Listings = new List<Listing>()
                },
                new Car {
                    Id = 6,
                    VIN = "",
                    Year = new DateTime(2013, 1, 1),
                    Make = "Ford",
                    Model = "Edge",
                    Trim = "SEL",
                    Listings = new List<Listing>()
                });

            builder.Entity<Listing>()
                .HasData(
                    new Listing {
                        Id = 1,
                        CarId = 1,
                        Media = new List<Media>()
                    },
                    new Listing {
                        Id = 2,
                        CarId = 2,
                        Media = new List<Media>()
                    },
                    new Listing {
                        Id = 3,
                        CarId = 3,
                        Media = new List<Media>()
                    },
                    new Listing {
                        Id = 4,
                        CarId = 4,
                        Media = new List<Media>()
                    },
                    new Listing {
                        Id = 5,
                        CarId = 5,
                        Media = new List<Media>()
                    },
                    new Listing {
                        Id = 6,
                        CarId = 6,
                        Media = new List<Media>()
                    }
                );

        }
    }
}

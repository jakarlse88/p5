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

        public DbSet<Car> Cars { get; set; }
        public DbSet<Listing> Listings { get; set; }
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
                .HasForeignKey(c => c.CarForeignKey);

                builder.Entity<Car>()
                    .HasData(
                        new Car {
                        Id = 1,
                        VIN = "",
                        Year = new DateTime(1991),
                        Make = "Mazda",
                        Model = "Miata",
                        Trim = "LE",
                    },
                    new Car {
                        Id = 2,
                        VIN = "",
                        Year = new DateTime(2007),
                        Make = "Jeep",
                        Model = "Liberty",
                        Trim = "Sport",
                    },
                    new Car {
                        Id = 3,
                        VIN = "",
                        Year = new DateTime(2017),
                        Make = "Ford",
                        Model = "Explorer",
                        Trim = "XLT",
                    },
                    new Car {
                        Id = 4,
                        VIN = "",
                        Year = new DateTime(2008),
                        Make = "Honda",
                        Model = "Civic",
                        Trim = "LX",
                    },
                    new Car {
                        Id = 5,
                        VIN = "",
                        Year = new DateTime(2016),
                        Make = "Volkswagen",
                        Model = "GTI",
                        Trim = "S",
                    },
                    new Car {
                        Id = 6,
                        VIN = "",
                        Year = new DateTime(2013),
                        Make = "Ford",
                        Model = "Edge",
                        Trim = "SEL",
                    });
        }
    }
}

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
                .HasKey(mt => new { TagId = mt.TagForeignKey, MediaId = mt.MediaForeignKey });

            builder.Entity<ListingTag>()
                .HasKey(lt => new { ListingId = lt.ListingForeignKey, TagId = lt.TagForeignKey });

            builder.Entity<MediaTag>()
                .HasOne<Tag>()
                .WithMany()
                .HasForeignKey(mt => mt.TagForeignKey);

            builder.Entity<MediaTag>()
                .HasOne<Media>()
                .WithMany()
                .HasForeignKey(mt => mt.MediaForeignKey);

            builder.Entity<ListingTag>()
                .HasOne<Listing>()
                .WithMany()
                .HasForeignKey(lt => lt.ListingForeignKey);

            builder.Entity<ListingTag>()
                .HasOne<Tag>()
                .WithMany()
                .HasForeignKey(lt => lt.TagForeignKey);

            builder.Entity<Listing>()
                .HasOne<RepairJob>()
                .WithOne()
                .HasForeignKey<RepairJob>(rj => rj.ListingForeignKey);

            builder.Entity<Car>()
                .HasMany<Listing>()
                .WithOne()
                .HasForeignKey(c => c.CarForeignKey);

            builder.Entity<Listing>()
                .HasMany<Media>()
                .WithOne()
                .HasForeignKey(m => m.ListingForeignKey);

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
                        CarForeignKey = 1
                    },
                    new Listing {
                        Id = 2,
                        CarForeignKey = 2
                    },
                    new Listing {
                        Id = 3,
                        CarForeignKey = 3
                    },
                    new Listing {
                        Id = 4,
                        CarForeignKey = 4
                    },
                    new Listing {
                        Id = 5,
                        CarForeignKey = 5
                    },
                    new Listing {
                        Id = 6,
                        CarForeignKey = 6
                    }
                );

        }
    }
}

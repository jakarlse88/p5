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
                .HasKey(mt => new { TagId = mt.TagId, MediaId = mt.MediaId });

            builder.Entity<ListingTag>()
                .HasKey(lt => new { ListingId = lt.ListingId, TagId = lt.TagId });
//
//            builder.Entity<MediaTag>()
//                .HasOne<Tag>()
//                .WithMany()
//                .HasForeignKey(mt => mt.TagId);
//
//            builder.Entity<MediaTag>()
//                .HasOne<Media>()
//                .WithMany()
//                .HasForeignKey(mt => mt.MediaId);
//
//            builder.Entity<ListingTag>()
//                .HasOne<Listing>()
//                .WithMany()
//                .HasForeignKey(lt => lt.ListingId);
//
//            builder.Entity<ListingTag>()
//                .HasOne<Tag>()
//                .WithMany()
//                .HasForeignKey(lt => lt.TagId);
//
//            builder.Entity<Listing>()
//                .HasOne<RepairJob>()
//                .WithOne()
//                .HasForeignKey<RepairJob>(rj => rj.ListingId);
//
//            builder.Entity<Car>()
//                .HasMany<Listing>()
//                .WithOne()
//                .HasForeignKey(c => c.CarId);
//
//            builder.Entity<Listing>()
//                .HasMany<Media>()
//                .WithOne()
//                .HasForeignKey(m => m.ListingId);

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
                        CarId = 1
                    },
                    new Listing {
                        Id = 2,
                        CarId = 2
                    },
                    new Listing {
                        Id = 3,
                        CarId = 3
                    },
                    new Listing {
                        Id = 4,
                        CarId = 4
                    },
                    new Listing {
                        Id = 5,
                        CarId = 5
                    },
                    new Listing {
                        Id = 6,
                        CarId = 6
                    }
                );

        }
    }
}

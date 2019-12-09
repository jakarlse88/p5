using Microsoft.EntityFrameworkCore;
using TheCarHub.Models;
using TheCarHub.Models.Entities;

namespace TheCarHub.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

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
                .HasKey(mt => new {TagId = mt.TagId, MediaId = mt.MediaId});

            builder.Entity<ListingTag>()
                .HasKey(lt => new {ListingId = lt.ListingId, TagId = lt.TagId});

            builder.Entity<Car>()
                .HasData(
                    new Car
                    {
                        Id = 1,
                        VIN = "",
                        Year = 1991,
                        Make = "Mazda",
                        Model = "Miata",
                        Trim = "LE",
                    },
                    new Car
                    {
                        Id = 2,
                        VIN = "",
                        Year = 2007,
                        Make = "Jeep",
                        Model = "Liberty",
                        Trim = "Sport",
                    },
                    new Car
                    {
                        Id = 3,
                        VIN = "",
                        Year = 2017,
                        Make = "Ford",
                        Model = "Explorer",
                        Trim = "XLT",
                    },
                    new Car
                    {
                        Id = 4,
                        VIN = "",
                        Year = 2008,
                        Make = "Honda",
                        Model = "Civic",
                        Trim = "LX",
                    },
                    new Car
                    {
                        Id = 5,
                        VIN = "",
                        Year = 2016,
                        Make = "Volkswagen",
                        Model = "GTI",
                        Trim = "S",
                    },
                    new Car
                    {
                        Id = 6,
                        VIN = "",
                        Year = 2013,
                        Make = "Ford",
                        Model = "Edge",
                        Trim = "SEL",
                    });

            builder.Entity<Listing>()
                .HasData(
                    new Listing
                    {
                        Id = 1,
                        CarId = 1,
                        StatusId = 2,
                        Description = "one description"
                    },
                    new Listing
                    {
                        Id = 2,
                        CarId = 2,
                        StatusId = 2,
                        Description = "two description"
                    },
                    new Listing
                    {
                        Id = 3,
                        CarId = 3,
                        StatusId = 1,
                        Description = "three description"
                    },
                    new Listing
                    {
                        Id = 4,
                        CarId = 4,
                        StatusId = 1,
                        Description = "four description"
                    },
                    new Listing
                    {
                        Id = 5,
                        CarId = 5,
                        StatusId = 2,
                        Description = "five description"
                    },
                    new Listing
                    {
                        Id = 6,
                        CarId = 6,
                        StatusId = 2,
                        Description = "six description"
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

            builder.Entity<Media>()
                .HasData(new Media
                    {
                        ListingId = 1,
                        Id = 1,
                        FileName = "file one"
                    },
                    new Media
                    {
                        ListingId = 2,
                        Id = 2,
                        FileName = "file two"
                    },
                    new Media
                    {
                        ListingId = 3,
                        Id = 3,
                        FileName = "file three"
                    },
                    new Media
                    {
                        ListingId = 4,
                        Id = 4,
                        FileName = "file four"
                    },
                    new Media
                    {
                        ListingId = 5,
                        Id = 5,
                        FileName = "file five"
                    },
                    new Media
                    {
                        ListingId = 6,
                        Id = 6,
                        FileName = "file six"
                    }
                );
        }
    }
}
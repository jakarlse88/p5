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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CarItem>()
                .HasKey(i => i.CarId);

            builder.Entity<ListingItem>()
                .HasKey(i => i.ListingId);
            
            builder.Entity<CarItem>()
                .HasOne(p => p.Listing)
                .WithOne(i => i.Car)
                .HasForeignKey<CarItem>(b => b.CarId);

            builder.Entity<ListingItem>()
                .HasMany(b => b.Images)
                .WithOne();
        }
    }
}

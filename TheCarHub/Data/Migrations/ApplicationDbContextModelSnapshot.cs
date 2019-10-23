﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheCarHub.Data;

namespace TheCarHub.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0");

            modelBuilder.Entity("TheCarHub.Models.CarItem", b =>
                {
                    b.Property<Guid>("CarId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LotDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Make")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PurchasePrice")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("RepairCost")
                        .HasColumnType("TEXT");

                    b.Property<string>("Repairs")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("SaleDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Trim")
                        .HasColumnType("TEXT");

                    b.Property<string>("VIN")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Year")
                        .HasColumnType("TEXT");

                    b.HasKey("CarId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("TheCarHub.Models.ImageItem", b =>
                {
                    b.Property<Guid>("ImageItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageLocation")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ListingItemListingId")
                        .HasColumnType("TEXT");

                    b.HasKey("ImageItemId");

                    b.HasIndex("ListingItemListingId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("TheCarHub.Models.ListingItem", b =>
                {
                    b.Property<Guid>("ListingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CarId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateLastUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("ListingStatus")
                        .HasColumnType("TEXT");

                    b.HasKey("ListingId");

                    b.ToTable("Listings");
                });

            modelBuilder.Entity("TheCarHub.Models.CarItem", b =>
                {
                    b.HasOne("TheCarHub.Models.ListingItem", "Listing")
                        .WithOne("Car")
                        .HasForeignKey("TheCarHub.Models.CarItem", "CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TheCarHub.Models.ImageItem", b =>
                {
                    b.HasOne("TheCarHub.Models.ListingItem", null)
                        .WithMany("Images")
                        .HasForeignKey("ListingItemListingId");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheCarHub.Data;

namespace TheCarHub.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TheCarHub.Models.CarItem", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ListingItemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LotDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Make")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PurchasePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("RepairCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Repairs")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SaleDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("SellingPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Trim")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VIN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Year")
                        .HasColumnType("datetime2");

                    b.HasKey("CarId");

                    b.HasIndex("ListingItemId")
                        .IsUnique();

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            CarId = 1,
                            ListingItemId = 1,
                            LotDate = new DateTime(2019, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Make = "Mazda",
                            Model = "Miata",
                            PurchaseDate = new DateTime(2019, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 1800m,
                            RepairCost = 7600m,
                            Repairs = "Full restoration",
                            SaleDate = new DateTime(2019, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SellingPrice = 9900m,
                            Trim = "LE",
                            VIN = "",
                            Year = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1991)
                        },
                        new
                        {
                            CarId = 2,
                            ListingItemId = 2,
                            LotDate = new DateTime(2019, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Make = "Jeep",
                            Model = "Liberty",
                            PurchaseDate = new DateTime(2019, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 4500m,
                            RepairCost = 350m,
                            Repairs = "Front wheel bearings",
                            SellingPrice = 5350m,
                            Trim = "Sport",
                            VIN = "",
                            Year = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2007)
                        },
                        new
                        {
                            CarId = 3,
                            ListingItemId = 3,
                            LotDate = new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Make = "Ford",
                            Model = "Explorer",
                            PurchaseDate = new DateTime(2019, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 24350m,
                            RepairCost = 1100m,
                            Repairs = "Tyres, brakes",
                            SellingPrice = 25950m,
                            Trim = "XLT",
                            VIN = "",
                            Year = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2017)
                        },
                        new
                        {
                            CarId = 4,
                            ListingItemId = 4,
                            LotDate = new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Make = "Honda",
                            Model = "Civic",
                            PurchaseDate = new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 4000m,
                            RepairCost = 475m,
                            Repairs = "Ac, brakes",
                            SaleDate = new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SellingPrice = 4975m,
                            Trim = "LX",
                            VIN = "",
                            Year = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2008)
                        },
                        new
                        {
                            CarId = 5,
                            ListingItemId = 5,
                            LotDate = new DateTime(2019, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Make = "Volkswagen",
                            Model = "GTI",
                            PurchaseDate = new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 15250m,
                            RepairCost = 440m,
                            Repairs = "Tyres",
                            SaleDate = new DateTime(2019, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SellingPrice = 16190m,
                            Trim = "S",
                            VIN = "",
                            Year = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2016)
                        },
                        new
                        {
                            CarId = 6,
                            ListingItemId = 6,
                            LotDate = new DateTime(2019, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Make = "Ford",
                            Model = "Edge",
                            PurchaseDate = new DateTime(2019, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 10990m,
                            RepairCost = 950m,
                            Repairs = "Tyres, brakes, AC",
                            SaleDate = new DateTime(2019, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SellingPrice = 12440m,
                            Trim = "SEL",
                            VIN = "",
                            Year = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2013)
                        });
                });

            modelBuilder.Entity("TheCarHub.Models.ImageItem", b =>
                {
                    b.Property<int>("ImageItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ListingItemId")
                        .HasColumnType("int");

                    b.HasKey("ImageItemId");

                    b.HasIndex("ListingItemId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("TheCarHub.Models.ListingItem", b =>
                {
                    b.Property<int>("ListingItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateLastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ListingStatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ListingItemId");

                    b.ToTable("Listings");

                    b.HasData(
                        new
                        {
                            ListingItemId = 1,
                            DateCreated = new DateTime(2019, 10, 25, 14, 49, 6, 272, DateTimeKind.Local).AddTicks(4370),
                            Description = "",
                            ListingStatus = "Available"
                        },
                        new
                        {
                            ListingItemId = 2,
                            DateCreated = new DateTime(2019, 10, 25, 14, 49, 6, 279, DateTimeKind.Local).AddTicks(2010),
                            Description = "",
                            ListingStatus = "Available"
                        },
                        new
                        {
                            ListingItemId = 3,
                            DateCreated = new DateTime(2019, 10, 25, 14, 49, 6, 279, DateTimeKind.Local).AddTicks(2070),
                            Description = "",
                            ListingStatus = "Available"
                        },
                        new
                        {
                            ListingItemId = 4,
                            DateCreated = new DateTime(2019, 10, 25, 14, 49, 6, 279, DateTimeKind.Local).AddTicks(2080),
                            Description = "",
                            ListingStatus = "Available"
                        },
                        new
                        {
                            ListingItemId = 5,
                            DateCreated = new DateTime(2019, 10, 25, 14, 49, 6, 279, DateTimeKind.Local).AddTicks(2080),
                            Description = "",
                            ListingStatus = "Available"
                        },
                        new
                        {
                            ListingItemId = 6,
                            DateCreated = new DateTime(2019, 10, 25, 14, 49, 6, 279, DateTimeKind.Local).AddTicks(2090),
                            Description = "",
                            ListingStatus = "Available"
                        });
                });

            modelBuilder.Entity("TheCarHub.Models.CarItem", b =>
                {
                    b.HasOne("TheCarHub.Models.ListingItem", "Listing")
                        .WithOne("Car")
                        .HasForeignKey("TheCarHub.Models.CarItem", "ListingItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TheCarHub.Models.ImageItem", b =>
                {
                    b.HasOne("TheCarHub.Models.ListingItem", "Listing")
                        .WithMany("Images")
                        .HasForeignKey("ListingItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

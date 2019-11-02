﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheCarHub.Data;

namespace TheCarHub.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20191101211809_UpdateSeedData")]
    partial class UpdateSeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TheCarHub.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Make")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Trim")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VIN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Year")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Car");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Make = "Mazda",
                            Model = "Miata",
                            Trim = "LE",
                            VIN = "",
                            Year = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1991)
                        },
                        new
                        {
                            Id = 2,
                            Make = "Jeep",
                            Model = "Liberty",
                            Trim = "Sport",
                            VIN = "",
                            Year = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2007)
                        },
                        new
                        {
                            Id = 3,
                            Make = "Ford",
                            Model = "Explorer",
                            Trim = "XLT",
                            VIN = "",
                            Year = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2017)
                        },
                        new
                        {
                            Id = 4,
                            Make = "Honda",
                            Model = "Civic",
                            Trim = "LX",
                            VIN = "",
                            Year = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2008)
                        },
                        new
                        {
                            Id = 5,
                            Make = "Volkswagen",
                            Model = "GTI",
                            Trim = "S",
                            VIN = "",
                            Year = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2016)
                        },
                        new
                        {
                            Id = 6,
                            Make = "Ford",
                            Model = "Edge",
                            Trim = "SEL",
                            VIN = "",
                            Year = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2013)
                        });
                });

            modelBuilder.Entity("TheCarHub.Models.Listing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateLastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PurchasePrice")
                        .HasColumnType("money");

                    b.Property<DateTime?>("SaleDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("SellingPrice")
                        .HasColumnType("money");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("Listing");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CarId = 1,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchaseDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 0m,
                            SellingPrice = 0m
                        },
                        new
                        {
                            Id = 2,
                            CarId = 2,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchaseDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 0m,
                            SellingPrice = 0m
                        },
                        new
                        {
                            Id = 3,
                            CarId = 3,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchaseDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 0m,
                            SellingPrice = 0m
                        },
                        new
                        {
                            Id = 4,
                            CarId = 4,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchaseDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 0m,
                            SellingPrice = 0m
                        },
                        new
                        {
                            Id = 5,
                            CarId = 5,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchaseDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 0m,
                            SellingPrice = 0m
                        },
                        new
                        {
                            Id = 6,
                            CarId = 6,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchaseDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchasePrice = 0m,
                            SellingPrice = 0m
                        });
                });

            modelBuilder.Entity("TheCarHub.Models.ListingTag", b =>
                {
                    b.Property<int>("ListingId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("ListingId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("ListingTag");
                });

            modelBuilder.Entity("TheCarHub.Models.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Caption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ListingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ListingId");

                    b.ToTable("Media");
                });

            modelBuilder.Entity("TheCarHub.Models.MediaTag", b =>
                {
                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<int>("MediaId")
                        .HasColumnType("int");

                    b.HasKey("TagId", "MediaId");

                    b.HasIndex("MediaId");

                    b.ToTable("MediaTag");
                });

            modelBuilder.Entity("TheCarHub.Models.RepairJob", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cost")
                        .HasColumnType("money");

                    b.Property<decimal>("HourlyRate")
                        .HasColumnType("money");

                    b.Property<DateTime>("Hours")
                        .HasColumnType("datetime2");

                    b.Property<int>("ListingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ListingId")
                        .IsUnique();

                    b.ToTable("RepairJob");
                });

            modelBuilder.Entity("TheCarHub.Models.SparePart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cost")
                        .HasColumnType("money");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RepairJobId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RepairJobId");

                    b.ToTable("SparePart");
                });

            modelBuilder.Entity("TheCarHub.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TagName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("TheCarHub.Models.Listing", b =>
                {
                    b.HasOne("TheCarHub.Models.Car", "Car")
                        .WithMany("Listings")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TheCarHub.Models.ListingTag", b =>
                {
                    b.HasOne("TheCarHub.Models.Listing", "Listing")
                        .WithMany("ListingTags")
                        .HasForeignKey("ListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheCarHub.Models.Tag", "Tag")
                        .WithMany("ListingTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TheCarHub.Models.Media", b =>
                {
                    b.HasOne("TheCarHub.Models.Listing", "Listing")
                        .WithMany("Media")
                        .HasForeignKey("ListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TheCarHub.Models.MediaTag", b =>
                {
                    b.HasOne("TheCarHub.Models.Media", "Media")
                        .WithMany("MediaTags")
                        .HasForeignKey("MediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheCarHub.Models.Tag", "Tag")
                        .WithMany("MediaTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TheCarHub.Models.RepairJob", b =>
                {
                    b.HasOne("TheCarHub.Models.Listing", "Listing")
                        .WithOne("RepairJob")
                        .HasForeignKey("TheCarHub.Models.RepairJob", "ListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TheCarHub.Models.SparePart", b =>
                {
                    b.HasOne("TheCarHub.Models.RepairJob", null)
                        .WithMany("Parts")
                        .HasForeignKey("RepairJobId");
                });
#pragma warning restore 612, 618
        }
    }
}

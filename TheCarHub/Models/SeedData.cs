using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TheCarHub.Data;
using System.Linq;

namespace TheCarHub.Models
{
    public static class SeedData
    {
        // public static void InitialiseListings(IServiceProvider serviceProvider)
        // {
        //     using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        //     {
        //         if (context.Listings.Any())
        //         {
        //             return; 
        //         }

        //         context.Listings.AddRange(
        //              new ListingItem {
        //                 ListingItemId = 1,
        //                 Description = "",
        //                 ListingStatus = "Available",
        //                 DateCreated = DateTime.Now,
        //                 DateLastUpdated = null
        //             },
        //              new ListingItem {
        //                 ListingItemId = 2,
        //                 Description = "",
        //                 ListingStatus = "Available",
        //                 DateCreated = DateTime.Now,
        //                 DateLastUpdated = null
        //             },
        //              new ListingItem {
        //                 ListingItemId = 3,
        //                 Description = "",
        //                 ListingStatus = "Available",
        //                 DateCreated = DateTime.Now,
        //                 DateLastUpdated = null
        //             },
        //              new ListingItem {
        //                 ListingItemId = 4,
        //                 Description = "",
        //                 ListingStatus = "Available",
        //                 DateCreated = DateTime.Now,
        //                 DateLastUpdated = null
        //             },
        //              new ListingItem {
        //                 ListingItemId = 5,
        //                 Description = "",
        //                 ListingStatus = "Available",
        //                 DateCreated = DateTime.Now,
        //                 DateLastUpdated = null
        //             },
        //              new ListingItem {
        //                 ListingItemId = 6,
        //                 Description = "",
        //                 ListingStatus = "Available",
        //                 DateCreated = DateTime.Now,
        //                 DateLastUpdated = null
        //             },
        //              new ListingItem {
        //                 ListingItemId = 7,
        //                 Description = "",
        //                 ListingStatus = "Available",
        //                 DateCreated = DateTime.Now,
        //                 DateLastUpdated = null
        //             }
        //         ); 

        //         context.SaveChanges();
        //     }
        // }

        public static void Initialise(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Cars.Any())
                {
                    return;
                }

                context.Cars.AddRange(
                    new CarItem {
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
                        ListingItemId = 4,
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
                        ListingItemId = 5,
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
                        ListingItemId = 6,
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
                        ListingItemId = 7,
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
                    }
                );
                context.SaveChanges();
            }
        }
    }
}

                   
                

                    
            
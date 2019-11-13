using System;
using System.Collections.Generic;

namespace TheCarHub.Models.Entities
{
    public class Listing 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public List<Media> Media { get; set; }
        public List<ListingTag> Tags { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateLastUpdated { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public DateTime? SaleDate { get; set; }
    }
}
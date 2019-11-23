using System;
using System.Collections.Generic;

namespace TheCarHub.Models.Entities
{
    public class Listing
    {
        public Listing()
        {
            Media = new HashSet<Media>();
            Tags = new HashSet<ListingTag>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public RepairJob RepairJob { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateLastUpdated { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public DateTime? SaleDate { get; set; }

        public ICollection<Media> Media { get; set; }
        public ICollection<ListingTag> Tags { get; set; }
    }
}
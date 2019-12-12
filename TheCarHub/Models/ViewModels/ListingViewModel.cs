using System;
using System.Collections.Generic;
using TheCarHub.Models.Entities;

namespace TheCarHub.Models.ViewModels
{
    public class ListingViewModel
    {
        public ListingViewModel()
        {
            Media = new HashSet<Media>();
            Tags = new List<ListingTag>();
        }
        
        public int Id { get; set; }
        public string Title { get; set; }
        public int CarId { get; set; }
        public CarViewModel Car { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateLastUpdated { get; set; }
        public DateTime PurchaseDate { get; set; }
        public RepairJobViewModel RepairJob { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime? SaleDate { get; set; }
        
        public ICollection<Media> Media { get; set; }
        public ICollection<ListingTag> Tags { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using TheCarHub.Models.Entities;

namespace TheCarHub.Models.ViewModels
{
    public class ListingViewModel
    {
        public ListingViewModel()
        {
            Car = new Car();
            RepairJob = new RepairJob();
            Media = new HashSet<Media>();
        }
        
        public int Id { get; set; }
        public string Title { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateLastUpdated { get; set; }
        public DateTime PurchaseDate { get; set; }
        public RepairJob RepairJob { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime? SaleDate { get; set; }
        
        public ICollection<Media> Media { get; set; }
        public ICollection<ListingTag> ListingTags { get; set; }
    }
}
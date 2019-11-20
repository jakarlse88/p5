using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TheCarHub.Models.Entities;

namespace TheCarHub.Models.InputModels
{
    public class ListingInputModel
    {
        public ListingInputModel()
        {
            Car = new Car();
            RepairJob = new RepairJob();
            Files = new List<IFormFile>();
        }
        
        public int Id { get; set; }
        public string Title { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int CarYear { get; set; }
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
        
        public HashSet<Media> Media { get; set; }
        public IList<IFormFile> Files { get; set; }
        public HashSet<ListingTag> ListingTags { get; set; }
    }
}
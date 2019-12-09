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
            Media = new HashSet<Media>();
            Tags = new HashSet<ListingTag>();
            Files = new List<IFormFile>();
            ImgNames = new List<string>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int CarId { get; set; }
        public CarInputModel Car { get; set; }
//        public int CarYear { get; set; }
        public List<string> ImgNames { get; set; }
        public string Description { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public DateTime SaleDate { get; set; }
        public string Status { get; set; }
        public RepairJobInputModel RepairJob { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal PurchasePrice { get; set; }

        public IEnumerable<Media> Media { get; set; }
        public IEnumerable<ListingTag> Tags { get; set; }
        public IList<IFormFile> Files { get; set; }
    }
}
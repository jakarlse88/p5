using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            ImgNames = new List<string>();
        }
        
        public string Title { get; set; }
        public Car Car { get; set; }
        public int CarYear { get; set; }
        public List<string> ImgNames { get; set; }
        public string Description { get; set; }
        public DateTime PurchaseDate { get; set; }
        public RepairJob RepairJob { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        
        public IList<IFormFile> Files { get; set; }
    }
}
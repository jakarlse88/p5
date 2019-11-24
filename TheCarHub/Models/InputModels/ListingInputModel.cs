using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace TheCarHub.Models.InputModels
{
    public class ListingInputModel
    {
        public ListingInputModel()
        {
            Files = new List<IFormFile>();
            ImgNames = new List<string>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public CarInputModel Car { get; set; }
        public int CarYear { get; set; }
        public List<string> ImgNames { get; set; }
        public string Description { get; set; }
        public DateTime PurchaseDate { get; set; }
        public RepairJobInputModel RepairJob { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        
        public IList<IFormFile> Files { get; set; }
    }
}
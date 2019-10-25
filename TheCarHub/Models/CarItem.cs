using System;

namespace TheCarHub.Models 
{
    public class CarItem
    {
        public int CarId { get; set; }
        public int ListingItemId { get; set; }
        public ListingItem Listing { get; set; }
        public string VIN { get; set; }
        public DateTime Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public string Repairs { get; set; }
        public decimal RepairCost { get; set; }
        public DateTime LotDate { get; set; }
        public decimal SellingPrice { get; set; }
        public DateTime? SaleDate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using TheCarHub.Models.Entities;

namespace TheCarHub.Models.InputModels
{
    public class CarInputModel
    {
        public CarInputModel()
        {
            Listings = new HashSet<Listing>();
        }
        public int Id { get; set; }
        public string VIN { get; set; }
        public DateTime Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        
        public ICollection<Listing> Listings { get; set; }
        
    }
}
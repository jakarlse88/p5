using System.Collections.Generic;
using TheCarHub.Models.Entities;

namespace TheCarHub.Models.ViewModels
{
    public class CarViewModel
    {
        public int Id { get; set; }
        public List<Listing> Listings { get; set; }
        public string VIN { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
    }
}
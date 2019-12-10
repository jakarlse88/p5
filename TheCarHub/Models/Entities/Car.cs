using System.Collections.Generic;

namespace TheCarHub.Models.Entities 
{
    public class Car
    {
        public Car()
        {
            Listings = new HashSet<Listing>();
        }
        
        public int Id { get; set; }
        public string VIN { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        
        public ICollection<Listing> Listings { get; set; }
    }
}
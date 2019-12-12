using System.Collections.Generic;

namespace TheCarHub.Models.Entities
{
    public class Status
    {
        public Status()
        {
            Listings = new HashSet<Listing>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Listing> Listings { get; set; }
    }
}
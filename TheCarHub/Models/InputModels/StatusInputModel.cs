using System.Collections.Generic;
using TheCarHub.Models.Entities;

namespace TheCarHub.Models.InputModels
{
    public class StatusInputModel
    {
        public StatusInputModel()
        {
            Listings = new HashSet<Listing>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Listing> Listings { get; set; }
    }
}
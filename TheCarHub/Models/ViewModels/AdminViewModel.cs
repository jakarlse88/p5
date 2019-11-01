using System.Collections.Generic;

namespace TheCarHub.Models
{
    public class AdminViewModel
    {
        public List<Car> Cars { get; set; }
        public List<Listing> Listings { get; set; }
    }
}
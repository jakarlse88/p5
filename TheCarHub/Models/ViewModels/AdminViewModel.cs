using System.Collections.Generic;
using TheCarHub.Models.Entities;

namespace TheCarHub.Models.ViewModels
{
    public class AdminViewModel
    {
        public List<Car> Cars { get; set; }
        public List<Listing> Listings { get; set; }
    }
}
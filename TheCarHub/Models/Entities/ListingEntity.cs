using System;
using System.Collections.Generic;

namespace TheCarHub.Models
{
    public class ListingEntity 
    {
        public int ListingEntityId { get; set; }
        // public int CarId { get; set; }
        // public CarItem Car { get; set; }
        // public List<ImageItem> Images { get; set; }
        public CarEntity Car { get; set; }
        public List<ImageEntity> Images { get; set; }
        public string Description { get; set; }
        public string ListingStatus { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateLastUpdated { get; set; }
    }
}
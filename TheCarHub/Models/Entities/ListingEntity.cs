using System;
using System.Collections.Generic;

namespace TheCarHub.Models
{
    public class ListingEntity 
    {
        public int ListingId { get; set; }
        public CarEntity Car { get; set; }
        public List<ImageEntity> Images { get; set; }
        public string Description { get; set; }
        public string ListingStatus { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateLastUpdated { get; set; }
    }
}
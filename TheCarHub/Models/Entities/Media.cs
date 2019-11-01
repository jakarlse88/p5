using System;
using System.Collections.Generic;

namespace TheCarHub.Models
{
    public class Media
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string Caption { get; set; }
        public int ListingId { get; set; }
        public Listing Listing { get; set; }
        public List<MediaTag> MediaTags { get; set; }   
    }
}
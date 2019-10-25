using System;

namespace TheCarHub.Models
{
    public class ImageItem
    {
        public int ImageItemId { get; set; }
        public Uri ImageLocation { get; set; }
        public int ListingItemId { get; set; }
        public ListingItem Listing { get; set; }
    }
}
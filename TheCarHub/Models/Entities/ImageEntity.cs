using System;

namespace TheCarHub.Models
{
    public class ImageEntity
    {
        public int ImageEntityId { get; set; }
        public byte[] Content { get; set; }
        public int ListingEntityId { get; set; }
        public ListingEntity Listing { get; set; }
    }
}
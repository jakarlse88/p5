using System.Collections.Generic;

namespace TheCarHub.Models.Entities
{
    public class Media
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Caption { get; set; }
        public int ListingId { get; set; }
        public List<MediaTag> Tags { get; set; }
    }
}
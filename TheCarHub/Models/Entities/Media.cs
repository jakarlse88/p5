using System.Collections.Generic;

namespace TheCarHub.Models.Entities
{
    public class Media
    {
        public Media()
        {
            Tags = new HashSet<MediaTag>();
        }        
        
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Caption { get; set; }
        public int ListingId { get; set; }
        
        public ICollection<MediaTag> Tags { get; set; }
    }
}
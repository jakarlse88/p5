using System.Collections.Generic;

namespace TheCarHub.Models.Entities 
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagName { get; set; }

        public List<MediaTag> MediaTags { get; set; }
        public List<ListingTag> ListingTags { get; set; }
    }
}
using System.Collections.Generic;

namespace TheCarHub.Models.Entities 
{
    public class Tag
    {
        public Tag()
        {
            Tags = new HashSet<MediaTag>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<MediaTag> Tags { get; set; }
    }
}
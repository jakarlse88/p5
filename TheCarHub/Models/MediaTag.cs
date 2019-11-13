using TheCarHub.Models.Entities;

namespace TheCarHub.Models
{
    public class MediaTag
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        
        public int MediaId { get; set; }
        public Media Media { get; set; }   
    }
}
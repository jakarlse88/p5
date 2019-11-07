using TheCarHub.Models.Entities;

namespace TheCarHub.Models
{
    public class MediaTag
    {
        public int TagForeignKey { get; set; }
//        public Tag Tag { get; set; }
        
        public int MediaForeignKey { get; set; }
//        public Media Media { get; set; }   
    }
}
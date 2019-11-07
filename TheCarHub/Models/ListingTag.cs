using TheCarHub.Models.Entities;

namespace TheCarHub.Models
{
    public class ListingTag
    {
        public int ListingForeignKey { get; set; }

        public int TagForeignKey { get; set; }
    }
}
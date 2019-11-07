using System.Collections.Generic;

namespace TheCarHub.Models.Entities
{
    public class Media
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Caption { get; set; }
        public int ListingForeignKey { get; set; }
    }
}
using TheCarHub.Models.Entities;

namespace TheCarHub.Models.ViewModels
{
    public class RepairJobViewModel
    {
        public int Id { get; set; }
        public int ListingId { get; set; }
        public Listing Listing { get; set; }
        public decimal Cost { get; set; }
        public decimal Tax { get; set; }
    }
}
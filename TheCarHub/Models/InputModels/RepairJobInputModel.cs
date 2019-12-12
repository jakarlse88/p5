namespace TheCarHub.Models.InputModels
{
    public class RepairJobInputModel
    {
        public int Id { get; set; }
        public int ListingId { get; set; }
        public ListingInputModel Listing { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public decimal Tax { get; set; }
    }
}
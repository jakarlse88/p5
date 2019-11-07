namespace TheCarHub.Models.Entities
{
    public class RepairJob
    {
        public int Id { get; set; }
        public int ListingForeignKey { get; set; }
        public decimal Cost { get; set; }
        public decimal Tax { get; set; }
    }
}
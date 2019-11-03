using System;
using System.Collections.Generic;

namespace TheCarHub.Models.Entities
{
    public class RepairJob
    {
        public int Id { get; set; }
        public int ListingId { get; set; }
        public Listing Listing { get; set; }
        public List<SparePart> Parts { get; set; }
        public DateTime Hours { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal Cost { get; set; }
    }
}
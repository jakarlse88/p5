using System;
using System.Collections.Generic;

namespace TheCarHub.Models.Entities
{
    public class RepairJob
    {
        public int Id { get; set; }
        public int ListingId { get; set; }
        public Listing Listing { get; set; }
        public decimal Cost { get; set; }
        public decimal Tax { get; set; }
    }
}
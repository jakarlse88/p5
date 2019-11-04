using System;
using System.Collections.Generic;

namespace TheCarHub.Models 
{
    public class Car
    {
        public int Id { get; set; }
        public List<Listing> Listings { get; set; }
        public string VIN { get; set; }
        public string Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
    }
}
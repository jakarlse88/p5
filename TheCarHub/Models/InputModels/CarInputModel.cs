using System;

namespace TheCarHub.Models.InputModels
{
    public class CarInputModel
    {
        public string VIN { get; set; }
        public DateTime Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
    }
}
using System;

namespace JOIEnergy.Models
{
    public class CostResponse
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal Cost { get; set; }
    }
}
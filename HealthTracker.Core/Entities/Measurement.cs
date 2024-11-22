using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Core.Entities
{
    public class Measurement : BaseEntity
    {
        public string UserId { get; set; } 
        public string Type { get; set; } // Weight, Height, etc.
        public double Value { get; set; }
        public string Unit { get; set; } //kg, cm
        public double Target { get; set; } 
        public DateTime MeasurementDate { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Core.Entities
{
    public class Measurement : BaseEntity
    {
        public string UserId { get; set; } // Changed from Guid to string
        public string Type { get; set; } // Weight, Height, etc.
        public double Value { get; set; }
        public string Unit { get; set; } // e.g., kg, cm
        public double Target { get; set; } // Target value
        public DateTime MeasurementDate { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
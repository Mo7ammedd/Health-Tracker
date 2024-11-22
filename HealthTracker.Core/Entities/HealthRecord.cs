using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Core.Entities
{
    public class HealthRecord : BaseEntity
    {
        public string UserId { get; set; } 
        public DateTime RecordDate { get; set; }
        public string Notes { get; set; }
        public double BloodPressure { get; set; }
        public double HeartRate { get; set; }
        public double BloodSugar { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
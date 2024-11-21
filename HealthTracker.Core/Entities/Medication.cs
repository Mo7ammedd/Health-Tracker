using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Core.Entities
{
    public class Medication : BaseEntity
    {
        public string UserId { get; set; } // Changed from Guid to string
        public string Name { get; set; }
        public string Dosage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Frequency { get; set; } // e.g., Once a day, Twice a day

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
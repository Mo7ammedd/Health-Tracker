using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Core.Entities
{
    public class Appointment : BaseEntity
    {
        public string UserId { get; set; } 
        public string DoctorName { get; set; }
        public string Specialty { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
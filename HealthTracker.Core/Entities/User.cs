using Microsoft.AspNetCore.Identity;

namespace HealthTracker.Core.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }

        public ICollection<HealthRecord> HealthRecords { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
        public ICollection<Diet> Diets { get; set; }
        public ICollection<Measurement> Measurements { get; set; }
        public ICollection<Medication> Medications { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
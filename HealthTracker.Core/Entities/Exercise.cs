using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Core.Entities
{
    public class Exercise : BaseEntity
    {
        public string UserId { get; set; } // Changed from Guid to string
        public string Name { get; set; }
        public int Duration { get; set; } // Duration in minutes
        public DateTime ExerciseDate { get; set; }
        public string Type { get; set; } // e.g., Cardio, Strength
        public string Intensity { get; set; } // e.g., Low, Medium, High
        public int CaloriesBurned { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
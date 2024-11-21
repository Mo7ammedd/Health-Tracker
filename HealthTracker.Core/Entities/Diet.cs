using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Core.Entities
{
    public class Diet : BaseEntity
    {
        public string UserId { get; set; } // Changed from Guid to string
        public string MealType { get; set; } // Breakfast, Lunch, Dinner, Snack
        public string Description { get; set; }
        public DateTime MealDate { get; set; }
        public int Calories { get; set; }
        public double Protein { get; set; } // in grams
        public double Carbohydrates { get; set; } // in grams
        public double Fats { get; set; } // in grams

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
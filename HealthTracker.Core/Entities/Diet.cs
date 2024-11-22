using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTracker.Core.Entities
{
    public class Diet : BaseEntity
    {
        public string UserId { get; set; } 
        public string MealType { get; set; } 
        public string Description { get; set; }
        public DateTime MealDate { get; set; }
        public int Calories { get; set; }
        public double Protein { get; set; }
        public double Carbohydrates { get; set; } 
        public double Fats { get; set; } 

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
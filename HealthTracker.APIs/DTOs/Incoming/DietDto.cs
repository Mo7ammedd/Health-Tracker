namespace HealthTracker.APIs.DTOs;
public class DietDto
{
    public string Id { get; set; }
    public string MealType { get; set; }
    public string Description { get; set; }
    public DateTime MealDate { get; set; }
    public int Calories { get; set; }
    public double Protein { get; set; }
    public double Carbohydrates { get; set; }
    public double Fats { get; set; }
}
namespace HealthTracker.APIs.DTOs;

public class ExerciseDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }
    public DateTime ExerciseDate { get; set; }
    public string Type { get; set; }
    public string Intensity { get; set; }
    public int CaloriesBurned { get; set; }
}
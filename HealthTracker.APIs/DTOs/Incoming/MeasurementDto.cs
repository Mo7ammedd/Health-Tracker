namespace HealthTracker.APIs.DTOs;

public class MeasurementDto
{
    public string Id { get; set; }
    public string Type { get; set; }
    public double Value { get; set; }
    public string Unit { get; set; }
    public double Target { get; set; }
    public DateTime MeasurementDate { get; set; }
}
namespace HealthTracker.APIs.DTOs
{
    public class HealthRecordDto
    {
        public string UserId { get; set; }
        public DateTime RecordDate { get; set; }
        public string Notes { get; set; }
        public double BloodPressure { get; set; }
        public double HeartRate { get; set; }
        public double BloodSugar { get; set; }
    }
}
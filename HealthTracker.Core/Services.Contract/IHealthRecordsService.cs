using HealthTracker.Core.Entities;

namespace HealthTracker.Core.Services.Contract;

public interface IHealthRecordsService
{
    Task<IReadOnlyList<HealthRecord>> GetAllHealthRecordsAsync(string userId);
    Task<HealthRecord> GetHealthRecordByIdAsync(string id);
    Task AddHealthRecordAsync(HealthRecord healthRecord);
    Task UpdateHealthRecordAsync(HealthRecord healthRecord);
    Task DeleteHealthRecordAsync(string id);
}
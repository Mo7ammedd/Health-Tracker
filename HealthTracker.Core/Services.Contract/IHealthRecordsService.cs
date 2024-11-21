using HealthTracker.Core.Entities;

namespace HealthTracker.Core.Services.Contract;

public interface IHealthRecordsService
{
    Task<IReadOnlyList<HealthRecord>> GetAllHealthRecordsAsync();
    Task<HealthRecord> GetHealthRecordByIdAsync(Guid id);
    Task AddHealthRecordAsync(HealthRecord healthRecord);
    Task UpdateHealthRecordAsync(HealthRecord healthRecord);
    Task DeleteHealthRecordAsync(Guid id);
}
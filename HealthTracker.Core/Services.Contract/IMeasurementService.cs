using HealthTracker.Core.Entities;

namespace HealthTracker.Core.Services.Contract;

public interface IMeasurementService
{
    Task<IEnumerable<Measurement>> GetAllMeasurementsAsync();
    Task<Measurement> GetMeasurementByIdAsync(string id);
    Task AddMeasurementAsync(Measurement measurement);
    Task UpdateMeasurementAsync(Measurement measurement);
    Task DeleteMeasurementAsync(string id);
    Task<IEnumerable<Measurement>> GetMeasurementsByUserIdAsync(string userId);
    Task<Measurement> GetLatestMeasurementByUserIdAsync(string userId);

}
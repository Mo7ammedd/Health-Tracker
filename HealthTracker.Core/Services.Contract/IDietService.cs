using HealthTracker.Core.Entities;

namespace HealthTracker.Core.Services.Contract;

public interface IDietService
{
    
    Task<IEnumerable<Diet>> GetAllDietsAsync();
    Task<Diet> GetDietByIdAsync(string id);
    Task AddDietAsync(Diet diet);
    Task UpdateDietAsync(Diet diet);
    Task DeleteDietAsync(string id);
    Task<IEnumerable<Diet>> GetDietsByUserIdAsync(string userId);
    Task<Diet> GetLatestDietByUserIdAsync(string userId);
}
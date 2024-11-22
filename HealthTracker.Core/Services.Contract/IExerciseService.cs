using HealthTracker.Core.Entities;

namespace HealthTracker.Core.Services.Contract;

public interface IExerciseService
{
    Task<IEnumerable<Exercise>> GetAllExercisesAsync();
    Task<Exercise> GetExerciseByIdAsync(string id);
    Task AddExerciseAsync(Exercise exercise);
    Task UpdateExerciseAsync(Exercise exercise);
    Task DeleteExerciseAsync(string id);
    Task<IEnumerable<Exercise>> GetExercisesByUserIdAsync(string userId);
    Task<Exercise> GetLatestExerciseByUserIdAsync(string userId);
}
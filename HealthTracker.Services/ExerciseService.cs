using HealthTracker.Core;
using HealthTracker.Core.Entities;
using HealthTracker.Core.Services.Contract;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthTracker.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ExerciseService> _logger;

        public ExerciseService(IUnitOfWork unitOfWork, ILogger<ExerciseService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<Exercise>> GetAllExercisesAsync()
        {
            return await _unitOfWork.Repository<Exercise>().GetAllAsync();
        }

        public async Task<Exercise> GetExerciseByIdAsync(string id)
        {
            return await _unitOfWork.Repository<Exercise>().GetById(id);
        }

        public async Task AddExerciseAsync(Exercise exercise)
        {
            try
            {
                await _unitOfWork.Repository<Exercise>().AddAsync(exercise);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding an exercise.");
                throw;
            }
        }

        public async Task UpdateExerciseAsync(Exercise exercise)
        {
            try
            {
                _unitOfWork.Repository<Exercise>().Update(exercise);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating an exercise.");
                throw;
            }
        }

        public async Task DeleteExerciseAsync(string id)
        {
            try
            {
                var exercise = await _unitOfWork.Repository<Exercise>().GetById(id);
                if (exercise != null)
                {
                    _unitOfWork.Repository<Exercise>().Delete(exercise);
                    await _unitOfWork.CompleteAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting an exercise.");
                throw;
            }
        }

        public async Task<IEnumerable<Exercise>> GetExercisesByUserIdAsync(string userId)
        {
            return await _unitOfWork.Repository<Exercise>().GetAllAsync(e => e.UserId == userId);
        }

        public async Task<Exercise> GetLatestExerciseByUserIdAsync(string userId)
        {
            return await _unitOfWork.Repository<Exercise>().FirstOrDefaultAsync(e => e.UserId == userId);
        }
    }
}
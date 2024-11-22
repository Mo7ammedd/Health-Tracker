using HealthTracker.Core;
using HealthTracker.Core.Entities;
using HealthTracker.Core.Services.Contract;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthTracker.Services
{
    public class DietService : IDietService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DietService> _logger;

        public DietService(IUnitOfWork unitOfWork, ILogger<DietService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<Diet>> GetAllDietsAsync()
        {
            return await _unitOfWork.Repository<Diet>().GetAllAsync();
        }

        public async Task<Diet> GetDietByIdAsync(string id)
        {
            return await _unitOfWork.Repository<Diet>().GetById(id);
        }

        public async Task AddDietAsync(Diet diet)
        {
            try
            {
                await _unitOfWork.Repository<Diet>().AddAsync(diet);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a diet.");
                throw;
            }
        }

        public async Task UpdateDietAsync(Diet diet)
        {
            try
            {
                _unitOfWork.Repository<Diet>().Update(diet);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a diet.");
                throw;
            }
        }

        public async Task DeleteDietAsync(string id)
        {
            try
            {
                var diet = await _unitOfWork.Repository<Diet>().GetById(id);
                if (diet != null)
                {
                    _unitOfWork.Repository<Diet>().Delete(diet);
                    await _unitOfWork.CompleteAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a diet.");
                throw;
            }
        }

        public async Task<IEnumerable<Diet>> GetDietsByUserIdAsync(string userId)
        {
            return await _unitOfWork.Repository<Diet>().GetAllAsync(d => d.UserId == userId);
        }

        public async Task<Diet> GetLatestDietByUserIdAsync(string userId)
        {
            return await _unitOfWork.Repository<Diet>().FirstOrDefaultAsync(d => d.UserId == userId);
        }
    }
}
using HealthTracker.Core;
using HealthTracker.Core.Entities;
using HealthTracker.Core.Services.Contract;
using Microsoft.Extensions.Logging;

namespace HealthTracker.Services
{
    public class HealthRecordsService : IHealthRecordsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HealthRecordsService> _logger;

        public HealthRecordsService(IUnitOfWork unitOfWork, ILogger<HealthRecordsService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IReadOnlyList<HealthRecord>> GetAllHealthRecordsAsync(string userId)
        {
            return await _unitOfWork.Repository<HealthRecord>().GetAllAsync(hr => hr.UserId == userId);
        }

        

        public async Task<HealthRecord> GetHealthRecordByIdAsync(string id)
        {
            return await _unitOfWork.Repository<HealthRecord>().GetById(id);
        }

        public async Task AddHealthRecordAsync(HealthRecord healthRecord)
        {
            try
            {
                await _unitOfWork.Repository<HealthRecord>().AddAsync(healthRecord);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a health record.");
                throw;
            }
        }

        public async Task UpdateHealthRecordAsync(HealthRecord healthRecord)
        {
            try
            {
                _unitOfWork.Repository<HealthRecord>().Update(healthRecord);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a health record.");
                throw;
            }
        }

        public async Task DeleteHealthRecordAsync(string id)
        {
            try
            {
                var healthRecord = await _unitOfWork.Repository<HealthRecord>().GetById(id);
                if (healthRecord != null)
                {
                    _unitOfWork.Repository<HealthRecord>().Delete(healthRecord);
                    await _unitOfWork.CompleteAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a health record.");
                throw;
            }
        }
    }
}
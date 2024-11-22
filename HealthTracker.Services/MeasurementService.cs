using HealthTracker.Core;
using HealthTracker.Core.Entities;
using HealthTracker.Core.Services.Contract;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthTracker.Services
{
    public class MeasurementService : IMeasurementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MeasurementService> _logger;

        public MeasurementService(IUnitOfWork unitOfWork, ILogger<MeasurementService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<Measurement>> GetAllMeasurementsAsync()
        {
            return await _unitOfWork.Repository<Measurement>().GetAllAsync();
        }

        public async Task<Measurement> GetMeasurementByIdAsync(string id)
        {
            return await _unitOfWork.Repository<Measurement>().GetById(id);
        }

        public async Task AddMeasurementAsync(Measurement measurement)
        {
            try
            {
                await _unitOfWork.Repository<Measurement>().AddAsync(measurement);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a measurement.");
                throw;
            }
        }

        public async Task UpdateMeasurementAsync(Measurement measurement)
        {
            try
            {
                _unitOfWork.Repository<Measurement>().Update(measurement);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a measurement.");
                throw;
            }
        }

        public async Task DeleteMeasurementAsync(string id)
        {
            try
            {
                var measurement = await _unitOfWork.Repository<Measurement>().GetById(id);
                if (measurement != null)
                {
                    _unitOfWork.Repository<Measurement>().Delete(measurement);
                    await _unitOfWork.CompleteAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a measurement.");
                throw;
            }
        }

        public async Task<IEnumerable<Measurement>> GetMeasurementsByUserIdAsync(string userId)
        {
            return await _unitOfWork.Repository<Measurement>().GetAllAsync(m => m.UserId == userId);
        }

        public async Task<Measurement> GetLatestMeasurementByUserIdAsync(string userId)
        {
            return await _unitOfWork.Repository<Measurement>().FirstOrDefaultAsync(m => m.UserId == userId);
        }
    }
}
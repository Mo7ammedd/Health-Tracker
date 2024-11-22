using System.Security.Claims;
using HealthTracker.APIs.DTOs;
using HealthTracker.Core.Entities;
using HealthTracker.Core.Services.Contract;
using HealthTrracke.APIs.Controllers.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthTracker.APIs.Controllers.V1
{
    [Authorize]
    public class HealthRecordsController : BaseApiController
    {
        private readonly IHealthRecordsService _healthRecordsService;

        public HealthRecordsController(IHealthRecordsService healthRecordsService)
        {
            _healthRecordsService = healthRecordsService;
        }

        [HttpGet]
        // HealthRecordsController.cs
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<HealthRecord>>> GetAllHealthRecords()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var healthRecords = await _healthRecordsService.GetAllHealthRecordsAsync(userId);
            return Ok(healthRecords);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HealthRecord>> GetHealthRecordById(string id)
        {
            var healthRecord = await _healthRecordsService.GetHealthRecordByIdAsync(id);
            if (healthRecord == null)
            {
                return NotFound();
            }
            return Ok(healthRecord);
        }

        [HttpPost]
        public async Task<ActionResult> AddHealthRecord(HealthRecordDto healthRecordDto)
        {
            var healthRecord = new HealthRecord
            {
                UserId = healthRecordDto.UserId,
                RecordDate = healthRecordDto.RecordDate,
                Notes = healthRecordDto.Notes,
                BloodPressure = healthRecordDto.BloodPressure,
                HeartRate = healthRecordDto.HeartRate,
                BloodSugar = healthRecordDto.BloodSugar
            };

            await _healthRecordsService.AddHealthRecordAsync(healthRecord);
            return CreatedAtAction(nameof(GetHealthRecordById), new { id = healthRecord.Id }, healthRecord);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateHealthRecord(string id, HealthRecordDto healthRecordDto)
        {
            var healthRecord = await _healthRecordsService.GetHealthRecordByIdAsync(id);
            if (healthRecord == null)
            {
                return NotFound();
            }

            healthRecord.UserId = healthRecordDto.UserId;
            healthRecord.RecordDate = healthRecordDto.RecordDate;
            healthRecord.Notes = healthRecordDto.Notes;
            healthRecord.BloodPressure = healthRecordDto.BloodPressure;
            healthRecord.HeartRate = healthRecordDto.HeartRate;
            healthRecord.BloodSugar = healthRecordDto.BloodSugar;

            await _healthRecordsService.UpdateHealthRecordAsync(healthRecord);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHealthRecord(string id)
        {
            var healthRecord = await _healthRecordsService.GetHealthRecordByIdAsync(id);
            if (healthRecord == null)
            {
                return NotFound();
            }

            await _healthRecordsService.DeleteHealthRecordAsync(id);
            return NoContent();
        }
    }
}
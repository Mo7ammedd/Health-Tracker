using HealthTracker.Core.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HealthTracker.APIs.DTOs;
using HealthTracker.Core.Entities;

namespace HealthTracker.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;
        private readonly IMeasurementService _measurementService;
        private readonly IDietService _dietService;

        public HealthController(IExerciseService exerciseService, IMeasurementService measurementService, IDietService dietService)
        {
            _exerciseService = exerciseService;
            _measurementService = measurementService;
            _dietService = dietService;
        }

        private string GetUserIdFromToken()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [HttpGet("exercises")]
        public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetExercises()
        {
            var userId = GetUserIdFromToken();
            var exercises = await _exerciseService.GetExercisesByUserIdAsync(userId);
            var exerciseDtos = exercises.Select(e => new ExerciseDto
            {
                Id = e.Id,
                Name = e.Name,
                Duration = e.Duration,
                ExerciseDate = e.ExerciseDate,
                Type = e.Type,
                Intensity = e.Intensity,
                CaloriesBurned = e.CaloriesBurned
            });
            return Ok(exerciseDtos);
        }

        [HttpPost("exercises")]
        public async Task<ActionResult> AddExercise([FromBody] ExerciseDto exerciseDto)
        {
            var userId = GetUserIdFromToken();
            var exercise = new Exercise
            {
                UserId = userId,
                Name = exerciseDto.Name,
                Duration = exerciseDto.Duration,
                ExerciseDate = exerciseDto.ExerciseDate,
                Type = exerciseDto.Type,
                Intensity = exerciseDto.Intensity,
                CaloriesBurned = exerciseDto.CaloriesBurned
            };
            await _exerciseService.AddExerciseAsync(exercise);
            return Ok();
        }

        [HttpGet("measurements")]
        public async Task<ActionResult<IEnumerable<MeasurementDto>>> GetMeasurements()
        {
            var userId = GetUserIdFromToken();
            var measurements = await _measurementService.GetMeasurementsByUserIdAsync(userId);
            var measurementDtos = measurements.Select(m => new MeasurementDto
            {
                Id = m.Id,
                Type = m.Type,
                Value = m.Value,
                Unit = m.Unit,
                Target = m.Target,
                MeasurementDate = m.MeasurementDate
            });
            return Ok(measurementDtos);
        }

        [HttpPost("measurements")]
        public async Task<ActionResult> AddMeasurement([FromBody] MeasurementDto measurementDto)
        {
            var userId = GetUserIdFromToken();
            var measurement = new Measurement
            {
                UserId = userId,
                Type = measurementDto.Type,
                Value = measurementDto.Value,
                Unit = measurementDto.Unit,
                Target = measurementDto.Target,
                MeasurementDate = measurementDto.MeasurementDate
            };
            await _measurementService.AddMeasurementAsync(measurement);
            return Ok();
        }

        [HttpGet("diets")]
        public async Task<ActionResult<IEnumerable<DietDto>>> GetDiets()
        {
            var userId = GetUserIdFromToken();
            var diets = await _dietService.GetDietsByUserIdAsync(userId);
            var dietDtos = diets.Select(d => new DietDto
            {
                Id = d.Id,
                MealType = d.MealType,
                Description = d.Description,
                MealDate = d.MealDate,
                Calories = d.Calories,
                Protein = d.Protein,
                Carbohydrates = d.Carbohydrates,
                Fats = d.Fats
            });
            return Ok(dietDtos);
        }

        [HttpPost("diets")]
        public async Task<ActionResult> AddDiet([FromBody] DietDto dietDto)
        {
            var userId = GetUserIdFromToken();
            var diet = new Diet
            {
                UserId = userId,
                MealType = dietDto.MealType,
                Description = dietDto.Description,
                MealDate = dietDto.MealDate,
                Calories = dietDto.Calories,
                Protein = dietDto.Protein,
                Carbohydrates = dietDto.Carbohydrates,
                Fats = dietDto.Fats
            };
            await _dietService.AddDietAsync(diet);
            return Ok();
        }
    }
}
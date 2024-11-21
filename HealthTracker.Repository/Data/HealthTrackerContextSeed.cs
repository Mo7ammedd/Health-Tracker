using System.Text.Json;
using HealthTracker.Core.Entities;

namespace HealthTracker.Repository.Data;

public class HealthTrackerContextSeed
{
    public async static Task SeedAsync(HealthTrackerDbContext _dbContext)
    {
        if (!_dbContext.Users.Any())
        {
            var usersData = await File.ReadAllTextAsync("../HealthTracker.Repository/Data/DataSeed/User.json");
            var users = JsonSerializer.Deserialize<List<User>>(usersData);
            if (users is not null && users.Count > 0)
            {
                foreach (var user in users)
                {
                    _dbContext.Set<User>().Add(user);
                }
                await _dbContext.SaveChangesAsync();
            }
        }
        if (!_dbContext.Appointments.Any())
        {
            var appointmentsData = await File.ReadAllTextAsync("../HealthTracker.Repository/Data/DataSeed/Appointment.json");
            var appointments = JsonSerializer.Deserialize<List<Appointment>>(appointmentsData);
            if (appointments is not null && appointments.Count > 0)
            {
                foreach (var appointment in appointments)
                {
                    _dbContext.Set<Appointment>().Add(appointment);
                }
                await _dbContext.SaveChangesAsync();
            }
        }


        if (!_dbContext.HealthRecords.Any())
        {
            var healthRecordsData = await File.ReadAllTextAsync("../HealthTracker.Repository/Data/DataSeed/HealthRecord.json");
            var healthRecords = JsonSerializer.Deserialize<List<HealthRecord>>(healthRecordsData);
            if (healthRecords is not null && healthRecords.Count > 0)
            {
                foreach (var healthRecord in healthRecords)
                {
                    _dbContext.Set<HealthRecord>().Add(healthRecord);
                }
                await _dbContext.SaveChangesAsync();
            }
        }

        if (!_dbContext.Exercises.Any())
        {
            var exercisesData = await File.ReadAllTextAsync("../HealthTracker.Repository/Data/DataSeed/Exercise.json");
            var exercises = JsonSerializer.Deserialize<List<Exercise>>(exercisesData);
            if (exercises is not null && exercises.Count > 0)
            {
                foreach (var exercise in exercises)
                {
                    _dbContext.Set<Exercise>().Add(exercise);
                }
                await _dbContext.SaveChangesAsync();
            }
        }

        if (!_dbContext.Diets.Any())
        {
            var dietsData = await File.ReadAllTextAsync("../HealthTracker.Repository/Data/DataSeed/Diet.json");
            var diets = JsonSerializer.Deserialize<List<Diet>>(dietsData);
            if (diets is not null && diets.Count > 0)
            {
                foreach (var diet in diets)
                {
                    _dbContext.Set<Diet>().Add(diet);
                }
                await _dbContext.SaveChangesAsync();
            }
        }

        if (!_dbContext.Measurements.Any())
        {
            var measurementsData = await File.ReadAllTextAsync("../HealthTracker.Repository/Data/DataSeed/Measurement.json");
            var measurements = JsonSerializer.Deserialize<List<Measurement>>(measurementsData);
            if (measurements is not null && measurements.Count > 0)
            {
                foreach (var measurement in measurements)
                {
                    _dbContext.Set<Measurement>().Add(measurement);
                }
                await _dbContext.SaveChangesAsync();
            }
        }

        if (!_dbContext.Medications.Any())
        {
            var medicationsData = await File.ReadAllTextAsync("../HealthTracker.Repository/Data/DataSeed/Medication.json");
            var medications = JsonSerializer.Deserialize<List<Medication>>(medicationsData);
            if (medications is not null && medications.Count > 0)
            {
                foreach (var medication in medications)
                {
                    _dbContext.Set<Medication>().Add(medication);
                }
                await _dbContext.SaveChangesAsync();
            }
        }
        
    }
}
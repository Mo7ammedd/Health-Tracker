using HealthTracker.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthTracker.Repository.Data;

public class HealthTrackerDbContext : IdentityDbContext
{

    public HealthTrackerDbContext(DbContextOptions<HealthTrackerDbContext> options) : base(options)
    {
      
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<HealthRecord> HealthRecords { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<Diet> Diets { get; set; }
    public DbSet<Measurement> Measurements { get; set; }
    public DbSet<Medication> Medications { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
}
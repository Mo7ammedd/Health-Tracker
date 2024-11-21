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
}
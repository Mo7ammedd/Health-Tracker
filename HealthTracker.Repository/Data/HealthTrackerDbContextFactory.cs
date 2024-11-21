using HealthTracker.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

public class HealthTrackerDbContextFactory : IDesignTimeDbContextFactory<HealthTrackerDbContext>
{
    public HealthTrackerDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "HealthTracker.APIs"))
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<HealthTrackerDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        return new HealthTrackerDbContext(optionsBuilder.Options);
    }
}
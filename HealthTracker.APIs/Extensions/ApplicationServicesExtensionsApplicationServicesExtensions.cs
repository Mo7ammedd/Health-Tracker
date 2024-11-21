using Asp.Versioning;
using HealthTracker.Core;
using HealthTracker.Core.IRepositories;
using HealthTracker.Core.Services.Contract;
using HealthTracker.Repository;
using HealthTracker.Repository.Data;
using HealthTracker.Services;
using Microsoft.EntityFrameworkCore;

namespace HealthTracker.APIs.Extentions;

public static class ApplicationServicesExtensionsApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IHealthRecordsService, HealthRecordsService>();
        services.AddDbContext<HealthTrackerDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });
        return services;
    }
}
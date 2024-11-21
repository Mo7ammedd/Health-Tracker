using Asp.Versioning;
using HealthTracker.APIs.Extentions;
using HealthTracker.APIs.Middlewares;
using HealthTracker.Core;
using HealthTracker.Core.Entities;
using HealthTracker.Core.IRepositories;
using HealthTracker.Repository;
using HealthTracker.Repository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerServices();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();
// using var scope = app.Services.CreateScope();
//           
// #region DatabaseMigration
// // Ask CLR to create a scope for the service provider
// var services = scope.ServiceProvider;
// var _dbcontext = services.GetRequiredService<HealthTrackerDbContext>();
// var loggerFactory = services.GetRequiredService<ILoggerFactory>();
// try
// {
//     await _dbcontext.Database.MigrateAsync();
//     await HealthTrackerContextSeed.SeedAsync(_dbcontext);
//     var _userManager = services.GetRequiredService<UserManager<User>>();
//                 
// }
// catch (Exception e)
// {
//     var logger = loggerFactory.CreateLogger<Program>();
//     logger.LogError(e, "An error occurred during migration");
// }
// #endregion

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerMiddleWare();
}
app.UseStatusCodePagesWithRedirects("/errors/{0}");

app.UseHttpsRedirection();
            
app.UseStaticFiles();
            
app.MapControllers();

app.UseAuthentication();

app.UseAuthorization();


app.Run();
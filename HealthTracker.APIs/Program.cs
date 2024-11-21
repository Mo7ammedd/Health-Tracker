using Asp.Versioning;
using HealthTracker.APIs.Extentions;
using HealthTracker.APIs.Middlewares;
using HealthTracker.Core;
using HealthTracker.Core.IRepositories;
using HealthTracker.Repository;
using HealthTracker.Repository.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>(); 
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); 
builder.Services.AddDbContext<HealthTrackerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStatusCodePagesWithRedirects("/errors/{0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.Run();
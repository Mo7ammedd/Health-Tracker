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
builder.Services.AddSwaggerServices();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
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
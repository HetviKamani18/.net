using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserManagementApi.Middleware;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

var app = builder.Build();

// Use custom logging middleware
app.UseMiddleware<LoggingMiddleware>();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run(); 
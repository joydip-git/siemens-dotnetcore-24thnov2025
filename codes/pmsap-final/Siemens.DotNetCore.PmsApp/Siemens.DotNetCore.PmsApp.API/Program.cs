using Microsoft.EntityFrameworkCore;
using Siemens.DotNetCore.PmsApp.DTOs;
using Siemens.DotNetCore.PmsApp.Repository;
using Siemens.DotNetCore.PmsApp.ServiceManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Action<DbContextOptionsBuilder> optionsBuilder
    = optBuilder => optBuilder.UseSqlServer(builder.Configuration.GetConnectionString("SiemensDbConStr"));

builder.Services.AddDbContext<SiemensDbContext>(optionsBuilder, ServiceLifetime.Scoped, ServiceLifetime.Scoped);

builder.Services.AddScoped<IAsyncServiceManager<ProductDTO, int>, ProductAsyncServiceManager>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

using EfCoreDemo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
//builder
//    .Services
//    .Configure<DbContextOptions<SiemensDbContext>>(options =>
//{
//    builder.Configuration.GetRequiredSection("ConnectionStrings:siemensDbConStr").Bind(options);
//});
builder
    .Services
    .AddDbContext<SiemensDbContext>(
    (DbContextOptionsBuilder options) =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("siemensDbConStr"));
    });

IHost host = builder.Build();
IServiceProvider serviceProvider = host.Services;

using (IServiceScope scope = serviceProvider.CreateScope())
{
    IServiceProvider services = scope.ServiceProvider;

    var dbContext = services.GetRequiredService<SiemensDbContext>();

    var employees = dbContext.Employees.ToList();
    foreach (var emp in employees)
    {
        Console.WriteLine($"ID: {emp.EmpId}, Name: {emp.EmpName}, Department: {emp.Department}, Salary: {emp.Salary}");
    }
}

await host.RunAsync();
//Console.WriteLine("");
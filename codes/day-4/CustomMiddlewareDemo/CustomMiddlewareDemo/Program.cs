var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

//app.UseCustomLoggerMiddleware();
app.MapGet("/", () => "Hello World!");

app.Run();

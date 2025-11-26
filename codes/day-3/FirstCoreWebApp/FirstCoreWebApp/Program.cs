using FirstCoreWebApp;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddSingleton<IRepo, Repo>()
    .AddControllers();

WebApplication app = builder.Build();

app.MapControllers();


//Delegate handler = () => "Hello World";

//app.MapGet("/welcome", handler);
//app.MapGet("/page", () => new { Name="joy"});
app.Run();

using FirstCoreWebApp;

namespace FirstCoreWebApp
{
    class Program
    {
        static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder
                .Services
                .AddSingleton<IRepo, Repo>()
                .AddControllers();

            //builder.Logging.AddSimpleConsole(options => options.SingleLine = true);

            WebApplication app = builder.Build();

            app.Logger.LogInformation($"Main Thread: {Environment.CurrentManagedThreadId}");

            app.UseCustomLoggerMiddleware();
            app.MapControllers();


            //Delegate handler = () => "Hello World";

            //app.MapGet("/welcome", handler);
            //app.MapGet("/page", () => new { Name="joy"});
            app.Run();

        }
    }
}

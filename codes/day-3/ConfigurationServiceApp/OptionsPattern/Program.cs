using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace OptionsPattern
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                HostApplicationBuilder hostBuilder = Host.CreateApplicationBuilder(args);

                IServiceCollection servicRegistry = hostBuilder.Services;

                Action<DbSetting> strOptDel = dbSetting => dbSetting.FilePath = "some file path";

                //register an IOptions<string> service with service registry, which requires to be dependency injected in the next service, IDataProvider type
                servicRegistry.Configure<DbSetting>(strOptDel);
                //servicRegistry.AddOptions<string>("some file path");

                servicRegistry
                    .AddSingleton<IDataProvider, DbDataProvider>();

                IHost host = hostBuilder.Build();

                IServiceProvider serviceProvider = host.Services;
                IDataProvider dataProvider = serviceProvider.GetRequiredService<IDataProvider>();

                Console.WriteLine(dataProvider.GetData());

                await host.RunAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConfigurationWithHostingService
{
    internal class Program
    {
        /*
        static void ConfigiureServices()
        {
            //create registry
            //IServiceCollection servicesRegistry = new ServiceCollection();
            //IConfigurationBuilder configBuilder = new ConfigurationBuilder();
            HostApplicationBuilder hostBuilder = Host.CreateApplicationBuilder([""]);

            //servicesRegistry.Add(null);
            //configBuilder.AddJsonFile(null);
            hostBuilder.Services.Add(null);
            ConfigurationManager manager = hostBuilder.Configuration;
            manager.SetBasePath().AddJsonFile();
            //hostBuilder.Services.AddLogging();

            //IServiceProvider provider = servicesRegistry.BuildServiceProvider();
            //IConfigurationRoot provider = configBuilder.Build();
            
            IHost host = hostBuilder.Build();
            IServiceProvider serviceProvider = host.Services;
            //serviceProvider.GetService<IConfiguration>();
            


        }
        */
        static async Task Main(string[] args)
        {
            HostApplicationBuilder hostBuilder = Host.CreateApplicationBuilder(args);

            //configure
            //1. 
            ConfigurationManager configurationManager = hostBuilder.Configuration;

            configurationManager
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appSettings.json", false, true);

            //IServiceCollection registry = hostBuilder.Services;

            //create host
            IHost host = hostBuilder.Build();
            //IServiceProvider provider = host.Services;
            //IConfiguration configProvider = provider.GetRequiredService<IConfiguration>();


            var productDbSetting = new ProductDbSetting();
            configurationManager
                .GetRequiredSection("databaseSettings:productdbSetting")
                .Bind(productDbSetting);

            Console.WriteLine(productDbSetting.Server);

            //run the host
            await host.RunAsync();
        }
    }
}

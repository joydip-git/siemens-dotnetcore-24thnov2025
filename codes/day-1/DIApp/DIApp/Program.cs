//using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DIApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //creating a regitry
            //IServiceCollection services = new ServiceCollection();

            //registering services
            //ServiceDescriptor descriptor = new ServiceDescriptor(typeof(IDbRepo), typeof(DbRepo), ServiceLifetime.Singleton);
            //services.Add(descriptor);
            //services
            //    .AddSingleton<IRepo, DbRepo>();

            //IServiceProvider serviceProvider = services.BuildServiceProvider();

            //IRepo dbRepo = serviceProvider.GetRequiredService<IRepo>();
            //Console.WriteLine(dbRepo.GetData());

            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

            //Services returns you an instance of IServiceCollection
            IServiceCollection registry = builder.Services;
            registry.AddSingleton<IRepo, DbRepo>();

            // Console.WriteLine("builder created and configured");

            IHost host = builder.Build();


            // Console.WriteLine("host created and configured");

            //Services returns you an instance of IServiceProvider
            IServiceProvider serviceProvider = host.Services;

            // Console.WriteLine("provider created and configured");

            IRepo dbRepo = serviceProvider.GetRequiredService<IRepo>();
            Console.WriteLine(dbRepo.GetData());

            host.Run();

        }
    }
}

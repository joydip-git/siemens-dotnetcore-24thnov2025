using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Siemens.DotNet.Pms.Entities;
using Siemens.DotNet.Pms.Manager;
using Siemens.DotNet.Pms.Repository;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
ConfigureServices(builder);

IHost host = builder.Build();

await ContactManager(host.Services);
await ContactManager(host.Services);

await host.RunAsync();



static async Task ContactManager(IServiceProvider serviceProvider)
{
    using (IServiceScope scope = serviceProvider.CreateScope())
    {
        var provider = scope.ServiceProvider;

        var manager = provider.GetRequiredService<IAsyncManager<Product, int>>();
        var products = await manager.GetAllAsync();
        foreach (var item in products)
        {
            Console.WriteLine(item);
        }

        var manager2 = provider.GetRequiredService<IAsyncManager<Product, int>>();
        var products2 = await manager2.GetAllAsync();
        foreach (var item in products2)
        {
            Console.WriteLine(item);
        }
    }
}

static void ConfigureServices(IHostApplicationBuilder builder)
{
    //in case the settings file name is something other than expected (appSettings.json), do configure the existing configuration provider in the following manner
    builder
        .Configuration
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appSettings.json", false, true);

    //registering IOptions<FileSetting> service
    builder
        .Services
        .Configure<FileSetting>(fs =>
            builder
            .Configuration
            .GetRequiredSection("fileSetting")
            .Bind(fs)
        );

    //registering IAsyncRepository<T> type service ( depends on IOptions<FileSetting> type service)
    //registering IAsyncManager<T, TId> type (depends on IAsyncRepository<T> type service)

    //1. as transient service: in a service scope whenever you demand instance of a service class, every time new instance will be created. all the instances will disposed off at the end of the scope

    //builder
    //    .Services
    //    .AddTransient<IAsyncRepository<Product>, AsyncFileRepository<Product>>()
    //    .AddTransient<IAsyncManager<Product, int>, AsyncProductManager>();


    //2. as scoped service: in a service scope if you demand to create a new instance of service mutiple times, only one instance will be created and that same instance will be served multiple times in the same scope, rather than creating new instance every time in that scope. this instance will be disposed off at the end of the scope where it was created.

    //builder
    //    .Services
    //    .AddScoped<IAsyncRepository<Product>, AsyncFileRepository<Product>>()
    //    .AddScoped<IAsyncManager<Product, int>, AsyncProductManager>();

    //3. as singleton service: for entire application only a single instance will be created. this instance though created in a scope, is NOT tied to any scope and is NOT disposed of at the end of a scope, rather at the end of the application
    builder
        .Services
        .AddSingleton<IAsyncRepository<Product>, AsyncFileRepository<Product>>()
        .AddSingleton<IAsyncManager<Product, int>, AsyncProductManager>();

   
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OptionsPattern;

//top level statement template
try
{
    //creating a Host builder
    HostApplicationBuilder hostBuilder = CreateHostBuilder();

    //Host Builder has an instance of service collection, which you can fetch the reference of, by using Services property of the builder
    IServiceCollection servicRegistry = hostBuilder.Services;

    ConfigurationManager manager = hostBuilder.Configuration;

    manager
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("settings.json", false, true);


    //register an IOptions<DbSetting> service with service registry, which requires to be dependency injected in the next service, IDataProvider type

    //when this call back method will be invoked by Configure<DbSetting>() method, the .NET runtime will create an instance of DbSetting class and pass the same to this callback as an argument. this instance you can re-configure in the method body
    //the callback must not return anything (as Action delegate suggests

    //Action<DbSetting> dbSettingDel = (DbSetting dbSetting) => dbSetting.DbPath = manager.GetRequiredSection("dbSetting:dbPath").Value ?? "default path";

    Action<DbSetting> dbSettingDel = (DbSetting dbSetting) => manager.GetRequiredSection("dbSetting").Bind(dbSetting);

    //pass the callback to the Configure method
    servicRegistry.Configure<DbSetting>(dbSettingDel);
    //servicRegistry.Configure<DbSetting>(setting => setting.FilePath = "some file path");

    //registering IDataProvider type which has dependency on IOptions<DbSetting> type
    //servicRegistry
    //    .AddSingleton<IDataProvider, SqlDbDataProvider>();

    //while regsitering two classes implementing the same non-generic interface you have to use keyed service registration technique
    servicRegistry
        .AddKeyedSingleton<IDataProvider, SqlDbDataProvider>("sql")
        .AddKeyedSingleton<IDataProvider, OracleDbDataProvider>("oracle")
        .AddSingleton<IRepo<Employee>, EmployeeRepo>()
        .AddSingleton<IRepo<Department>, DepartmentRepo>();


    //building Host
    IHost host = hostBuilder.Build();

    //getting service provider from the IHost
    IServiceProvider serviceProvider = host.Services;

    //asking service provider to create an service which implements IDataProvider instance and return the same
    //IDataProvider dataProvider = serviceProvider.GetRequiredService<IDataProvider>();
    IDataProvider sqlDataProvider = serviceProvider.GetRequiredKeyedService<IDataProvider>("sql");

    IDataProvider orclDataProvider = serviceProvider.GetRequiredKeyedService<IDataProvider>("oracle");

    IRepo<Employee> empRepo = serviceProvider.GetRequiredService<IRepo<Employee>>();

    Console.WriteLine(sqlDataProvider.GetData());
    Console.WriteLine(orclDataProvider.GetData());

    await host.RunAsync();
}
catch (Exception e)
{
    Console.WriteLine(e);
}
//inner function => local (static) function 
static HostApplicationBuilder CreateHostBuilder()
{
    return Host.CreateApplicationBuilder();
}
class Garden() { }

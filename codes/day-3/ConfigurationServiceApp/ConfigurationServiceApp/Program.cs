using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationServiceApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();

            var settings = new Dictionary<string, string?>
            {
                ["filePath"] = "some path",
                ["dbconstr"] = "connecstion string"
            };

            builder
                .AddInMemoryCollection(settings)
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", false, true);


            IConfigurationRoot configProvider = builder.Build();

            Console.WriteLine(configProvider["filePath"]);
            Console.WriteLine(configProvider.GetRequiredSection("filePath").Value);
            Console.WriteLine(configProvider.GetRequiredSection("databaseSettings").GetRequiredSection("productdbSetting").GetRequiredSection("database").Value);
            Console.WriteLine(configProvider.GetRequiredSection("databaseSettings:productdbSetting:server").Value);

            var dbSettings = new DatabaseSettings();
            dbSettings.ProductDbSetting = new ProductDbSetting();
            configProvider
                .GetRequiredSection("databaseSettings")
                .Bind(dbSettings);
            Console.WriteLine(dbSettings.ProductDbSetting.UserName);

            var productDbSetting = new ProductDbSetting();
            configProvider.GetRequiredSection("databaseSettings:productdbSetting").Bind(productDbSetting);

            Console.WriteLine(productDbSetting.Password);
        }
    }
}

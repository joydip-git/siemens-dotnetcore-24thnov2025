using Siemens.DotNet.Pms.Entities;
using Siemens.DotNet.Pms.Manager;
using Siemens.DotNet.Pms.Repository;

namespace Siemens.DotNet.Pms.UserInterface
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                //string dirPath = AppContext.BaseDirectory;
                string dirPath = Directory.GetCurrentDirectory();
                string filePath = Path.Combine(dirPath, "productData.json");

                //await AddNewProductAsync(new Product { Id = 102, Name = "Lenovo ThinkPad", Price = 140000m, Description = "new laptop from Lenovo" }, filePath);
                //await AddNewProductAsync(new Product { Id = 101, Name = "HP Probook", Price = 130000m, Description = "new laptop from HP" }, filePath);

                await GetProductsAsync(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static async Task GetProductsAsync(string filePath)
        {
            try
            {
                //var fileRepository = new AsyncFileRepository<Product>("productData.json");
                var fileRepository = new AsyncFileRepository<Product>(filePath);
                var manager = new AsyncProductManager(fileRepository);
                var products = await manager.GetAllAsync();
                products?.ToList().ForEach(product => Console.WriteLine(product));
            }
            catch (Exception)
            {
                throw;
            }
        }
        static async Task AddNewProductAsync(Product p, string filePath)
        {
            try
            {
                //var fileRepository = new AsyncFileRepository<Product>("productData.json");
                var fileRepository = new AsyncFileRepository<Product>(filePath);
                var manager = new AsyncProductManager(fileRepository);
                await manager.AddAsync(p);
                Console.WriteLine("product added successfully");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

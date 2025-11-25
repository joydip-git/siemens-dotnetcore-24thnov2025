using Siemens.DotNet.Pms.Entities;
using Siemens.DotNet.Pms.Manager;
using Siemens.DotNet.Pms.Repository;

namespace Siemens.DotNet.Pms.UserInterface
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await GetProductsAsync();
        }
        static async Task GetProductsAsync()
        {
            try
            {
                var fileRepository = new AsyncFileRepository<Product>("products.json");
                var manager = new AsyncProductManager(fileRepository);
                var products = await manager.GetAllAsync();
                products?.ToList().ForEach(product => Console.WriteLine(product));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using Siemens.DotNet.Pms.Entities;
using Siemens.DotNet.Pms.Repository;

namespace Siemens.DotNet.Pms.Manager
{
    public class AsyncProductManager : IAsyncManager<Product, int>
    {
        private readonly IAsyncRepository<Product> repository;

        public AsyncProductManager(IAsyncRepository<Product> repository) => this.repository = repository;

        public Task<Product> AddAsync(Product item)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            try
            {
                var products = await repository.ReadAllAsync();
                if (products != null && products.Count > 0)
                    return [.. products];
                else
                    throw new Exception("no products found");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Product> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateAsync(Product item)
        {
            throw new NotImplementedException();
        }
    }
}

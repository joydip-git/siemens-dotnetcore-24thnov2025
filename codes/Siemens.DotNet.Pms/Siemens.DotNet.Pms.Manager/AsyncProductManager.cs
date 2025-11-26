using Siemens.DotNet.Pms.Entities;
using Siemens.DotNet.Pms.Repository;
using System.Data.SqlTypes;

namespace Siemens.DotNet.Pms.Manager
{
    public class AsyncProductManager : IAsyncManager<Product, int>
    {
        private readonly IAsyncRepository<Product> repository;

        public AsyncProductManager(IAsyncRepository<Product> repository) => this.repository = repository;

        public async Task<Product> AddAsync(Product item)
        {
            try
            {
                var products = await repository.ReadAllAsync();
                if (products != null)
                {
                    if (!Exists(item.Id, products))
                    {
                        bool status = products.Add(item);
                        if (status)
                        {
                            await repository.WriteAllAsync(products);
                            return item;
                        }
                        else
                            throw new Exception("could not add the product");
                    }
                    else
                        throw new Exception($"{nameof(Product)} with id: {item.Id} does exist");
                }
                else
                    throw new NullReferenceException("the collection to add the item was null");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            try
            {
                var products = await repository.ReadAllAsync();
                if (products != null && products.Count > 0)
                    return [.. products];
                //return products.ToList();
                else
                    throw new Exception("no products found");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product> GetAsync(int id)
        {
            try
            {
                var products = await repository.ReadAllAsync();
                if (products != null && products.Count > 0)
                {
                    if (Exists(id, products))
                    {
                        var product = products.First(p => p.Id == id);
                        return product;
                    }
                    else
                        throw new Exception($"{nameof(Product)} with id: {id} does not exist");
                }
                else
                    throw new Exception("no product records at all");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product> RemoveAsync(int id)
        {
            try
            {
                var products = await repository.ReadAllAsync();
                if (products != null && products.Count > 0)
                {
                    if (Exists(id, products))
                    {
                        var product = products.First(p => p.Id == id);
                        var status = products.Remove(product);
                        if (status)
                        {
                            await repository.WriteAllAsync(products);
                            return product;
                        }
                        else
                            throw new Exception($"product with id:{id} found, but could not be removed");
                    }
                    else
                        throw new Exception($"{nameof(Product)} with id: {id} does not exist");
                }
                else
                    throw new Exception("no product records at all");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product> UpdateAsync(Product item)
        {
            try
            {
                var products = await repository.ReadAllAsync();
                if (products != null && products.Count > 0)
                {
                    if (Exists(item.Id, products))
                    {
                        var list = products.ToList();
                        var productIndex = list.FindIndex(p => p.Id == item.Id);
                        list[productIndex] = item;
                        var set = list.ToHashSet();
                        await repository.WriteAllAsync(set);
                        return item;

                    }
                    else
                        throw new Exception($"{nameof(Product)} with id: {item.Id} does not exist");
                }
                else
                    throw new Exception("no product records at all");
            }
            catch (Exception)
            {
                throw;
            }
        }
        private bool Exists(int id, HashSet<Product> products)
        {
            bool exists = false;
            if (products.Count > 0)
                exists = products.Any(p => p.Id == id);
            return exists;
        }
    }
}

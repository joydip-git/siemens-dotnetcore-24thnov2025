using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Siemens.DotNetCore.PmsApp.DTOs;
using Siemens.DotNetCore.PmsApp.Repository;

namespace Siemens.DotNetCore.PmsApp.ServiceManager
{
    public class ProductAsyncServiceManager(SiemensDbContext _context) : IAsyncServiceManager<ProductDTO, int>
    {
        //private readonly SiemensDbContext _context;
        //public ProductAsyncServiceManager(SiemensDbContext _context)
        //{
        //    this._context = _context;
        //}

        public async Task<ProductDTO> AddAsync(ProductDTO obj)
        {
            try
            {
                var products = _context.Products;
                Product product = Mapper.Map<ProductDTO, Product>(obj);
                //EntityEntry<Product> entry = products.Add(product);
                //entry.State = EntityState.Added;
                products.Add(product);
                int result = await _context.SaveChangesAsync();
                var addedProduct = products.Last<Product>();
                return result > 0
                    ? Mapper.Map<Product, ProductDTO>(addedProduct)
                    : throw new Exception("Insert operation failed");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ProductDTO>> GetAllAsync()
        {
            try
            {
                List<ProductDTO> productDTOs = [];
                DbSet<Product> products = _context.Products;
                products
                    .ToList()
                    .ForEach(
                        p =>
                        {
                            ProductDTO dto = Mapper.Map<Product, ProductDTO>(p);
                            productDTOs.Add(dto);
                        }
                    );
                return productDTOs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<ProductDTO> GetAsync(int id)
        {
            try
            {
                var products = _context.Products;
                Product? product = products.Find(id);
                if (product != null)
                {
                    ProductDTO dto = Mapper.Map<Product, ProductDTO>(product);
                    return Task.FromResult(dto);
                }
                else
                    throw new Exception($"product with id: {id} not found");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductDTO> RemoveAsync(int id)
        {
            try
            {
                var products = _context.Products;
                Product? product = products.Find(id);
                if (product != null)
                {
                    var dto = Mapper.Map<Product, ProductDTO>(product);
                    products.Remove(product);
                    int result = await _context.SaveChangesAsync();
                    return result > 0
                        ? dto
                        : throw new Exception("Delete operation failed");
                }
                else
                    throw new Exception($"product with id: {id} not found");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductDTO> UpdateAsync(int id, ProductDTO obj)
        {
            try
            {
                var products = _context.Products;
                Product? product = products.Find(id);
                if (product != null)
                {
                    product.ProductName = obj.ProductName;
                    product.Price = obj.Price;
                    product.Description = obj.Description;
                    products.Update(product);

                    int result = await _context.SaveChangesAsync();
                    return result > 0
                        ? Mapper.Map<Product, ProductDTO>(product)
                        : throw new Exception("Update operation failed");
                }
                else
                    throw new Exception($"product with id: {id} not found");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

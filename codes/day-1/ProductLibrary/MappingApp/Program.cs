using DataEntities;
using ProductLibrary;

namespace MappingApp
{
    class Mapper
    {
        public static TTarget Map<TSource,TTarget>(TSource sourceObject)
        {
            return null;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Product product = new Product { Id = 100, Name = "abcd" };
            //ProductEntity entity = new ProductEntity { Id = product.Id, Name = product.Name };
            Mapper.Map<Product,ProductEntity>(product);
        }
    }
}

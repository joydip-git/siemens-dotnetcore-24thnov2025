using DataEntities;
using ProductLibrary;
using System.Reflection;

namespace MappingApp
{
    class Mapper
    {
        public static TTarget Map<TSource, TTarget>(TSource sourceObject) where TSource : class where TTarget : class, new()
        {
            //TTarget? target = (TTarget?)Activator.CreateInstance(typeof(TTarget));
            TTarget target = new TTarget();

            Type sourceType = sourceObject.GetType();
            Type targetType = target.GetType();

            PropertyInfo[] sourceProperties = sourceType.GetProperties();
            PropertyInfo[] targetProperties = targetType.GetProperties();

            foreach (var sourceProp in sourceProperties)
            {
                foreach (var targetProp in targetProperties)
                {
                    if (sourceProp.PropertyType == targetProp.PropertyType && sourceProp.Name == targetProp.Name)
                    {
                        targetProp.SetValue(target, sourceProp.GetValue(sourceObject));
                        break;
                    }
                }
            }

            return target;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Product product = new Product { Id = 100, Name = "abcd" };
            //ProductEntity entity = new ProductEntity() { Id = product.Id, Name = product.Name };
            ProductEntity entity = Mapper.Map<Product, ProductEntity>(product);
            Console.WriteLine(entity.GetType().Name);
            Console.WriteLine(entity.Id + " " + entity.Name);
        }
    }
}

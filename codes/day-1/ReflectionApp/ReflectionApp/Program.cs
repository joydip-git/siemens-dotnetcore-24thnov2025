using System.Reflection;

namespace ReflectionApp
{
    internal class Program
    {
        static void ExtractTypes(Assembly assembly)
        {
            try
            {
                Type[] allTypes = assembly.GetTypes();
                allTypes
                    .ToList()
                    .ForEach(
                    t => Console.WriteLine($"" +
                    $"Name={t.FullName}\n" +
                    $"IsClass? {t.IsClass}\n" +
                    $"Is Interface? {t.IsInterface}\n" +
                    $"Is Value Type? {t.IsValueType}" + Environment.NewLine));

            }
            catch (Exception)
            {
                throw;
            }
        }
        static object? CreateObject(Assembly assembly)
        {
            try
            {
                Type? productType = assembly.GetType("ProductLibrary.Product");
                Console.WriteLine($"Type Name: {productType?.Name}");
                if (productType != null)
                {
                    object? obj = Activator.CreateInstance(productType);
                    return obj;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        static void ExtractMethodInfo(Assembly assembly, object? obj)
        {
            Type? productType = assembly.GetType("ProductLibrary.Product");
            MethodInfo[]? allMethods = productType?.GetMethods();
            allMethods?.ToList().ForEach(m => Console.WriteLine($"Name: {m.Name}\nReturn Type: {m.ReturnType}" + Environment.NewLine));

            MethodInfo? printMethodInfo = productType?.GetMethod("PrintInfo");
            ParameterInfo[]? allParams = printMethodInfo?.GetParameters();
            if (obj != null)
            {
                if (allParams?.Length == 0)
                {
                    object? res = printMethodInfo?.Invoke(obj, null);
                    Console.WriteLine(res ?? "No Result");
                }
                else
                {
                    printMethodInfo?.Invoke(obj, new object[] { });
                }
            }
        }
        static void UseProperties(Assembly assembly)
        {
            Type? productType = assembly.GetType("ProductLibrary.Product");
            PropertyInfo[]? allProperties = productType?.GetProperties();
            allProperties?.ToList().ForEach(p => Console.WriteLine($"Name: {p.Name}\nProperty Type: {p.PropertyType}" + Environment.NewLine));
        }
        static void SetIdProprtyValue(Assembly assembly, object? obj)
        {
            Type? productType = assembly.GetType("ProductLibrary.Product");
            PropertyInfo? idProp = productType?.GetProperty("Id");
            if (obj != null)
            {
                idProp?.SetValue(obj, 100);
                Console.WriteLine(idProp?.GetValue(obj));
            }

        }
        static void Main(string[] args)
        {
            try
            {
                Assembly assembly = LoadAssembly();
                ExtractTypes(assembly);
                object? productObj = CreateObject(assembly);
                Console.WriteLine(productObj ?? "NA");
                SetIdProprtyValue(assembly, productObj);
                ExtractMethodInfo(assembly, productObj);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static Assembly LoadAssembly()
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(@"E:\siemens-dotnetcore-24thnov2025\codes\day-1\ProductLibrary\ProductLibrary\bin\Debug\net9.0\ProductLibrary.dll");
                Console.WriteLine(assembly.FullName);
                return assembly;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

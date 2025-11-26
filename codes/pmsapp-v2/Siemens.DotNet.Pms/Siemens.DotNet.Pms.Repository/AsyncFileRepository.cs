using Microsoft.Extensions.Options;
using Siemens.DotNet.Pms.Entities;
using System.Text.Json;

namespace Siemens.DotNet.Pms.Repository
{
    public class AsyncFileRepository<T> : IAsyncRepository<T> where T : class
    {
        private readonly string _filePath;

        public AsyncFileRepository(IOptions<FileSetting> options)
        {
            FileSetting setting = options.Value;
            _filePath = setting.FilePath ?? throw new Exception("file path was not supplied");
            Console.WriteLine("repo created...");
        }

        public async Task<HashSet<T>?> ReadAllAsync()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    HashSet<T>? data = null;
                    using (Stream stream = File.OpenRead(_filePath))
                    {
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        data = await JsonSerializer.DeserializeAsync<HashSet<T>>(stream, options);
                    }
                    return data;
                }
                else
                    throw new FileNotFoundException($"{_filePath} doesn't exist");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task WriteAllAsync(HashSet<T> set)
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    using (var stream = File.OpenWrite(_filePath))
                    {
                        var options = new JsonSerializerOptions { WriteIndented = true };
                        await JsonSerializer.SerializeAsync(stream, set, options);
                    }
                }
                else
                    throw new FileNotFoundException($"{_filePath} doesn't exist");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

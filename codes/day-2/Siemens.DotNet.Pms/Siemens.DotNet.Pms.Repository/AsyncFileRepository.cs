using System.Text.Json;

namespace Siemens.DotNet.Pms.Repository
{
    public class AsyncFileRepository<T> : IAsyncRepository<T> where T : class
    {
        private readonly string _filePath;

        public AsyncFileRepository(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<HashSet<T>?> ReadAllAsync()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    using Stream stream = File.OpenRead(_filePath);
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var data = await JsonSerializer.DeserializeAsync<HashSet<T>>(stream, options);
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
                    using var stream = File.OpenWrite(_filePath);
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    await JsonSerializer.SerializeAsync<HashSet<T>>(stream, set, options);
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

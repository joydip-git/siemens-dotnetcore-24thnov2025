namespace Siemens.DotNet.Pms.Repository
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<HashSet<T>?> ReadAllAsync();
        Task WriteAllAsync(HashSet<T> set);
    }
}

namespace Siemens.DotNetCore.PmsApp.ServiceManager
{
    public interface IAsyncServiceManager<T, TId> where T : class
    {
        Task<T> AddAsync(T obj);
        Task<T> RemoveAsync(TId id);
        Task<T> UpdateAsync(TId id, T obj);
        Task<T> GetAsync(TId id);
        Task<List<T>> GetAllAsync();
    }
}

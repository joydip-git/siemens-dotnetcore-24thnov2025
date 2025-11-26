namespace Siemens.DotNet.Pms.Manager
{
    public interface IAsyncManager<T, TId> where T : class
    {
        Task<T> GetAsync(TId id);
        Task<IReadOnlyList<T>> GetAllAsync(int sortChoice = 1);
        Task<T> AddAsync(T item);
        Task<T> RemoveAsync(TId id);
        Task<T> UpdateAsync(T item);
    }
}

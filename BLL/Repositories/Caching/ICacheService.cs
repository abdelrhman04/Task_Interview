namespace BLL;
public interface ICacheService
{
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken =default)
        where T : class;
    Task<T?> GetAsync<T>(string key,Func<Task<T>>factory ,CancellationToken cancellationToken = default)
        where T : class;
    Task SetAsync<T>(string key,T Value, int time = 3)
        where T : class;
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    Task RemoveByPrefixAsync(string Prefixkey, CancellationToken cancellationToken = default);
}


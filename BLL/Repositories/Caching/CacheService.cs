using BLL.Shared;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Concurrent;
namespace BLL;
public class CacheService : ICacheService
{
    private readonly IDistributedCache distributedCache;
    private static readonly ConcurrentDictionary<string, bool> CacheKey = new();
    public CacheService(IDistributedCache distributedCache)
    {
        this.distributedCache = distributedCache;
    }
    //Chack if caching or not using enum keycaching using switch
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        string? cachevalue = await distributedCache.GetStringAsync(key, cancellationToken);
        if (cachevalue == null)
        {
            return default(T?);
        }
        T? value =(T) JsonConvert.DeserializeObject<T>(cachevalue);
        return value;
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await distributedCache.RemoveAsync(key, cancellationToken);
        //using out because it return true if found and remove item and false if not found item

        CacheKey.TryRemove(key, out bool _);
    }

    public async Task RemoveByPrefixAsync(string Prefixkey, CancellationToken cancellationToken = default)
    {
        //foreach(var key in CacheKey.Keys)
        // {
        //     if (key.StartsWith(Prefixkey))
        //     {
        //         await RemoveAsync(key, cancellationToken);
        //     }
        // }
        if (Prefixkey==KeyCaching.None.ToString())
        {
            return;
        }
        IEnumerable<Task> tasks = CacheKey.Keys.Where(k => k.StartsWith(Prefixkey))
             .Select(k => RemoveAsync(k, cancellationToken));
        await Task.WhenAll(tasks);
    }

    public async Task SetAsync<T>(string key, T Value,int time=3 ) where T : class
    {
        if (key == KeyCaching.None.ToString())
        {
            return;
        }
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
          //  NullValueHandling = NullValueHandling.Include

        };
        var cacheEntryOptions = new DistributedCacheEntryOptions()
    .SetSlidingExpiration(TimeSpan.FromMinutes(time));
        string cachevalue = JsonConvert.SerializeObject(Value, settings);
        await distributedCache.SetStringAsync(key, cachevalue, cacheEntryOptions, new CancellationToken());
        CacheKey.TryAdd(key, false);
    }

    public async Task<T?> GetAsync<T>(string key, Func<Task<T>> factory, CancellationToken cancellationToken = default) where T : class
    {
        if (key == KeyCaching.None.ToString())
        {
            return await factory();
        }
        T? cachevalue = await GetAsync<T>(key, cancellationToken);
        if (cachevalue != null)
        {
            return cachevalue;
        }
        T value =await factory();
        await SetAsync(key, value );   
        return value;
    }
}


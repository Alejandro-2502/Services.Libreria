using Microsoft.Extensions.Caching.Distributed;

namespace Libreria.Application.Interfaces.Redis;

public interface IDistributedRedisCacheInteractor
{
    Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options);
    Task<T?> GetOrSetAsync<T>(string keyCache, Func<Task<T>> factory, Caches cache);

}

using Libreria.Application.Configuration;
using Libreria.Application.Interfaces.ICommon;
using Libreria.Application.Interfaces.Redis;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace Libreria.Application.Interactors.Redis;

public class DistributedRedisCacheInteractor : IDistributedRedisCacheInteractor
{
    private readonly IDistributedCache _distributedCache;
    private readonly ILogServicesInteractor _logServicesInteractor;

    public DistributedRedisCacheInteractor(IDistributedCache distributedCache, ILogServicesInteractor logServicesInteractor)
    {
        _distributedCache = distributedCache;
        _logServicesInteractor = logServicesInteractor;
    }

    public async Task<T?> GetOrSetAsync<T>(string keyCache, Func<Task<T>> factory, Caches cache)
    {
        try
        {
            if (TryGetValue(keyCache, out T? cacheValue) && cacheValue is not null)
                return cacheValue;

            var newValue = await factory();

            if (newValue is not null)
            {
                var options = GetCacheEntryOptions(cache);
                await SetAsync(keyCache, newValue, options);
            }

            return newValue;
        }
        catch (Exception ex)
        {
            _logServicesInteractor.LogError(nameof(DistributedRedisCacheInteractor) + nameof(GetOrSetAsync) + ex.Message);
            throw;
        }
    }

    public async Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options)
    {
        try
        {
            var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value));
            await _distributedCache.SetAsync(key, bytes, options);
        }
        catch (Exception ex)
        {
            _logServicesInteractor.LogError(nameof(DistributedRedisCacheInteractor) + nameof(GetOrSetAsync) + ex.Message);
            throw;
        }
    }

    private bool TryGetValue<T>(string key, out T? value)
    {
        try
        {
            var cacheKey = _distributedCache.Get(key);
            value = cacheKey is null ? default : JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(cacheKey));
            return cacheKey is not null;
        }
        catch (Exception ex)
        {
            _logServicesInteractor.LogError(nameof(DistributedRedisCacheInteractor) + nameof(TryGetValue) + ex.Message);
            throw;
        }
    }

    private DistributedCacheEntryOptions GetCacheEntryOptions(Caches cache)
    {
        try
        {
            DistributedCacheEntryOptions option = new DistributedCacheEntryOptions();

            switch (cache)
            {
                case Caches.Libros:
                    option = new DistributedCacheEntryOptions()
                                 .SetAbsoluteExpiration(DateTime.Now.AddMinutes(ConfigHelper.TTLCaches!.TTLLibroAbsoluteExpire))
                                 .SetSlidingExpiration(TimeSpan.FromMinutes(ConfigHelper.TTLCaches.TTLLibroSlidingExpire));
                    break;
                default:
                    break;
            }
            return option;
        }
        catch (Exception ex)
        {
            _logServicesInteractor.LogError(nameof(DistributedRedisCacheInteractor) + nameof(GetCacheEntryOptions) + ex.Message);
            throw;
        }
    }
}

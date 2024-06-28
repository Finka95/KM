using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using Tinder.BLL.Interfaces;

namespace Tinder.BLL.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T> GetAsync<T>(string key, CancellationToken cancellationToken)
        {
            var jsonStringValue = await _distributedCache.GetStringAsync(key, cancellationToken);
            if (jsonStringValue == null)
            {
                return default;
            }
            var model = JsonSerializer.Deserialize<T>(jsonStringValue);
            return model;
        }

        public Task SetAsync<T>(string key, T value, CancellationToken cancellationToken)
        {
            var jsonStringValue = JsonSerializer.Serialize(value);
            return _distributedCache.SetStringAsync(key, jsonStringValue, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
            }, cancellationToken);
        }

        public Task RemoveAsync(string key, CancellationToken cancellationToken)
        {
            return _distributedCache.RemoveAsync(key, cancellationToken);
        }
    }
}

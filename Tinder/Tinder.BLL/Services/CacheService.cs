﻿using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Tinder.BLL.Interfaces;

namespace Tinder.BLL.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IMapper _mapper;

        public CacheService(IDistributedCache distributedCache, IMapper mapper)
        {
            _distributedCache = distributedCache;
            _mapper = mapper;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var jsonStringValue = await _distributedCache.GetStringAsync(key, default);
            var model = JsonSerializer.Deserialize<T>(jsonStringValue);
            return model;
        }

        public Task SetAsync<T>(string key, T value)
        {
            var jsonStringValue = JsonSerializer.Serialize(value);
            return _distributedCache.SetStringAsync(key, jsonStringValue, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
            });
        }

        public Task RemoveAsync(string key)
        {
           return _distributedCache.RemoveAsync(key, default);
        }
    }
}

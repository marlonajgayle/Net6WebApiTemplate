using Microsoft.Extensions.Caching.Memory;
using Net6WebApiTemplate.Application.Common.Interfaces;

namespace Net6WebApiTemplate.Infrastructure.Cache.InMemory
{
    public class CacheProvider : ICacheProvider
    {
        private readonly IMemoryCache _memoryCache;

        public CacheProvider(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void ClearCache(string key)
        {
            _memoryCache.Remove(key);
        }

        public T GetFromCache<T>(string key) where T : class
        {
            _memoryCache.TryGetValue(key, out T cachedResponse);

            return cachedResponse as T;
        }

        public void SetCache<T>(string key, T value, DateTimeOffset duration) where T : class
        {
            _memoryCache.Set(key, value, duration);
        }

        public void SetCache<T>(string key, T value, MemoryCacheEntryOptions options) where T : class
        {
            _memoryCache.Set(key, value, options);
        }
    }
}
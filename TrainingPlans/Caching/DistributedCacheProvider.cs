using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using TrainingPlans.Common;

namespace TrainingPlans.Caching
{
    public class DistributedCacheProvider<T> : ICacheProvider<T>
    {
        private readonly IDistributedCache _cache;
        public DistributedCacheProvider(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetOrAdd(string key, Func<T> valueRetriever, TimeSpan? slidingExpiration = null, TimeSpan? absoluteExpiration = null) 
        {
            var existing = await _cache.GetAsync(key);
            if (existing != null)
                return await existing.FromByteArray<T>();
            var options = new DistributedCacheEntryOptions
            {
                SlidingExpiration = slidingExpiration.HasValue ? slidingExpiration : TimeSpan.FromMinutes(10),
                AbsoluteExpirationRelativeToNow = absoluteExpiration.HasValue ? absoluteExpiration : TimeSpan.FromMinutes(60)
            };
            var value = valueRetriever();
            await _cache.SetAsync(key, await value.ToByteArray(), options);
            return value;
        }

        public async Task TryRemove(string key)
        {
            if (await _cache.GetAsync(key) is null)
                return;
            await _cache.RemoveAsync(key);
        }

        public async Task<T> Get(string key)
        {
            var result = await _cache.GetAsync(key);
            return await result.FromByteArray<T>();
        }

        public async Task AddOrUpdate(string key, T value, TimeSpan? slidingExpiration = null, TimeSpan? absoluteExpiration = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                SlidingExpiration = slidingExpiration.HasValue ? slidingExpiration : TimeSpan.FromMinutes(10),
                AbsoluteExpirationRelativeToNow = absoluteExpiration.HasValue ? absoluteExpiration : TimeSpan.FromMinutes(60)
            };
            await _cache.SetAsync(key, await value.ToByteArray(), options);
        }
    }
}

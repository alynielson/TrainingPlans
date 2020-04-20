using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using TrainingPlans.Common;

namespace TrainingPlans.Caching
{
    public static class DistributedCacheExtensions
    {
        public static async Task<T> GetOrAdd<T>(this IDistributedCache cache, string key, 
            Func<Task<T>> valueRetriever, TimeSpan? slidingExpiration = null, TimeSpan? absoluteExpiration = null) 
        {
            var existing = await cache.GetAsync(key);
            if (existing != null)
                return await existing.FromByteArray<T>();
            var options = new DistributedCacheEntryOptions
            {
                SlidingExpiration = slidingExpiration.HasValue ? slidingExpiration : TimeSpan.FromMinutes(10),
                AbsoluteExpirationRelativeToNow = absoluteExpiration.HasValue ? absoluteExpiration : TimeSpan.FromMinutes(60)
            };
            var value = await valueRetriever();
            await cache.SetAsync(key, await value.ToByteArray(), options);
            return value;
        }

        public static async Task TryRemove<T>(this IDistributedCache cache, string key)
        {
            if (await cache.GetAsync(key) is null)
                return;
            await cache.RemoveAsync(key);
        }

        public static async Task<T> Get<T>(this IDistributedCache cache, string key)
        {
            var result = await cache.GetAsync(key);
            return await result.FromByteArray<T>();
        }

        public static async Task AddOrUpdate<T>(this IDistributedCache cache, string key, T value, 
            TimeSpan? slidingExpiration = null, TimeSpan? absoluteExpiration = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                SlidingExpiration = slidingExpiration.HasValue ? slidingExpiration : TimeSpan.FromMinutes(10),
                AbsoluteExpirationRelativeToNow = absoluteExpiration.HasValue ? absoluteExpiration : TimeSpan.FromMinutes(60)
            };
            await cache.SetAsync(key, await value.ToByteArray(), options);
        }
    }
}

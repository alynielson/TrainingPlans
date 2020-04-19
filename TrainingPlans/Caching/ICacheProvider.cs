using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingPlans.Caching
{
    public interface ICacheProvider<T>
    {
        Task<T> GetOrAdd(string key, Func<T> valueRetriever, TimeSpan? slidingExpiration = null, TimeSpan? absoluteExpiration = null);
        Task TryRemove(string key);
        Task<T> Get(string key);
        Task AddOrUpdate(string key, T value, TimeSpan? slidingExpiration = null, TimeSpan? absoluteExpiration = null);
    }
}

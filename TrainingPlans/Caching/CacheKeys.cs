using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.Models;

namespace TrainingPlans.Caching
{
    public static class CacheKeys
    {
        public static string UserCacheEntry(this int userId)
        {
            return $"User:{userId}";
        }
    }
}

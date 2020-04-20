using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.Models;
using TrainingPlans.ExceptionHandling;
using TrainingPlans.Repositories;
using TrainingPlans.Caching;

namespace TrainingPlans.Services
{
    public static class Extensions
    {
        public static async Task<User> FindUser(int userId, IUserRepository userRepository, IDistributedCache cache)
        {
            // TODO don't save user details here when those get added. Only user defaults.
            return await cache.GetOrAdd(userId.UserCacheEntry(), async () =>
            {
                var user = await userRepository.Get(userId);
                if (user is null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound, "User not found.");
                }
                return user;
            });
        }
    }
}

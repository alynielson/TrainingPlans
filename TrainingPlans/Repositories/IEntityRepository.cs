using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.Models;

namespace TrainingPlans.Repositories
{
    public interface IEntityRepository<T> where T : class
    {
        public Task<int> Create(T entity);
        public Task<T> Get(int id);
    }
}

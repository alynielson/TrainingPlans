using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database;

namespace TrainingPlans.Repositories
{
    public class EntityRepositoryBase<T> : IEntityRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;
        public EntityRepositoryBase(TrainingPlanDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async virtual Task<int> Create(T entity)
        {
            _dbContext.Add(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async virtual Task<T> Get(int id)
        {
            return await _dbContext.FindAsync<T>(id);
        }
    }
}

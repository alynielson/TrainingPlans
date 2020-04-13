using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database;
using TrainingPlans.ExceptionHandling;

namespace TrainingPlans.Repositories
{
    public abstract class AbstractEntityRepositoryBase<T> : IEntityRepository<T> where T : class
    {
        protected readonly TrainingPlanDbContext _dbContext;
        public AbstractEntityRepositoryBase(TrainingPlanDbContext dbContext)
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

        public async virtual Task<int?> Delete(int id)
        {
            var entity = await Get(id);
            if (entity is null)
            {
                return null;
            }
            _dbContext.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async virtual Task<int> Update(T entity)
        {
            _dbContext.Update(entity);
            return await _dbContext.SaveChangesAsync();
        }
    }
}

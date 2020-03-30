using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingPlans.Repositories
{
    public interface IEntityRepository<T> where T : class
    {
        Task<int> Create(T entity);


        Task<T> Get(int id);

        Task<IReadOnlyList<T>> GetAll();
    }
}

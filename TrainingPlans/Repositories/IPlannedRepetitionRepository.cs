using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.Models;

namespace TrainingPlans.Repositories
{
    public interface IPlannedRepetitionRepository : IEntityRepository<PlannedRepetition>
    {
        Task<int?> Delete(int repetitionId, int workoutId, int userId);
        Task<PlannedRepetition> Get(int repetitionId, int workoutId, int userId);
    }
}

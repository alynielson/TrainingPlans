using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.Models;

namespace TrainingPlans.Repositories
{
    public interface ICompletedWorkoutRepository : IEntityRepository<CompletedWorkout>
    {
        Task<int?> Delete(int workoutId, int userId);
        Task<IReadOnlyList<CompletedWorkout>> FindByDateRange(int userId, DateTime from, DateTime to, bool track = true);
        Task<IReadOnlyList<CompletedWorkout>> GetAll(int userId, bool track = true);
    }
}

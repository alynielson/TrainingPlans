using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.Models;

namespace TrainingPlans.Repositories
{
    public interface IPlannedWorkoutRepository : IEntityRepository<PlannedWorkout>
    {
        Task<IReadOnlyList<PlannedWorkout>> FindByDateRange(int userId, DateTime from, DateTime to);
        Task<IReadOnlyList<PlannedWorkout>> GetAll(int userId);
        Task<PlannedWorkout> GetSingle(int userId, int workoutId);
    }
}

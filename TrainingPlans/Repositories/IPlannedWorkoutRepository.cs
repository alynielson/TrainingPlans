using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.Database.Models;

namespace TrainingPlans.Repositories
{
    public interface IPlannedWorkoutRepository : IEntityRepository<PlannedWorkout>
    {
        Task<IReadOnlyList<PlannedWorkout>> FindByDateRange(int userId, DateTime from, DateTime to, bool track = true);
        Task<int?> Delete(int workoutId, int userId);
        Task<IReadOnlyList<PlannedWorkout>> GetAllMatchingTimeSpan(DateTime day, TimeOfDay timeOfDay, int userId, bool track = true);
        Task<IReadOnlyList<PlannedWorkout>> GetAll(IReadOnlyList<int> ids, int userId, bool track = true);
    }
}

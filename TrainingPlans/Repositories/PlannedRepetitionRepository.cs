using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database;
using TrainingPlans.Database.Models;

namespace TrainingPlans.Repositories
{
    public class PlannedRepetitionRepository : AbstractEntityRepositoryBase<PlannedRepetition>, IPlannedRepetitionRepository
    {
        public PlannedRepetitionRepository(TrainingPlanDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int?> Delete(int repetitionId, int workoutId, int userId)
        {
            var repetition = await Get(repetitionId);
            if (repetition is null || repetition.PlannedWorkoutId != workoutId || repetition.UserId != userId)
                return null;
            _dbContext.Remove(repetition);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<PlannedRepetition> Get(int repetitionId, int workoutId, int userId, bool track = true)
        {
            var repetition = track ? await Get(repetitionId) : await GetNoTracking(repetitionId);
            if (repetition is null || repetition.PlannedWorkoutId != workoutId || repetition.UserId != userId)
                return null;
            return repetition;
        }

        public int GetNumberOfRepsInWorkout(int workoutId)
        {
            return _dbContext.PlannedRepetition.Count(x => x.PlannedWorkoutId == workoutId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database;
using TrainingPlans.Database.Models;

namespace TrainingPlans.Repositories
{
    public class CompletedRepetitionRepository : AbstractEntityRepositoryBase<CompletedRepetition>, ICompletedRepetitionRepository
    {
        public CompletedRepetitionRepository(TrainingPlanDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<int?> Delete(int repetitionId, int workoutId, int userId)
        {
            var repetition = await Get(repetitionId);
            if (repetition is null || repetition.CompletedWorkoutId != workoutId || repetition.UserId != userId)
                return null;
            _dbContext.Remove(repetition);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<CompletedRepetition> Get(int repetitionId, int workoutId, int userId)
        {
            var repetition = await Get(repetitionId);
            if (repetition is null || repetition.CompletedWorkoutId != workoutId || repetition.UserId != userId)
                return null;
            return repetition;
        }

        public int GetNumberOfRepsInWorkout(int workoutId)
        {
            return _dbContext.CompletedRepetition.Count(x => x.CompletedWorkoutId == workoutId);
        }
    }
}

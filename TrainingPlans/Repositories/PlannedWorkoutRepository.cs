using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database;
using TrainingPlans.Database.Models;

namespace TrainingPlans.Repositories
{
    public class PlannedWorkoutRepository : AbstractEntityRepositoryBase<PlannedWorkout>, IPlannedWorkoutRepository
    {
        public PlannedWorkoutRepository(TrainingPlanDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<PlannedWorkout>> GetAll(int userId)
        {
            return await _dbContext.PlannedWorkout.Where(x => x.UserId == userId).Include(x => x.PlannedRepetitions).ToListAsync();
        }

        public async Task<PlannedWorkout> GetSingle(int userId, int workoutId)
        {
            var result = await Get(workoutId);
            if (result?.UserId != userId)
                return null;
            return result;
        }

        public async Task<IReadOnlyList<PlannedWorkout>> FindByDateRange(int userId, DateTime from, DateTime to)
        {
            return (await GetAll(userId))
                .Where(x => x.ScheduledDate >= from && x.ScheduledDate <= to)
                .ToList();
        }
    }
}

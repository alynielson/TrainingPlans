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
            return (await GetAll()).Where(x => x.UserId == userId).ToList();
        }

        public async Task<IReadOnlyList<PlannedWorkout>> FindByDateRange(int userId, DateTime from, DateTime to)
        {
            return (await GetAll(userId))
                .Where(x => x.ScheduledDate >= from && x.ScheduledDate <= to)
                .ToList();
        }
    }
}

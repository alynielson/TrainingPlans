using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database;
using TrainingPlans.Database.Models;

namespace TrainingPlans.Repositories
{
    public class CompletedWorkoutRepository : AbstractEntityRepositoryBase<CompletedWorkout>, ICompletedWorkoutRepository
    {
        public CompletedWorkoutRepository(TrainingPlanDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<CompletedWorkout> Get(int workoutId)
        {
            return await _dbContext.CompletedWorkout.Include(x => x.CompletedRepetitions).FirstOrDefaultAsync(x => x.Id == workoutId);
        }

        public override async Task<CompletedWorkout> GetNoTracking(int workoutId)
        {
            return await _dbContext.CompletedWorkout.AsNoTracking().Include(x => x.CompletedRepetitions).FirstOrDefaultAsync(x => x.Id == workoutId);
        }

        public async Task<int?> Delete(int workoutId, int userId)
        {
            var workout = await base.Get(workoutId);
            if (workout is null || workout.UserId != userId)
                return null;
            _dbContext.Remove(workout);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<CompletedWorkout>> GetAll(int userId, bool track = true)
        {
            if (!track)
                _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _dbContext.CompletedWorkout.Where(x => x.UserId == userId).Include(x => x.CompletedRepetitions).ToListAsync();
        }

        public async Task<IReadOnlyList<CompletedWorkout>> FindByDateRange(int userId, DateTime from, DateTime to, bool track = true)
        {
            return (await GetAll(userId, track))
                .Where(x => x.CompletedDateTime >= from && x.CompletedDateTime <= to)
                .ToList();
        }
    }
}

﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.Database.Models;

namespace TrainingPlans.Repositories
{
    public class PlannedWorkoutRepository : AbstractEntityRepositoryBase<PlannedWorkout>, IPlannedWorkoutRepository
    {
        public PlannedWorkoutRepository(TrainingPlanDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<PlannedWorkout> Get(int workoutId)
        {
            return await _dbContext.PlannedWorkout.Include(x => x.PlannedRepetitions).FirstOrDefaultAsync(x => x.Id == workoutId);
        }

        public override async Task<PlannedWorkout> GetNoTracking(int workoutId)
        {
            return await _dbContext.PlannedWorkout.AsNoTracking().Include(x => x.PlannedRepetitions).FirstOrDefaultAsync(x => x.Id == workoutId);
        }

        public async Task<IReadOnlyList<PlannedWorkout>> FindByDateRange(int userId, DateTime from, DateTime to, bool track = true)
        {
            if (!track)
                _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _dbContext.PlannedWorkout
                .Where(x => x.ScheduledDate >= from && x.ScheduledDate <= to && x.UserId == userId)
                .Include(x => x.PlannedRepetitions)
                .ToListAsync();
        }

        public async Task<int?> Delete(int workoutId, int userId)
        {
            var workout = await base.Get(workoutId);
            if (workout is null || workout.UserId != userId)
                return null;
            _dbContext.Remove(workout);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<PlannedWorkout>> GetAll(IReadOnlyList<int> ids, int userId, bool track = true)
        {
            if (!track)
                _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _dbContext.PlannedWorkout.Where(x => ids.Contains(x.Id) && x.UserId == userId).ToListAsync();
        }

        public async Task<IReadOnlyList<PlannedWorkout>> GetAllMatchingTimeSpan(DateTime day, TimeOfDay timeOfDay, int userId, bool track = true)
        {
            if (!track)
                _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _dbContext.PlannedWorkout
                .Where(x => x.ScheduledDate >= day && x.ScheduledDate < day.AddDays(1) && x.TimeOfDay == timeOfDay && x.UserId == userId)
                .Include(x => x.PlannedRepetitions).ToListAsync();
        }
    }
}

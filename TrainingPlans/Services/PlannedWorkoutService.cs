using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Common;
using TrainingPlans.Database.Models;
using TrainingPlans.Repositories;
using TrainingPlans.ViewModels;
using TrainingPlans.ExceptionHandling;
using TrainingPlans.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace TrainingPlans.Services
{
    public class PlannedWorkoutService : IPlannedWorkoutService
    {
        private readonly IPlannedWorkoutRepository _plannedWorkoutRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDistributedCache _cache;

        public PlannedWorkoutService(IPlannedWorkoutRepository plannedWorkoutRepository, IUserRepository userRepository, IDistributedCache cache)
        {
            _plannedWorkoutRepository = plannedWorkoutRepository;
            _userRepository = userRepository;
            _cache = cache;
        }

        public async Task<bool> Create(PlannedWorkoutVM workout, int userId)
        {
            await Extensions.FindUser(userId, _userRepository, _cache);
            await SetValidOrder(workout, userId);
            var model = new PlannedWorkout(workout, userId, 0);
            var entriesSaved = await _plannedWorkoutRepository.Create(model);
            return entriesSaved == (model.PlannedRepetitions.Count + 1);
        }

        public async Task<IReadOnlyList<PlannedWorkoutVM>> GetInDateRange(string from, string to, int userId, bool includeReps)
        {
            var fromDate = from.ValidateDate();
            var toDate = to.ValidateDate();


            var user = await Extensions.FindUser(userId, _userRepository, _cache);
            var plan = await _plannedWorkoutRepository.FindByDateRange(userId, fromDate, toDate, false);

            var userDefaults = user.GetUserDefaultsFormatted();

            return plan?.OrderBy(x => x.ScheduledDate).Select(x => new PlannedWorkoutVM(x, userDefaults[x.ActivityType], includeReps)).ToList();
        }

        public async Task<PlannedWorkoutVM> GetSingle(int userId, int workoutId, bool includeReps)
        {
            var user = await Extensions.FindUser(userId, _userRepository, _cache);
            var workout = await _plannedWorkoutRepository.GetNoTracking(workoutId);
            if (workout is null || userId != workout.UserId)
                return null;
            var defaults = user.GetUserDefaultsForActivity(workout.ActivityType);

            return new PlannedWorkoutVM(workout, defaults, includeReps);
        }

        public async Task<bool?> DeleteWorkout(int userId, int workoutId)
        {
            var entriesDeleted = await _plannedWorkoutRepository.Delete(workoutId, userId);
            if (entriesDeleted is null)
                return null;
            return entriesDeleted > 0;
        }

        public async Task<bool?> UpdateWorkout(int userId, int workoutId, PlannedWorkoutVM updatedWorkout)
        {
            await Extensions.FindUser(userId, _userRepository, _cache);
            var workout = await _plannedWorkoutRepository.Get(workoutId);
            if (workout is null || userId != workout.UserId)
                return null;

            if (workout.DatesAreEdited(updatedWorkout))
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Date information cannot be changed in this request.");

            workout.UpdateFromVM(updatedWorkout);
            return (await _plannedWorkoutRepository.Update(workout)) > 0;
        }

        public async Task<bool> ChangeWorkoutOrders(PlannedWorkoutDateUpdate dateUpdate, int userId)
        {
            await Extensions.FindUser(userId, _userRepository, _cache);
            var existing = await _plannedWorkoutRepository.GetAll(dateUpdate.WorkoutOrders.Select(x => x.WorkoutId).ToList(), userId);
            if (existing.Count != dateUpdate.WorkoutOrders.Count)
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Invalid workoutIds.");

            var existingInTimeSpan = await _plannedWorkoutRepository.GetAllMatchingTimeSpan(DateTime.Parse(dateUpdate.ScheduledDate), dateUpdate.TimeOfDay, userId);
            if (existingInTimeSpan.Any(x => !existing.Contains(x)))
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Does not contain all workouts in this time span.");

            var toUpdate = existing.Select(x =>
            {
                x.UpdateDateValues(dateUpdate);
                return x;
            }).ToList();
            return (await _plannedWorkoutRepository.UpdateMany(toUpdate) == toUpdate.Count);
        }

        private async Task SetValidOrder(PlannedWorkoutVM newWorkout, int userId)
        {
            var timeOfDay = newWorkout.TimeOfDay.HasValue ? newWorkout.TimeOfDay.Value : Database.AdditionalData.TimeOfDay.Any;
            var existing = await _plannedWorkoutRepository.GetAllMatchingTimeSpan(DateTime.Parse(newWorkout.ScheduledDate), timeOfDay, userId);
            if (existing is null || existing.Count == 0)
                return;

            var orders = existing.Select(x => x.Order).ToList();
            orders.Add(newWorkout.Order);

            if (orders.IsDistinctOrder())
                return;

            newWorkout.Order = orders.Max() + 1;
        }
    }
}

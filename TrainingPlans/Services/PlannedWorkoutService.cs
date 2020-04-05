using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Common;
using TrainingPlans.Database.Models;
using TrainingPlans.Repositories;
using TrainingPlans.ViewModels;
using TrainingPlans.ExceptionHandling;

namespace TrainingPlans.Services
{
    public class PlannedWorkoutService : IPlannedWorkoutService
    {
        private readonly IPlannedWorkoutRepository _plannedWorkoutRepository;
        private readonly IUserRepository _userRepository;

        public PlannedWorkoutService(IPlannedWorkoutRepository plannedWorkoutRepository, IUserRepository userRepository)
        {
            _plannedWorkoutRepository = plannedWorkoutRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> Create(PlannedWorkoutVM workout, int userId)
        {
            await Extensions.FindUser(userId, _userRepository);
            var model = new PlannedWorkout(workout, userId);
            var entriesSaved = await _plannedWorkoutRepository.Create(model);
            return entriesSaved == (model.PlannedRepetitions.Count + 1);
        }

        public async Task<IReadOnlyList<PlannedWorkoutVM>> GetInDateRange(string from, string to, int userId, bool includeReps)
        {
            var user = await Extensions.FindUser(userId, _userRepository);
            var plan = await _plannedWorkoutRepository.FindByDateRange(userId, from.ValidateDate(), to.ValidateDate());

            var userDefaults = user.GetUserDefaultsFormatted();

            return plan?.OrderBy(x => x.ScheduledDate).Select(x => new PlannedWorkoutVM(x, userDefaults[x.ActivityType], includeReps)).ToList();
        }

        public async Task<PlannedWorkoutVM> GetSingle(int userId, int workoutId, bool includeReps)
        {
            var user = await Extensions.FindUser(userId, _userRepository);
            var workout = await _plannedWorkoutRepository.Get(workoutId);
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
    }
}

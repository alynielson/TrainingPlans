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
            await FindUser(userId);
            var model = new PlannedWorkout(workout);
            model.UserId = userId;
            var entriesSaved = await _plannedWorkoutRepository.Create(model);
            return entriesSaved == (model.PlannedRepetitions.Count + 1);
        }

        public async Task<IReadOnlyList<PlannedWorkoutVM>> GetInDateRange(string from, string to, int userId, bool includeReps)
        {
            var user = await FindUser(userId);
            var plan = await _plannedWorkoutRepository.FindByDateRange(userId, from.ValidateDate(), to.ValidateDate());

            var userDefaults = user.GetUserDefaultsFormatted();

            return plan?.OrderBy(x => x.ScheduledDate).Select(x => new PlannedWorkoutVM(x, userDefaults[x.ActivityType], includeReps)).ToList();
        }

        public async Task<PlannedWorkoutVM> GetSingle(int userId, int workoutId, bool includeReps)
        {
            var user = await FindUser(userId);
            var workout = await _plannedWorkoutRepository.GetSingle(userId, workoutId);
            if (workout is null)
                return null;
            var defaults = user.GetUserDefaultsForActivity(workout.ActivityType);

            return new PlannedWorkoutVM(workout, defaults, includeReps);
        }

        public async Task<bool?> DeleteWorkout(int userId, int workoutId)
        {
            await FindUser(userId);
            var entriesDeleted = await _plannedWorkoutRepository.Delete(workoutId);
            if (entriesDeleted is null)
                return null;
            return entriesDeleted > 0;
        }

        private async Task<User> FindUser(int userId)
        {
            var user = await _userRepository.Get(userId);
            if (user is null)
            {
                throw new RestException(System.Net.HttpStatusCode.NotFound, "User not found.");
            }
            return user;
        }
    }
}

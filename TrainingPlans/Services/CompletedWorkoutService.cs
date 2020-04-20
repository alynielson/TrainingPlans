using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Common;
using TrainingPlans.Database.Models;
using TrainingPlans.ExceptionHandling;
using TrainingPlans.Repositories;
using TrainingPlans.ViewModels;

namespace TrainingPlans.Services
{
    public class CompletedWorkoutService : ICompletedWorkoutService
    {
        private readonly ICompletedWorkoutRepository _completedWorkoutRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlannedWorkoutRepository _plannedWorkoutRepository;
        private readonly IDistributedCache _cache;

        public CompletedWorkoutService(ICompletedWorkoutRepository CompletedWorkoutRepository, IUserRepository userRepository,
            IPlannedWorkoutRepository plannedWorkoutRepository, IDistributedCache cache)
        {
            _completedWorkoutRepository = CompletedWorkoutRepository;
            _userRepository = userRepository;
            _plannedWorkoutRepository = plannedWorkoutRepository;
            _cache = cache;
        }

        public async Task<bool> CompleteUnplannedWorkout(int userId, CompletedWorkoutVM viewModel)
        {
            await Extensions.FindUser(userId, _userRepository, _cache);
            viewModel.PlannedWorkoutId = null;
            var model = new CompletedWorkout(viewModel, userId, 0);
            var entriesSaved = await _completedWorkoutRepository.Create(model);
            return entriesSaved == (model.CompletedRepetitions.Count + 1);
        }

        public async Task<bool> CompletePlannedWorkout(int userId, CompletedWorkoutVM viewModel)
        {
            await Extensions.FindUser(userId, _userRepository, _cache);
            await VerifyReferencedPlannedWorkout(viewModel, userId, true);

            var model = new CompletedWorkout(viewModel, userId, 0);
            var entriesSaved = await _completedWorkoutRepository.Create(model);
            return entriesSaved == (model.CompletedRepetitions.Count + 1);
        }

        public async Task<bool?> UpdateCompletedWorkout(int userId, int workoutId, CompletedWorkoutVM viewModel)
        {
            await Extensions.FindUser(userId, _userRepository, _cache);
            var workout = await _completedWorkoutRepository.Get(workoutId);
            if (workout is null || userId != workout.UserId)
                return null;

            await VerifyReferencedPlannedWorkout(viewModel, userId, false);
            workout.UpdateFromVM(viewModel);
            return (await _completedWorkoutRepository.Update(workout)) > 0;
        }
        public async Task<bool?> DeleteCompletedWorkout(int workoutId, int userId)
        {
            var entriesDeleted = await _completedWorkoutRepository.Delete(workoutId, userId);
            if (entriesDeleted is null)
                return null;
            return entriesDeleted > 0;
        }
        public async Task<CompletedWorkoutVM> GetCompletedWorkout(int userId, int workoutId, bool includeReps)
        {
            var user = await Extensions.FindUser(userId, _userRepository, _cache);
            var workout = await _completedWorkoutRepository.GetNoTracking(workoutId);
            if (workout is null || userId != workout.UserId)
                return null;
            var defaults = user.GetUserDefaultsForActivity(workout.ActivityType);

            return new CompletedWorkoutVM(workout, defaults, includeReps);
        }
        public async Task<IReadOnlyList<CompletedWorkoutVM>> GetCompletedWorkoutsInDateRange(int userId, string from, string to, bool includeReps) 
        {
            var fromDate = from.ValidateDate();
            var toDate = to.ValidateDate();

            var user = await Extensions.FindUser(userId, _userRepository, _cache);
            var workout = await _completedWorkoutRepository.FindByDateRange(userId, fromDate, toDate, false);

            var userDefaults = user.GetUserDefaultsFormatted();
            return workout?.OrderBy(x => x.CompletedDateTime).Select(x => new CompletedWorkoutVM(x, userDefaults[x.ActivityType], includeReps)).ToList();
        }

        private bool VerifyPlannedRepetitionIds(CompletedWorkoutVM completedWorkout, PlannedWorkout plannedWorkout)
        {
            var completedReferencingPlanned = completedWorkout.CompletedRepetitions.Where(x => x.PlannedRepetitionId.HasValue)
                .Select(x => x.PlannedRepetitionId.Value).Distinct().ToList();
            if (completedReferencingPlanned.Count == 0)
                return true;
            var plannedRepetitionIds = plannedWorkout.PlannedRepetitions.Select(x => x.Id).ToList();

            return completedReferencingPlanned.Any(x => !plannedRepetitionIds.Contains(x));
        }

        private async Task VerifyReferencedPlannedWorkout(CompletedWorkoutVM viewModel, int userId, bool throwIfNoPlannedWorkout)
        {
            if (throwIfNoPlannedWorkout && !viewModel.PlannedWorkoutId.HasValue)
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Missing planned workout id.");
            else if (!viewModel.PlannedWorkoutId.HasValue)
            {
                if (viewModel.CompletedRepetitions.Any(x => x.PlannedRepetitionId.HasValue))
                {
                    throw new RestException(System.Net.HttpStatusCode.BadRequest, "Planned repetitions cannot exist if not planned workout exists.");
                }
                return;
            }

            var plannedWorkout = await _plannedWorkoutRepository.GetNoTracking(viewModel.PlannedWorkoutId.Value);
            if (plannedWorkout is null || plannedWorkout.UserId != userId)
                throw new RestException(System.Net.HttpStatusCode.NotFound, "Planned workout not found.");
            if (!VerifyPlannedRepetitionIds(viewModel, plannedWorkout))
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Not all planned repetitions were found.");
        }
    }
}

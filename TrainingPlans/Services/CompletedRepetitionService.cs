using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.ExceptionHandling;
using TrainingPlans.Repositories;
using TrainingPlans.ViewModels;

namespace TrainingPlans.Services
{
    public class CompletedRepetitionService : ICompletedRepetitionService
    {
        private readonly ICompletedRepetitionRepository _completedRepetitionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDistributedCache _cache;

        public CompletedRepetitionService(ICompletedRepetitionRepository completedRepetitionRepository, IUserRepository userRepository, IDistributedCache cache)
        {
            _completedRepetitionRepository = completedRepetitionRepository;
            _userRepository = userRepository;
            _cache = cache;
        }

        public async Task<bool?> DeleteRepetition(int userId, int workoutId, int repetitionId)
        {
            var existingRepsCount = _completedRepetitionRepository.GetNumberOfRepsInWorkout(workoutId);
            if (existingRepsCount < 2)
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Cannot delete the only repetition in a workout. Add another before deleting.");

            var result = await _completedRepetitionRepository.Delete(repetitionId, workoutId, userId);
            if (result is null)
                return null;

            return result > 0;
        }

        public async Task<CompletedRepetitionVM> GetSingle(int userId, int workoutId, int repetitionId)
        {
            var model = await _completedRepetitionRepository.Get(repetitionId, workoutId, userId, false);
            if (model is null)
                return null;

            var user = await Extensions.FindUser(userId, _userRepository, _cache);
            var userDefaults = user.GetUserDefaultsForActivity(model.ActivityType);

            return new CompletedRepetitionVM(model, userDefaults);
        }

        public async Task<bool?> UpdateRepetition(int userId, int workoutId, CompletedRepetitionVM viewModel)
        {
            var existing = await _completedRepetitionRepository.Get(viewModel.Id, workoutId, userId);
            if (viewModel is null)
                return null;

            if (viewModel.PlannedRepetitionId.HasValue && viewModel.PlannedRepetitionId != existing.PlannedRepetitionId)
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Cannot change planned repetition in this request.");

            existing.UpdateFromVM(viewModel);

            return (await _completedRepetitionRepository.Update(existing)) > 0;
        }
    }
}

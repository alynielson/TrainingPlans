using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.Models;
using TrainingPlans.ExceptionHandling;
using TrainingPlans.Repositories;
using TrainingPlans.ViewModels;

namespace TrainingPlans.Services
{
    public class PlannedRepetitionService : IPlannedRepetitionService
    {
        private readonly IPlannedRepetitionRepository _plannedRepetitionRepository;
        private readonly IUserRepository _userRepository;

        public PlannedRepetitionService(IPlannedRepetitionRepository plannedRepetitionRepository, IUserRepository userRepository)
        {
            _plannedRepetitionRepository = plannedRepetitionRepository;
            _userRepository = userRepository;
        }

        public async Task<bool?> DeleteRepetition(int userId, int workoutId, int repetitionId)
        {
            var existingRepsCount = _plannedRepetitionRepository.GetNumberOfRepsInWorkout(workoutId);
            if (existingRepsCount < 2)
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Cannot delete the only repetition in a workout. Add another before deleting.");

            var result = await _plannedRepetitionRepository.Delete(repetitionId, workoutId, userId);
            if (result is null)
                return null;

            return result > 0;
        }

        public async Task<PlannedRepetitionVM> GetSingle(int userId, int workoutId, int repetitionId)
        {
            var model = await _plannedRepetitionRepository.Get(repetitionId, workoutId, userId);
            if (model is null)
                return null;

            var user = await Extensions.FindUser(userId, _userRepository);
            var userDefaults = user.GetUserDefaultsForActivity(model.ActivityType);

            return new PlannedRepetitionVM(model, userDefaults);
        }

        public async Task<bool?> UpdateRepetition(int userId, int workoutId, PlannedRepetitionVM viewModel)
        {
            var existing = await _plannedRepetitionRepository.Get(viewModel.Id, workoutId, userId);
            if (viewModel is null)
                return null;

            if (existing.OrderIsEdited(viewModel))
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Date information cannot be edited in this request.");

            existing.UpdateFromVM(viewModel);

            return (await _plannedRepetitionRepository.Update(existing)) > 0;
        }
    }
}

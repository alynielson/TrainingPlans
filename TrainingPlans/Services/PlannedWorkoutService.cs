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

        public async Task<IReadOnlyList<PlannedWorkoutVM>> GetInDateRange(string from, string to, int userId)
        {
            await FindUser(userId);
            var plan = await _plannedWorkoutRepository.FindByDateRange(userId, from.ValidateDate(), to.ValidateDate());
            return plan.OrderBy(x => x.ScheduledDate).Select(x => new PlannedWorkoutVM(x)).ToList();
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

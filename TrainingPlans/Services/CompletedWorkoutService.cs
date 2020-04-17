using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Repositories;
using TrainingPlans.ViewModels;

namespace TrainingPlans.Services
{
    public class CompletedWorkoutService : ICompletedWorkoutService
    {
        private readonly ICompletedWorkoutRepository _completedWorkoutRepository;
        private readonly IUserRepository _userRepository;

        public CompletedWorkoutService(ICompletedWorkoutRepository CompletedWorkoutRepository, IUserRepository userRepository)
        {
            _completedWorkoutRepository = CompletedWorkoutRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> CompleteUnplannedWorkout(int userId, CompletedWorkoutVM viewModel)
        {

        }
    }
}

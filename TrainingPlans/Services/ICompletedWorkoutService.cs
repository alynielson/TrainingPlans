using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.ViewModels;

namespace TrainingPlans.Services
{
    public interface ICompletedWorkoutService
    {
        Task<bool> CompleteUnplannedWorkout(int userId, CompletedWorkoutVM viewModel);
        Task<bool> CompletePlannedWorkout(int userId, CompletedWorkoutVM viewModel);
        Task<bool?> UpdateCompletedWorkout(int userId, int workoutId, CompletedWorkoutVM viewModel);
        Task<bool?> DeleteCompletedWorkout(int workoutId, int userId);
        Task<CompletedWorkoutVM> GetCompletedWorkout(int userId, int workoutId, bool includeReps);
        Task<IReadOnlyList<CompletedWorkoutVM>> GetCompletedWorkoutsInDateRange(int userId, string from, string to, bool includeReps);
    }
}

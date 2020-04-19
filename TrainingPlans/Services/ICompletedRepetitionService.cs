using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.ViewModels;

namespace TrainingPlans.Services
{
    public interface ICompletedRepetitionService
    {
        Task<bool?> DeleteRepetition(int userId, int workoutId, int repetitionId);
        Task<CompletedRepetitionVM> GetSingle(int userId, int workoutId, int repetitionId);
        Task<bool?> UpdateRepetition(int userId, int workoutId, CompletedRepetitionVM viewModel);
    }
}

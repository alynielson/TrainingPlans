using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.ViewModels;

namespace TrainingPlans.Services
{
    public interface IPlannedRepetitionService
    {
        Task<bool?> DeleteRepetition(int userId, int workoutId, int repetitionId);
        Task<PlannedRepetitionVM> GetSingle(int userId, int workoutId, int repetitionId);
    }
}

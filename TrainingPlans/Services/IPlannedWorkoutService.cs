using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.Models;
using TrainingPlans.ViewModels;

namespace TrainingPlans.Services
{
    public interface IPlannedWorkoutService
    {
        Task<bool> Create(PlannedWorkoutVM workout, int userId);
        Task<IReadOnlyList<PlannedWorkoutVM>> GetInDateRange(string from, string to, int id);
    }
}

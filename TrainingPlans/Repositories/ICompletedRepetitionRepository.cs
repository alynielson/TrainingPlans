﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.Models;

namespace TrainingPlans.Repositories
{
    public interface ICompletedRepetitionRepository : IEntityRepository<CompletedRepetition>
    {
        int GetNumberOfRepsInWorkout(int workoutId);
        Task<CompletedRepetition> Get(int repetitionId, int workoutId, int userId, bool track = true);
        Task<int?> Delete(int repetitionId, int workoutId, int userId);
    }
}

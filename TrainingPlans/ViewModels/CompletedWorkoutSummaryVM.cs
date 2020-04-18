using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.Models;

namespace TrainingPlans.ViewModels
{
    public class CompletedWorkoutSummaryVM : AbstractSummaryVM<CompletedRepetition>
    {
        protected override void AddToTotals(CompletedRepetition repetition, UserDefaults userDefaults, bool isRest)
        {
            if (isRest)
                AddToTotals(repetition.RestDistanceQuantity, repetition.RestDistanceUom, repetition.RestTimeQuantity, repetition.RestTimeUom, userDefaults, 1, isRest);
            else
                AddToTotals(repetition.DistanceQuantity, repetition.DistanceUom, repetition.TimeQuantity, repetition.TimeUom, userDefaults, 1, isRest);
        }

        public CompletedWorkoutSummaryVM(List<CompletedRepetition> reps, UserDefaults defaults) : base(reps, defaults)
        {
        }
    }
}

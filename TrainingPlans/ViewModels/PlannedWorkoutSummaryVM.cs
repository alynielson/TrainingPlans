using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Common;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.Database.Models;

namespace TrainingPlans.ViewModels
{
    public class PlannedWorkoutSummaryVM : AbstractSummaryVM<PlannedRepetition>
    {
        protected override void AddToTotals(PlannedRepetition repetition, UserDefaults userDefaults, bool isRest)
        {
            if (isRest)
                AddToTotals(repetition.RestDistanceQuantity, repetition.RestDistanceUom, repetition.RestTimeQuantity, repetition.RestTimeUom, userDefaults, repetition.Quantity, isRest);
            else
                AddToTotals(repetition.DistanceQuantity, repetition.DistanceUom, repetition.TimeQuantity, repetition.TimeUom, userDefaults, repetition.Quantity, isRest);
        }

        public PlannedWorkoutSummaryVM(List<PlannedRepetition> reps, UserDefaults defaults) : base(reps, defaults)
        {
        }
    }
}

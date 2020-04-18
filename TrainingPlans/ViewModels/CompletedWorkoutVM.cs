using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Common;
using TrainingPlans.Database.Models;

namespace TrainingPlans.ViewModels
{
    public class CompletedWorkoutVM : AbstractWorkoutVM
    {
        public string CompletedDateTime { get; set; }
        public int? PlannedWorkoutId { get; set; }
        public List<CompletedRepetitionVM> CompletedRepetitions { get; set; }

        public CompletedWorkoutSummaryVM WorkoutSummary { get; set; }

        public CompletedWorkoutVM(CompletedWorkout model, UserDefaults defaults, bool includeReps) : base(model)
        {
            PlannedWorkoutId = model.PlannedWorkoutId;
            CompletedDateTime = model.CompletedDateTime.ToString(Constants.DateTimeOffsetFormatString);
            CompletedRepetitions = includeReps ? model.CompletedRepetitions?
                .Select(x => new CompletedRepetitionVM(x, defaults)).OrderBy(x => x.Order).ToList() : null;
            WorkoutSummary = new CompletedWorkoutSummaryVM(model.CompletedRepetitions, defaults);
        }
        public CompletedWorkoutVM() { }
    }
}

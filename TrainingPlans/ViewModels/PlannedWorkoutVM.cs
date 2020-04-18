using System.Linq;
using System.Collections.Generic;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.Database.Models;
using TrainingPlans.Common;
using TrainingPlans.Database.Contracts;
using System.Text.Json.Serialization;

namespace TrainingPlans.ViewModels
{
    public class PlannedWorkoutVM : AbstractWorkoutVM, IOrderable
    {
        public TimeOfDay? TimeOfDay { get; set; } = Database.AdditionalData.TimeOfDay.Any;
        public string ScheduledDate { get; set; }
        public int Order { get; set; }
        public List<PlannedRepetitionVM> PlannedRepetitions { get; set; }
        public PlannedWorkoutSummaryVM WorkoutSummary { get; set; }

        public PlannedWorkoutVM(PlannedWorkout model, UserDefaults defaults, bool includeReps) : base(model)
        {
            TimeOfDay = model.TimeOfDay;
            ScheduledDate = model.ScheduledDate.ToString(Constants.DateOnlyFormatString);
            Order = model.Order;
            PlannedRepetitions = includeReps ? model.PlannedRepetitions?
                .Select(x => new PlannedRepetitionVM(x, defaults)).OrderBy(x => x.Order).ToList() : null;
            WorkoutSummary = new PlannedWorkoutSummaryVM(model.PlannedRepetitions, defaults);
        }
        public PlannedWorkoutVM() { }
    }
}

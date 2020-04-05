using System.Linq;
using System.Collections.Generic;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.Database.Models;
using TrainingPlans.Common;
using TrainingPlans.Database.Contracts;

namespace TrainingPlans.ViewModels
{
    public class PlannedWorkoutVM : AbstractWorkoutVM, IOrderable
    {
        public TimeOfDay? TimeOfDay { get; set; } = Database.AdditionalData.TimeOfDay.Any;
        public string ScheduledDate { get; set; }
        public int Order { get; set; }
        public List<PlannedRepetitionVM> PlannedRepetitions { get; set; }

        public PlannedWorkoutVM(PlannedWorkout model, UserDefaults defaults, bool includeReps)
        {
            Id = model.Id;
            Name = model.Name;
            TimeOfDay = model.TimeOfDay;
            ScheduledDate = model.ScheduledDate.ToString(Constants.DateOnlyFormatString);
            Order = model.Order;
            ActivityType = model.ActivityType;
            WorkoutType = model.WorkoutType;
            PlannedRepetitions = includeReps ? model.PlannedRepetitions?
                .Select(x => new PlannedRepetitionVM(x, defaults)).OrderBy(x => x.Order).ToList() : null;
            WorkoutSummary = new WorkoutSummaryVM(model.PlannedRepetitions, defaults);
        }
        public PlannedWorkoutVM() { }
    }
}

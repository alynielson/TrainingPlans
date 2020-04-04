using System.Linq;
using System.Collections.Generic;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.Database.Models;
using TrainingPlans.Common;
using TrainingPlans.Database.Interfaces;

namespace TrainingPlans.ViewModels
{
    public class PlannedWorkoutVM : IOrderable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeOfDay? TimeOfDay { get; set; } = Database.AdditionalData.TimeOfDay.Any;
        public string ScheduledDate { get; set; }
        public int Order { get; set; }
        public ActivityType ActivityType { get; set; }
        public WorkoutType WorkoutType { get; set; }
        public List<PlannedRepetitionVM> PlannedRepetitions { get; set; }
        public string CompletedDateTime { get; set; }

        public PlannedWorkoutVM(PlannedWorkout model)
        {
            Id = model.Id;
            Name = model.Name;
            TimeOfDay = model.TimeOfDay;
            ScheduledDate = model.ScheduledDate.ToString(Constants.DateOnlyFormatString);
            Order = model.Order;
            ActivityType = model.ActivityType;
            WorkoutType = model.WorkoutType;
            PlannedRepetitions = model.PlannedRepetitions?.Select(x => new PlannedRepetitionVM(x)).OrderBy(x => x.Order).ToList();
            CompletedDateTime = model.CompletedDateTime?.ToString(Constants.DateTimeOffsetFormatString);
        }
        public PlannedWorkoutVM() { }
    }
}

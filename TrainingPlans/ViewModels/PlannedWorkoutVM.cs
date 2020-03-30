using System.Linq;
using System.Collections.Generic;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.Database.Models;
using System;
using TrainingPlans.Common;

namespace TrainingPlans.ViewModels
{
    public class PlannedWorkoutVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TimeOfDay { get; set; }
        public string ScheduledDate { get; set; }
        public int Order { get; set; }
        public string ActivityType { get; set; }
        public string WorkoutType { get; set; }
        public List<PlannedRepetitionVM> PlannedRepetitions { get; set; }

        public PlannedWorkoutVM(PlannedWorkout model)
        {
            Id = model.Id;
            Name = model.Name;
            TimeOfDay = model.TimeOfDay.ToString();
            ScheduledDate = model.ScheduledDate.ToString(Constants.DateFormatString);
            Order = model.Order;
            ActivityType = model.ActivityType.ToString();
            WorkoutType = model.WorkoutType.ToString();
            PlannedRepetitions = model.PlannedRepetitions?.Select(x => new PlannedRepetitionVM(x)).ToList();
        }
        public PlannedWorkoutVM() { }
    }
}

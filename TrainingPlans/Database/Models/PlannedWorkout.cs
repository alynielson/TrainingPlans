using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using TrainingPlans.Common;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.Database.Contracts;
using TrainingPlans.ViewModels;

namespace TrainingPlans.Database.Models
{
    public class PlannedWorkout : AbstractWorkout, IOrderable
    {
        public TimeOfDay TimeOfDay { get; set; }
        public DateTime ScheduledDate { get; set; }
        public int Order { get; set; }
        public List<PlannedRepetition> PlannedRepetitions { get; set; }
        public PlannedWorkout() { }
        public PlannedWorkout(PlannedWorkoutVM viewModel, int userId)
        {
            Id = viewModel.Id;
            Name = viewModel.Name;
            TimeOfDay = viewModel.TimeOfDay.HasValue ? viewModel.TimeOfDay.Value : TimeOfDay.Any;
            ScheduledDate = DateTime.Parse(viewModel.ScheduledDate);
            Order = viewModel.Order;
            ActivityType = viewModel.ActivityType;
            WorkoutType = viewModel.WorkoutType;
            PlannedRepetitions = viewModel.PlannedRepetitions?.Select(x => new PlannedRepetition(x, viewModel, userId)).ToList();
            UserId = userId;
        }
    }
}

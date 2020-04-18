using System;
using System.Collections.Generic;
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
        public PlannedWorkout() : base() { } 
        public PlannedWorkout(PlannedWorkoutVM viewModel, int userId, int id) : base(viewModel, userId, id)
        {
            TimeOfDay = viewModel.TimeOfDay.HasValue ? viewModel.TimeOfDay.Value : TimeOfDay.Any;
            ScheduledDate = DateTime.Parse(viewModel.ScheduledDate);
            Order = viewModel.Order;
            PlannedRepetitions = viewModel.PlannedRepetitions?.Select(x => new PlannedRepetition(x, ActivityType, userId, x.Id, id)).ToList();
        }

        public void UpdateDateValues(PlannedWorkoutDateUpdate dateUpdate)
        {
            TimeOfDay = dateUpdate.TimeOfDay;
            ScheduledDate = DateTime.Parse(dateUpdate.ScheduledDate);
            Order = dateUpdate.WorkoutOrders.First(x => x.WorkoutId == Id).Order;
        }

        public void UpdateFromVM(PlannedWorkoutVM viewModel)
        {
            Name = viewModel.Name;
            ActivityType = viewModel.ActivityType;
            WorkoutType = viewModel.WorkoutType;
            PlannedRepetitions = GetPlannedRepetitionsFromVM(viewModel.PlannedRepetitions);
        }

        private List<PlannedRepetition> GetPlannedRepetitionsFromVM(List<PlannedRepetitionVM> viewModelReps)
        {
            var newReps = viewModelReps?.Where(x => x.Id == 0).Select(x => new PlannedRepetition(x, ActivityType, UserId.Value, 0, Id)).ToList();
            return GetPlannedRepetitionsFromVM(newReps, PlannedRepetitions, viewModelReps);
        }

        public bool DatesAreEdited(PlannedWorkoutVM update)
        {
            return update.Order != Order
                || update.ScheduledDate != ScheduledDate.ToString(Constants.DateOnlyFormatString)
                || update.TimeOfDay != TimeOfDay;
        }
    }
}

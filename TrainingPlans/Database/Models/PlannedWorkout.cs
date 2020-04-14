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
        public PlannedWorkout(PlannedWorkoutVM viewModel, int userId, int id)
        {
            Id = id;
            Name = viewModel.Name;
            TimeOfDay = viewModel.TimeOfDay.HasValue ? viewModel.TimeOfDay.Value : TimeOfDay.Any;
            ScheduledDate = DateTime.Parse(viewModel.ScheduledDate);
            Order = viewModel.Order;
            ActivityType = viewModel.ActivityType;
            WorkoutType = viewModel.WorkoutType;
            PlannedRepetitions = viewModel.PlannedRepetitions?.Select(x => new PlannedRepetition(x, viewModel.ActivityType, userId, x.Id)).ToList();
            UserId = userId;
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
            PlannedRepetitions = GetPlannedRepetitionsFromVM(viewModel.PlannedRepetitions, viewModel.ActivityType);
        }

        private List<PlannedRepetition> GetPlannedRepetitionsFromVM(List<PlannedRepetitionVM> viewModelReps, ActivityType activity)
        {
            var newReps = viewModelReps?.Where(x => x.Id == 0).Select(x => new PlannedRepetition(x, activity, UserId.Value, 0)).ToList();
            var oldRepVMs = viewModelReps?.Where(x => x.Id != 0).ToDictionary(x => x.Id, x => x);
            var oldReps = PlannedRepetitions.Select(x =>
            {
                if (oldRepVMs.TryGetValue(x.Id, out var vm))
                {
                    x.UpdateFromParentVM(vm, activity);
                    return x;
                }
                return null;
            }).Where(x => x != null).ToList();
            newReps.AddRange(oldReps);
            return newReps;
        }

        public bool DatesAreEdited(PlannedWorkoutVM update)
        {
            return update.Order != Order
                || update.ScheduledDate != ScheduledDate.ToString(Constants.DateOnlyFormatString)
                || update.TimeOfDay != TimeOfDay;
        }
    }
}

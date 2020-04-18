using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TrainingPlans.Common;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.Database.Contracts;
using TrainingPlans.ViewModels;

namespace TrainingPlans.Database.Models
{
    public class PlannedRepetition : AbstractRepetition
    {
        public int Quantity { get; set; } = 1;
        public PlannedWorkout PlannedWorkout { get; set; }
        [ForeignKey("PlannedWorkout")]
        public int PlannedWorkoutId { get; set; }
        public PlannedRepetition(PlannedRepetitionVM viewModel, ActivityType activityType, int userId, int id, int workoutId) : base(viewModel, activityType, userId, id)
        {
            Quantity = viewModel.Quantity;
            PlannedWorkoutId = workoutId;
            
        }
        public void UpdateFromWorkoutVM(PlannedRepetitionVM viewModel, ActivityType activityType)
        {
            base.UpdateFromWorkoutVM(viewModel, activityType);
            Quantity = viewModel.Quantity;
        }

        public bool OrderIsEdited(PlannedRepetitionVM viewModel)
        {
            return !(Order == viewModel.Order);
        }

        public void UpdateFromVM(PlannedRepetitionVM viewModel)
        {
            base.UpdateFromVM(viewModel);
            Quantity = viewModel.Quantity;
        }

        public PlannedRepetition() : base()
        {
        }
    }
}

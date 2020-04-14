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
        public PlannedRepetition(PlannedRepetitionVM viewModel, ActivityType activityType, int userId, int id)
        {
            Id = id;
            DistanceQuantity = viewModel.DistanceQuantity;
            DistanceUom = viewModel.DistanceUom;
            TimeQuantity = viewModel.TimeQuantity;
            TimeUom = viewModel.TimeUom;
            Notes = viewModel.Notes;
            Quantity = viewModel.Quantity;
            RestDistanceQuantity = viewModel.RestDistanceQuantity;
            RestDistanceUom = viewModel.RestDistanceUom;
            RestTimeQuantity = viewModel.RestTimeQuantity;
            RestTimeUom = viewModel.RestTimeUom;
            Order = viewModel.Order;
            ActivityType = activityType;
            UserId = userId;
        }
        public void UpdateFromParentVM(PlannedRepetitionVM viewModel, ActivityType activityType)
        {
            DistanceQuantity = viewModel.DistanceQuantity;
            DistanceUom = viewModel.DistanceUom;
            TimeQuantity = viewModel.TimeQuantity;
            TimeUom = viewModel.TimeUom;
            Notes = viewModel.Notes;
            Quantity = viewModel.Quantity;
            RestDistanceQuantity = viewModel.RestDistanceQuantity;
            RestDistanceUom = viewModel.RestDistanceUom;
            RestTimeQuantity = viewModel.RestTimeQuantity;
            RestTimeUom = viewModel.RestTimeUom;
            Order = viewModel.Order;
            ActivityType = activityType;
        }

        public bool OrderIsEdited(PlannedRepetitionVM viewModel)
        {
            return !(Order == viewModel.Order);
        }

        public void UpdateFromVM(PlannedRepetitionVM viewModel)
        {
            DistanceQuantity = viewModel.DistanceQuantity;
            DistanceUom = viewModel.DistanceUom;
            TimeQuantity = viewModel.TimeQuantity;
            TimeUom = viewModel.TimeUom;
            Notes = viewModel.Notes;
            Quantity = viewModel.Quantity;
            RestDistanceQuantity = viewModel.RestDistanceQuantity;
            RestDistanceUom = viewModel.RestDistanceUom;
            RestTimeQuantity = viewModel.RestTimeQuantity;
            RestTimeUom = viewModel.RestTimeUom;
        }

        public PlannedRepetition()
        {
        }
    }
}

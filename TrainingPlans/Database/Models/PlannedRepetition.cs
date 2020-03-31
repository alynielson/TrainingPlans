using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TrainingPlans.Common;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.ViewModels;

namespace TrainingPlans.Database.Models
{
    public class PlannedRepetition
    {
        [Key]
        public int Id { get; set; }
        public double? DistanceQuantity { get; set; }
        public DistanceUom? DistanceUom { get; set; }
        public double? TimeQuantity { get; set; }
        public TimeUom? TimeUom { get; set; }
        public string Notes { get; set; }
        public int Quantity { get; set; } = 1;
        public PlannedWorkout PlannedWorkout { get; set; }
        public PlannedRepetition(PlannedRepetitionVM viewModel)
        {
            Id = viewModel.Id;
            DistanceQuantity = viewModel.DistanceQuantity;
            DistanceUom = viewModel.DistanceUom is null ? null 
                : (DistanceUom?)Enum.Parse<DistanceUom>(viewModel.DistanceUom); ;
            TimeQuantity = viewModel.TimeQuantity;
            TimeUom = viewModel.TimeUom is null ? null
                : (TimeUom?)Enum.Parse<TimeUom>(viewModel.TimeUom);
            Notes = viewModel.Notes;
            Quantity = viewModel.Quantity;
        }
        public PlannedRepetition()
        {

        }
    }
}

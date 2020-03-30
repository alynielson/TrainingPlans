using System;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.Database.Models;

namespace TrainingPlans.ViewModels
{
    public class PlannedRepetitionVM
    {
        public int Id { get; set; }
        public double? DistanceQuantity { get; set; }
        public string DistanceUom { get; set; }
        public double? TimeQuantity { get; set; }
        public string TimeUom { get; set; }
        public string Notes { get; set; }
        public int Quantity { get; set; } = 1;

        public PlannedRepetitionVM(PlannedRepetition model)
        {
            Id = model.Id;
            DistanceQuantity = model.DistanceQuantity;
            DistanceUom = model.DistanceUom.ToString();
            TimeQuantity = model.TimeQuantity;
            TimeUom = model.TimeUom.ToString();
            Notes = model.Notes;
            Quantity = model.Quantity;
        }
        public PlannedRepetitionVM() { }
    }
}

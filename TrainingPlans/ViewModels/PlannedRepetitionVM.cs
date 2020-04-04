using System;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.Database.Models;
using TrainingPlans.Common;
using TrainingPlans.Database.Contracts;

namespace TrainingPlans.ViewModels
{
    public class PlannedRepetitionVM : IOrderable
    {
        public int Id { get; set; }
        public double? DistanceQuantity { get; set; }
        public DistanceUom? DistanceUom { get; set; }
        public double? TimeQuantity { get; set; }
        public TimeUom? TimeUom { get; set; }
        public string Notes { get; set; }
        public int Quantity { get; set; } = 1;
        public int Order { get; set; }
        public string PaceMinutesPerMile { get; private set; }
        public double? RestDistanceQuantity { get; set; }
        public DistanceUom? RestDistanceUom { get; set; }
        public double? RestTimeQuantity { get; set; }
        public TimeUom? RestTimeUom { get; set; }
        public string RestPaceMinutesPerMile { get; set; }

        public PlannedRepetitionVM(PlannedRepetition model)
        {
            Id = model.Id;
            DistanceQuantity = model.DistanceQuantity;
            DistanceUom = model.DistanceUom;
            TimeQuantity = model.TimeQuantity;
            TimeUom = model.TimeUom;
            Notes = model.Notes;
            Quantity = model.Quantity;
            RestDistanceQuantity = model.RestDistanceQuantity;
            RestDistanceUom = model.RestDistanceUom;
            RestTimeQuantity = model.RestTimeQuantity;
            RestTimeUom = model.RestTimeUom;
            Order = model.Order;
            SetPaces();
        }
        public PlannedRepetitionVM() { }

        private void SetPaces()
        {
            if (DistanceQuantity.HasValue && DistanceUom.HasValue && TimeQuantity.HasValue && TimeUom.HasValue)
            {
                PaceMinutesPerMile = UnitConversions.GetMinutesPerMileAsString(DistanceQuantity.Value, DistanceUom.Value, TimeQuantity.Value, TimeUom.Value);
            }
            if (RestDistanceQuantity.HasValue && RestDistanceUom.HasValue && RestDistanceQuantity.HasValue && RestTimeUom.HasValue)
            {
                RestPaceMinutesPerMile = UnitConversions.GetMinutesPerMileAsString(RestDistanceQuantity.Value, RestDistanceUom.Value, RestTimeQuantity.Value, RestTimeUom.Value);
            }
        }
    }
}

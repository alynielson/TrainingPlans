using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Common;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.Database.Contracts;

namespace TrainingPlans.ViewModels
{
    public abstract class AbstractRepetitionVM : IOrderable
    {
        public int Id { get; set; }
        public double? DistanceQuantity { get; set; }
        public DistanceUom? DistanceUom { get; set; }
        public double? TimeQuantity { get; set; }
        public TimeUom? TimeUom { get; set; }
        public string Notes { get; set; }
        public int Order { get; set; }
        public string PaceMinutesPerMile { get; protected set; }
        public double? RestDistanceQuantity { get; set; }
        public DistanceUom? RestDistanceUom { get; set; }
        public double? RestTimeQuantity { get; set; }
        public TimeUom? RestTimeUom { get; set; }
        public string RestPaceMinutesPerMile { get; set; }

        protected void SetPaces()
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

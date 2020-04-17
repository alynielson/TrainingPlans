using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Common;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.Database.Contracts;
using TrainingPlans.Database.Models;

namespace TrainingPlans.ViewModels
{
    public abstract class AbstractRepetitionVM : IOrderable, IIdentifiable
    {
        public int Id { get; set; }
        public double? DistanceQuantity { get; set; }
        public DistanceUom? DistanceUom { get; set; }
        public double? TimeQuantity { get; set; }
        public TimeUom? TimeUom { get; set; }
        public string Notes { get; set; }
        public int Order { get; set; }
        public string Pace { get; protected set; }
        public string PaceUom { get; protected set; }
        public double? RestDistanceQuantity { get; set; }
        public DistanceUom? RestDistanceUom { get; set; }
        public double? RestTimeQuantity { get; set; }
        public TimeUom? RestTimeUom { get; set; }
        public string RestPace { get; protected set; }

        public AbstractRepetitionVM(AbstractRepetition model, UserDefaults defaults = null)
        {
            Id = model.Id;
            DistanceQuantity = model.DistanceQuantity;
            DistanceUom = model.DistanceUom;
            TimeQuantity = model.TimeQuantity;
            TimeUom = model.TimeUom;
            Notes = model.Notes;
            RestDistanceQuantity = model.RestDistanceQuantity;
            RestDistanceUom = model.RestDistanceUom;
            RestTimeQuantity = model.RestTimeQuantity;
            RestTimeUom = model.RestTimeUom;
            Order = model.Order;
            if (defaults is { })
                SetPaces(defaults);
        }

        public AbstractRepetitionVM() { }

        protected void SetPaces(UserDefaults defaults)
        {
            if (DistanceQuantity.HasValue && DistanceUom.HasValue && TimeQuantity.HasValue && TimeUom.HasValue)
            {
                Pace = UnitConversions.GetPaceAsString(DistanceQuantity.Value, DistanceUom.Value, TimeQuantity.Value, TimeUom.Value, defaults);
            }
            if (RestDistanceQuantity.HasValue && RestDistanceUom.HasValue && RestDistanceQuantity.HasValue && RestTimeUom.HasValue)
            {
                RestPace = UnitConversions.GetPaceAsString(RestDistanceQuantity.Value, RestDistanceUom.Value, RestTimeQuantity.Value, RestTimeUom.Value, defaults);
            }
            PaceUom = defaults.IsPaceDistancePerTime ? $"{defaults.DistanceUom}/{defaults.TimeUom}" : $"{defaults.TimeUom}/{defaults.DistanceUom}";
        }
    }
}

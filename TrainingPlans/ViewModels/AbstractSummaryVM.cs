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
    public abstract class AbstractSummaryVM<T> where T : AbstractRepetition
    {
        public double TotalDistanceQuantity { get; set; }
        public DistanceUom TotalDistanceUom { get; set; }
        public double TotalTimeQuantity { get; set; }
        public TimeUom TotalTimeUom { get; set; }
        public string AveragePace { get; set; }
        public string AveragePaceUom { get; set; }
        public List<int> RepetitionIds { get; set; }

        public AbstractSummaryVM(List<T> modelReps, UserDefaults userDefaults)
        {
            if (modelReps is null || modelReps.Count == 0)
                return;

            TotalDistanceUom = userDefaults.DistanceUom;
            TotalTimeUom = userDefaults.TimeUom;

            modelReps.ForEach(x =>
            {
                AddToTotals(x, userDefaults, false);
                AddToTotals(x, userDefaults, true);
            });

            RepetitionIds = modelReps.Select(x => x.Id).ToList();
            AveragePace = UnitConversions.GetPaceAsString(TotalDistanceQuantity, TotalDistanceUom, TotalTimeQuantity, TotalTimeUom, userDefaults);
            AveragePaceUom = userDefaults.IsPaceDistancePerTime
                ? $"{userDefaults.DistanceUom}/{userDefaults.TimeUom}" : $"{userDefaults.TimeUom}/{userDefaults.DistanceUom}";
        }

        protected abstract void AddToTotals(T repetition, UserDefaults userDefaults, bool isRest);

        protected void AddToTotals(double? distanceQuantity, DistanceUom? distanceUom, double? timeQuantity, TimeUom? timeUom,
            UserDefaults userDefaults, int quantity, bool isRest)
        {
            double? repDistance = null;
            double? repTime = null;
            if (distanceQuantity.HasValue && distanceUom.HasValue)
            {
                repDistance = UnitConversions.ConvertDistance(distanceQuantity.Value, distanceUom.Value, userDefaults.DistanceUom) * quantity;
            }
            if (timeQuantity.HasValue && timeUom.HasValue)
            {
                repTime = UnitConversions.ConvertTime(timeQuantity.Value, timeUom.Value, userDefaults.TimeUom) * quantity;
            }

            if (repDistance.HasValue && repTime.HasValue)
            {
                TotalDistanceQuantity += repDistance.Value;
                TotalTimeQuantity += repTime.Value;
            }
            else if (repDistance.HasValue)
            {
                TotalDistanceQuantity += repDistance.Value;
                TotalTimeQuantity += (userDefaults.IsPaceDistancePerTime ? repDistance.Value / userDefaults.Pace : repDistance.Value * userDefaults.Pace);
            }
            else if (repTime.HasValue && !isRest)
            {
                TotalTimeQuantity += repTime.Value;
                TotalDistanceQuantity += (userDefaults.IsPaceDistancePerTime ? repTime.Value * userDefaults.Pace : repTime.Value / userDefaults.Pace);
            }
            else
            {
                return; // could not get reliable values from repetition
            }
        }
    }
}

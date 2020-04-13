using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Common;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.Database.Models;

namespace TrainingPlans.ViewModels
{
    public class WorkoutSummaryVM
    {
        public double TotalDistanceQuantity { get; set; }
        public DistanceUom TotalDistanceUom { get; set; }
        public double TotalTimeQuantity { get; set; }
        public TimeUom TotalTimeUom { get; set; }
        public string AveragePace { get; set; }
        public string AveragePaceUom { get; set; }
        public List<int> RepetitionIds { get; set; }

        public WorkoutSummaryVM(List<PlannedRepetition> modelReps, UserDefaults userDefaults)
        {
            if (modelReps is null || modelReps.Count == 0)
                return;

            TotalDistanceUom = userDefaults.DistanceUom;
            TotalTimeUom = userDefaults.TimeUom;

            modelReps.ForEach(x =>
            {
                AddToTotals(x.DistanceQuantity, x.DistanceUom, x.TimeQuantity, x.TimeUom, userDefaults, x.Quantity, false);
                AddToTotals(x.RestDistanceQuantity, x.RestDistanceUom, x.RestTimeQuantity, x.RestTimeUom, userDefaults, x.Quantity, true);
            });

            RepetitionIds = modelReps.Select(x => x.Id).ToList();
            AveragePace = UnitConversions.GetPaceAsString(TotalDistanceQuantity, TotalDistanceUom, TotalTimeQuantity, TotalTimeUom, userDefaults);
            AveragePaceUom = userDefaults.IsPaceDistancePerTime 
                ? $"{userDefaults.DistanceUom}/{userDefaults.TimeUom}" : $"{userDefaults.TimeUom}/{userDefaults.DistanceUom}";
        }

        public WorkoutSummaryVM(List<CompletedRepetition> modelReps, UserDefaults userDefaults)
        {
            if (modelReps is null || modelReps.Count == 0)
                return;

            modelReps.ForEach(x =>
            {
                AddToTotals(x.DistanceQuantity, x.DistanceUom, x.TimeQuantity, x.TimeUom, userDefaults, 1, false);
                AddToTotals(x.RestDistanceQuantity, x.RestDistanceUom, x.RestTimeQuantity, x.RestTimeUom, userDefaults, 1, true);
            });

            RepetitionIds = modelReps.Select(x => x.Id).ToList();
            AveragePace = UnitConversions.GetPaceAsString(TotalDistanceQuantity, TotalDistanceUom, TotalTimeQuantity, TotalTimeUom, userDefaults);
            AveragePaceUom = userDefaults.IsPaceDistancePerTime
                ? $"{userDefaults.DistanceUom}/{userDefaults.TimeUom}" : $"{userDefaults.TimeUom}/{userDefaults.DistanceUom}";
        }

        private void AddToTotals(double? distanceQuantity, DistanceUom? distanceUom, double? timeQuantity, TimeUom? timeUom, 
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

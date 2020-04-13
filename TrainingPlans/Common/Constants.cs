using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.Database.Contracts;
using TrainingPlans.Database.Models;

namespace TrainingPlans.Common
{
    public static class Constants
    {
        public static readonly string DateOnlyFormatString = "MM/dd/yyyy";
        public static readonly string DateTimeOffsetFormatString = "MM/dd/yyyy HH:mm:ss zzz";
        public static readonly IReadOnlyDictionary<ActivityType, UserDefaults> ActivityDefaults = new Dictionary<ActivityType, UserDefaults>
        {
            { ActivityType.Bike, new UserDefaults
            {
                ActivityType = ActivityType.Bike,
                DistanceUom = DistanceUom.Miles,
                TimeUom = TimeUom.Hours,
                Pace = 14,
                IsPaceDistancePerTime = true
            }
            },
            { ActivityType.Run, new UserDefaults
            {
                ActivityType = ActivityType.Run,
                DistanceUom = DistanceUom.Miles,
                TimeUom = TimeUom.Minutes,
                Pace = 9,
                IsPaceDistancePerTime = false
            }
            },
            { ActivityType.Swim, new UserDefaults
            {
                ActivityType = ActivityType.Swim,
                DistanceUom = DistanceUom.Meters100,
                TimeUom = TimeUom.Minutes,
                Pace = 2,
                IsPaceDistancePerTime = false
            }
            }
        };
    }
}

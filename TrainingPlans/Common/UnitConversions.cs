using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.Database.Models;

namespace TrainingPlans.Common
{
    public static class UnitConversions
    {
        // Distance conversions
        private static readonly double KmToMMultiplier = 1000;
        private static readonly double MToKmMultiplier = 0.001;
        private static readonly double KmToMilesMultiplier = 0.621;
        private static readonly double MilesToKmMultiplier = 1.609;
        private static readonly double MToMilesMultiplier = 0.000621;
        private static readonly double MilesToMMultiplier = 1609.344;
        private static readonly double Meters100ToMMultiplier = 100;
        private static readonly double MToMeters100Multiplier = 0.01;

        // Time conversions
        private static readonly double SToHMultiplier = 0.000278;
        private static readonly double SToMinMultiplier = 0.0167;
        private static readonly double MinToHMultiplier = 0.0167;
        private static readonly double MinToSMultiplier = 60;
        private static readonly double HToMinMultiplier = 60;
        private static readonly double HToSMultiplier = 3600;

        public static double ConvertDistance(this double value, DistanceUom fromUom, DistanceUom toUom)
        {
            if (fromUom == toUom)
                return value;

            switch (fromUom, toUom)
            {
                case (DistanceUom.Kilometers, DistanceUom.Meters):
                    return value * KmToMMultiplier;
                case (DistanceUom.Meters, DistanceUom.Kilometers):
                    return value * MToKmMultiplier;
                case (DistanceUom.Miles, DistanceUom.Kilometers):
                    return value * MilesToKmMultiplier;
                case (DistanceUom.Miles, DistanceUom.Meters):
                    return value * MilesToMMultiplier;
                case (DistanceUom.Meters, DistanceUom.Miles):
                    return value * MToMilesMultiplier;
                case (DistanceUom.Kilometers, DistanceUom.Miles):
                    return value * KmToMilesMultiplier;
                case (DistanceUom.Meters100, DistanceUom.Meters):
                    return (value * Meters100ToMMultiplier);
                case (DistanceUom.Meters100, DistanceUom.Kilometers):
                    return (value * Meters100ToMMultiplier * MToKmMultiplier);
                case (DistanceUom.Meters100, DistanceUom.Miles):
                    return (value * Meters100ToMMultiplier * MToMilesMultiplier);
                case (DistanceUom.Meters, DistanceUom.Meters100):
                    return (value * MToMeters100Multiplier);
                case (DistanceUom.Kilometers, DistanceUom.Meters100):
                    return (value * KmToMMultiplier * MToMeters100Multiplier);
                case (DistanceUom.Miles, DistanceUom.Meters100):
                    return (value * MilesToMMultiplier * MToMeters100Multiplier);
            }

            throw new NotImplementedException($"No conversions exist for {fromUom} to {toUom}.");
        }

        public static double ConvertTime(this double value, TimeUom fromUom, TimeUom toUom)
        {
            if (fromUom == toUom)
                return value;

            switch (fromUom, toUom)
            {
                case (TimeUom.Hours, TimeUom.Seconds):
                    return value * HToSMultiplier;
                case (TimeUom.Hours, TimeUom.Minutes):
                    return value * HToMinMultiplier;
                case (TimeUom.Seconds, TimeUom.Hours):
                    return value * SToHMultiplier;
                case (TimeUom.Seconds, TimeUom.Minutes):
                    return value * SToMinMultiplier;
                case (TimeUom.Minutes, TimeUom.Hours):
                    return value * MinToHMultiplier;
                case (TimeUom.Minutes, TimeUom.Seconds):
                    return value * MinToSMultiplier;
            }

            throw new NotImplementedException($"No conversions exist for {fromUom} to {toUom}.");
        }

        public static string GetPaceAsString(double distance, DistanceUom distanceUom, double time, TimeUom timeUom, UserDefaults defaults)
        {
            var distanceConverted = distance.ConvertDistance(distanceUom, defaults.DistanceUom);

            if (defaults.IsPaceDistancePerTime)
            {
                var timeConverted = time.ConvertTime(timeUom, defaults.TimeUom);
                var result = distanceConverted / timeConverted;
                return $"{Math.Round(result, 2)}";
            }
            else
            {
                // It does not seem right to have hours and seconds as options here. 
                // Just change to minutes and set the defaults to minutes for now, unless having the other options becomes reasonable.
                var timeSeconds = time.ConvertTime(timeUom, TimeUom.Seconds);
                var minutesPerDistanceUnit = timeSeconds * SToMinMultiplier / distanceConverted;
                var minutesWithoutSeconds = (int)minutesPerDistanceUnit;
                var leftoverSeconds = (timeSeconds - minutesWithoutSeconds * MinToSMultiplier * distanceConverted) / distanceConverted;

                defaults.TimeUom = TimeUom.Minutes;

                return $"{minutesWithoutSeconds}:{(leftoverSeconds < 10 ? $"0{leftoverSeconds}" : $"{leftoverSeconds}")}";
            }
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.ExceptionHandling;

namespace TrainingPlans.Common
{
    public static class CommonExtensions
    {
        public static DateTime ValidateDate(this string dateString)
        {
            if (DateTime.TryParse(dateString, out var date))
            {
                return date;
            }
            throw new RestException(System.Net.HttpStatusCode.BadRequest, "Invalid date format.");
        }

        public static bool IsDistinctOrder(this IReadOnlyList<int> values)
        {
            return values.Distinct().ToList().Count == values.Count;
        }
    }
}

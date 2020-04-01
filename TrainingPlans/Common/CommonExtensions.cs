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

        public static string ToJsonString<T>(this T value, JsonSerializerSettings settings = null)
        {
            return JsonConvert.SerializeObject(value, typeof(T), settings);
        }

        public static string ToJsonString(object value, Type type, JsonSerializerSettings settings = null)
        {
            return JsonConvert.SerializeObject(value, type, settings);
        }
    }
}

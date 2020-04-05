using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Common;
using TrainingPlans.Database.AdditionalData;

namespace TrainingPlans.Database.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public List<UserDefaults> UserDefaults { get; set; }

        public IReadOnlyDictionary<ActivityType, UserDefaults> GetUserDefaultsFormatted()
        {
            if (UserDefaults is null || UserDefaults.Count == 0)
                return Constants.ActivityDefaults;

            var result = UserDefaults.ToDictionary(x => x.ActivityType, x => x);

            foreach (var activity in Enum.GetValues(typeof(ActivityType)))
            {
                var activityType = (ActivityType)activity;
                if (!Constants.ActivityDefaults.TryGetValue(activityType, out var applicationDefault))
                {
                    // Activity was added to application but defaults were not. Log (eventually) and continue.
                    continue;
                }
                result.TryAdd(activityType, applicationDefault);
            }

            return result;
        }

        public UserDefaults GetUserDefaultsForActivity(ActivityType activityType)
        {
            var userOverride = UserDefaults.FirstOrDefault(x => x.ActivityType == activityType);
            return userOverride is null ? Constants.ActivityDefaults[activityType] : userOverride;
        }

    }
}

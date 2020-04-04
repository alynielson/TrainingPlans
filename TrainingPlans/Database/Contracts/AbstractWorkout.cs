using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.AdditionalData;
using TrainingPlans.Database.Models;

namespace TrainingPlans.Database.Contracts
{
    public abstract class AbstractWorkout : IOrderable
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public ActivityType ActivityType { get; set; }
        public WorkoutType WorkoutType { get; set; }
        public User User { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; } // This should be required, but making it nullable to not cause a bunch of sql cascade problems on deleting user.
    }
}

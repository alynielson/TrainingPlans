using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingPlans.Database.AdditionalData;

namespace TrainingPlans.Database.Models
{
    public class PlannedWorkout
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeOfDay TimeOfDay { get; set; } = TimeOfDay.Any;
        public int Order { get; set; }
        public ActivityType ActivityType { get; set; }
        public WorkoutType WorkoutType { get; set; }
        public int AthleteId { get; set; }
        [ForeignKey("AthleteId")]
        public User Athlete { get; set; }
        public List<PlannedRepetition> PlannedRepetitions { get; set; }
    }
}

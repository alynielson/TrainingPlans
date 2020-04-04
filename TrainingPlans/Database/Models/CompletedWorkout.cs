using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.Contracts;

namespace TrainingPlans.Database.Models
{
    public class CompletedWorkout : AbstractWorkout
    {
        public DateTimeOffset CompletedDateTime { get; set; }
        public PlannedWorkout PlannedWorkout { get; set; }
        [ForeignKey("PlannedWorkout")]
        public int? PlannedWorkoutId { get; set; }
        public List<CompletedRepetition> CompletedRepetitions { get; set; }
    }
}

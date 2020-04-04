using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingPlans.ViewModels
{
    public class CompletedWorkoutVM : AbstractWorkoutVM
    {
        public string CompletedDateTime { get; set; }
        public int? PlannedWorkoutId { get; set; }
        public List<CompletedRepetitionVM> CompletedRepetitions { get; set; }
    }
}

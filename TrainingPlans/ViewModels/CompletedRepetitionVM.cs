using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingPlans.ViewModels
{
    public class CompletedRepetitionVM : AbstractRepetitionVM
    {
        public int? PlannedRepetitionId { get; set; }
    }
}

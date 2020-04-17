using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlans.Database.Models;

namespace TrainingPlans.ViewModels
{
    public class CompletedRepetitionVM : AbstractRepetitionVM
    {
        public int? PlannedRepetitionId { get; set; }

        public CompletedRepetitionVM() : base()
        {
        }

        public CompletedRepetitionVM(CompletedRepetition model, UserDefaults defaults = null) : base(model, defaults)
        {
            PlannedRepetitionId = model.PlannedRepetitionId;
        }
    }
}
